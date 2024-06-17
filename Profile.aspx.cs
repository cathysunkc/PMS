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
    }
}