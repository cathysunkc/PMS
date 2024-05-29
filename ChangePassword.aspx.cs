using System;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;

namespace PMS
{
    public partial class ChangePassword : Page
    {
        protected void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string oldPassword = OldPasswordTextBox.Text;
            string newPassword = NewPasswordTextBox.Text;
            string confirmPassword = ConfirmPasswordTextBox.Text;

            if (newPassword != confirmPassword)
            {
                // Show error message: new passwords do not match
                Response.Write("<script>alert('New passwords do not match.');</script>");
                return;
            }

            // Connect to the database
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the username and old password are correct
                string query = "SELECT Password FROM Users WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                string storedPasswordHash = (string)command.ExecuteScalar();

                if (storedPasswordHash == null || !FormsAuthentication.HashPasswordForStoringInConfigFile(oldPassword, "SHA1").Equals(storedPasswordHash))
                {
                    // Show error message: invalid username or old password
                    Response.Write("<script>alert('Invalid username or old password.');</script>");
                    return;
                }

                // Update the password
                string newPasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "SHA1");
                query = "UPDATE Users SET Password = @NewPassword WHERE Username = @Username";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NewPassword", newPasswordHash);
                command.Parameters.AddWithValue("@Username", username);
                command.ExecuteNonQuery();

                // Show success message
                Response.Write("<script>alert('Password successfully changed.');</script>");
            }
        }
    }
}
