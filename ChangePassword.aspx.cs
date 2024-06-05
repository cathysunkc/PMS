using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace PMS
{
    public partial class ChangePassword : Page
    {
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string oldPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmNewPassword.Text;

            if (newPassword != confirmPassword)
            {
                lblErrorNewPassword.Visible = true;
                return;
            }

            string server = "localhost";
            string database = "pms";
            string uid = "root";
            string password = "password";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";


            // Get the connection string from Web.config
            //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Connect to the database
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Check if the username and old password are correct
                string query = "SELECT password FROM pms_user WHERE user_id = @Username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                string storedPasswordHash = (string)command.ExecuteScalar();

                if (storedPasswordHash == null || !VerifyPassword(oldPassword, storedPasswordHash))
                {
                    lblErrorCurrentPassword.Visible = true;
                    return;
                }

                // Update the password
                //string newPassword = HashPassword(newPassword);
                query = "UPDATE pms_user SET Password = @NewPassword WHERE user_id = @Username";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@NewPassword", newPassword);
                command.Parameters.AddWithValue("@Username", username);
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
            return enteredPassword == storedHash;
            //var enteredHash = HashPassword(enteredPassword);
            //return enteredHash.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
