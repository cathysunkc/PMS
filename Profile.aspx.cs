using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS
{
    public partial class Profile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                User user = new User();
                user = user.GetUserByID(Session["UserID"].ToString());

                if (user.IsRealtor())
                {
                    this.panelListing.Visible = true;
                    this.lblProfileNote.Text = "Realtor Profile";
                }
                else if (user.IsClient())
                {
                    this.panelListing.Visible = false;
                    this.lblProfileNote.Text = "Client Profile";
                }

                this.lblUserID.Text = user.UserID;
                this.lblFullName.Text = user.GetFullName();
                this.lblEmail.Text = user.Email;
            }
            else
            {
                Response.Redirect("Login");
            }
        }

        protected void AddProperty_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProperty");
        }


        protected void UpdateUser_Click(object sender, EventArgs e)
        {
            string userID = txtUpdateUserID.Text.Trim();
            string password = txtUpdatePassword.Text.Trim();
            string firstName = txtUpdateFirstName.Text.Trim();
            string lastName = txtUpdateLastName.Text.Trim();
            string email = txtUpdateEmail.Text.Trim();
            string phone = txtUpdatePhone.Text.Trim();
            string role = rblUpdateRole.SelectedValue;

            lblErrorUpdateFieldsRequired.Visible = false;
            lblErrorUpdateFail.Visible = false;
            lblUpdateSuccess.Visible = false;

            // Validate inputs (example for validation, adapt as needed)
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(role))
            {
                lblErrorUpdateFieldsRequired.Visible = true;
                return;
            }

            User user = new User(userID, password, firstName, lastName, email, phone, role);
            DB db = new DB();

            if (db.UpdateUser(user))
            {
                lblUpdateSuccess.Visible = true;  // Display success message
            }
            else
            {
                lblErrorUpdateFail.Visible = true;  // Display error message
            }
        }


        protected void UpdateProfileLink_Click(object sender, EventArgs e)
        {
            panelUpdate.Visible = true;
      

            // Prepopulate the update form with existing user details if available
            string userID = Session["UserID"].ToString();
            User user = new User();
            user = user.GetUserDetails(userID);

            if (user != null)
            {
                txtUpdateUserID.Text = user.UserID;
                txtUpdatePassword.Text = user.Password;
                txtUpdateFirstName.Text = user.FirstName;
                txtUpdateLastName.Text = user.LastName;
                txtUpdateEmail.Text = user.Email;
                txtUpdatePhone.Text = user.Phone;
                rblUpdateRole.SelectedValue = user.Role;
            }
        }



    }

}