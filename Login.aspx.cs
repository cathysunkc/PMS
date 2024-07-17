using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                Response.Redirect("Profile");
            }
        }

        protected void LoginSubmit_Click(object sender, EventArgs e)
        {
            lblErrorNoLoginInfo.Visible = false;
            lblErrorLoginFail.Visible = false;

            string userID = txtLoginUserID.Text.Trim();
            string password = txtLoginPassword.Text.Trim();

            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password))
            {
                lblErrorNoLoginInfo.Visible = true;
                return;
            }


            //if ((userID == "client01" || userID == "realtor01") && password == "password")
            User user = new User();
            user = user.Login(userID, password);

            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["UserRole"] = user.Role;
                Response.Redirect("Default");

            }

            else
            {
                User failedUser = new User().GetUserByID(userID);
                if (failedUser != null && failedUser.IsLocked)
                {
                    lblErrorLoginFail.Text = "Your account is locked. Please try again after 2 minutes.";
                }
                else
                {
                    lblErrorLoginFail.Text = "Invalid Login ID or Password.";
                }
                lblErrorLoginFail.Visible = true;
            }

            //Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);

        }

        protected void RegisterLink_Click(object sender, EventArgs e)
        {
            panelRegister.Visible = true;
            panelLogin.Visible = false;
        }

        protected void LoginLink_Click(object sender, EventArgs e)
        {
            panelRegister.Visible = false;
            panelLogin.Visible = true;
        }



        //

        protected void RegisterSubmit_Click(object sender, EventArgs e)
        {
            string userID = txtRegisterUserID.Text.Trim();
            string password = txtRegisterPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string firstName = txtRegisterFirstName.Text.Trim();
            string lastName = txtRegisterLastName.Text.Trim();
            string email = txtRegisterEmail.Text.Trim();
            string phone = txtRegisterPhone.Text.Trim();
            string role = rblRegisterRole.SelectedValue;

            // Hide all error messages initially
            lblErrorRegisterFieldsRequired.Visible = false;
            lblErrorRegisterFail.Visible = false;
            lblErrorPasswordMismatch.Visible = false;
            lblErrorRoleRequired.Visible = false;

            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) ||
                string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(role) || role == "")
            {
                if (string.IsNullOrEmpty(role) || role == "")
                {
                    lblErrorRoleRequired.Visible = true;
                }
                else
                {
                // Show error message that all fields are required
                    lblErrorRegisterFieldsRequired.Visible = true;
                }
                return;
            }



            if (password != confirmPassword)
            {
                // Show error message that passwords do not match
                lblErrorPasswordMismatch.Visible = true;
                return;
            }

            try
            {
                User user = new User(userID, password, firstName, lastName, email, phone, role);
                DB db = new DB();

                if (db.AddUser(user))
                {
                    // Registration successful, redirect to the home page
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    // Show error message that registration failed
                    lblErrorRegisterFail.Visible = true;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                if (ex.Number == 1062) // Error code for duplicate entry
                {
                    // Show error message that the user ID already exists
                    lblErrorRegisterFail.Text = "User ID already exists. Please choose a different User ID.";
                    lblErrorRegisterFail.Visible = true;
                }
            }

      
        }










    }
}