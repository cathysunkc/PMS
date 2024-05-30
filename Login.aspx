using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace PMS
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                Response.Redirect("Dashboard");
            }
        }

        protected void LoginSubmit_Click(object sender, EventArgs e)
        {
            this.lblErrorNoLoginInfo.Visible = false;
            this.lblErrorLoginFail.Visible = false;

            string userID = this.txtLoginUserID.Text.Trim();
            string password = this.txtLoginPassword.Text.Trim();

            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password))
            {
                this.lblErrorNoLoginInfo.Visible = true;
                return;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT password, role FROM pms_user WHERE user_id = @userID";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userID", userID);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string storedPasswordHash = reader["password"].ToString();
                    string role = reader["role"].ToString();

                    if (VerifyPassword(password, storedPasswordHash))
                    {
                        Session["UserID"] = userID;
                        Session["UserRole"] = role;
                        Response.Redirect("Default");
                        return;
                    }
                }

                Session["UserID"] = null;
                Session["UserRole"] = null;
                this.lblErrorLoginFail.Visible = true;
            }
        }

        protected void RegisterLink_Click(object sender, EventArgs e)
        {
            this.panelRegister.Visible = true;
            this.panelLogin.Visible = false;
        }

        protected void LoginLink_Click(object sender, EventArgs e)
        {
            this.panelRegister.Visible = false;
            this.panelLogin.Visible = true;
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
