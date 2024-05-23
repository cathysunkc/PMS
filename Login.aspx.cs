using System;
using System.Data;
using System.Web;
using System.Web.UI;
using PMS;

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

            User user = new User();
            user = user.Login(userID, password);

            if (user != null)
            {
                Session["UserID"] = user.UserID;
                Session["UserRole"] = user.Role;
                Response.Redirect("Default.aspx");
            }
            else
            {
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

        protected void RegisterSubmit_Click(object sender, EventArgs e)
{
    string userID = txtRegisterUserID.Text.Trim();
    string password = txtRegisterPassword.Text.Trim();
    string confirmPassword = txtConfirmPassword.Text.Trim();
    string firstName = txtRegisterFirstName.Text.Trim();
    string lastName = txtRegisterLastName.Text.Trim();
    string email = txtRegisterEmail.Text.Trim();
    string phone = txtRegisterPhone.Text.Trim();
    string role = txtRegisterRole.Text.Trim();

    // Hide all error messages initially
    lblErrorRegisterFieldsRequired.Visible = false;
    lblErrorRegisterFail.Visible = false;
    lblErrorPasswordMismatch.Visible = false;




    if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) ||
        string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) ||
        string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(role))
    {
        // Show error message that all fields are required
        lblErrorRegisterFieldsRequired.Visible = true;
        return;
    }



    if (password != confirmPassword)
    {
        // Show error message that passwords do not match
        lblErrorPasswordMismatch.Visible = true;
        return;
    }
    // I used Chatgpt to help with the logic 
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
        else
        {
            // Log the exception (consider using a logging framework)
            // Show a user-friendly error message
            lblErrorRegisterFail.Text = "An error occurred during registration. Please contact support.";
            lblErrorRegisterFail.Visible = true;

            // Optionally, log the detailed exception message for debugging purposes
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

}
    }
}
