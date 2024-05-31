using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace PMS
{
    public partial class ChangePassword : Page
    {
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            string userID = Session["UserID"].ToString();
            string oldPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmNewPassword.Text;

            if (newPassword != confirmPassword)
            {
                lblErrorNewPassword.Visible = true;
                return;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT password FROM pms_user WHERE user_id = @userID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userID", userID);
                string storedPasswordHash = (string)command.ExecuteScalar();

                if (storedPasswordHash == null || !VerifyPassword(oldPassword, storedPasswordHash))
                {
                    lblErrorCurrentPassword.Visible = true;
                    return;
                }

                string newPasswordHash = HashPassword(newPassword);
                query = "UPDATE pms_user SET password = @newPassword WHERE user_id = @userID";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@newPassword", newPasswordHash);
                command.Parameters.AddWithValue("@userID", userID);
                command.ExecuteNonQuery();

                lblSuccessPasswordChange.Visible = true;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var enteredHash = HashPassword(enteredPassword);
            return enteredHash.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
