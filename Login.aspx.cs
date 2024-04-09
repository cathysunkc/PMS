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
                Response.Redirect("Dashboard");
            }
        }        

        protected void LoginSubmit_Click(object sender, EventArgs e)
        {
            this.lblErrorNoLoginInfo.Visible = false;
            this.lblErrorLoginFail.Visible = false;

            string userID = this.txtLoginUserID.Text.Trim();
            string password = this.txtLoginPassword.Text.Trim();

            if (userID == string.Empty || password == string.Empty)
            {
                this.lblErrorNoLoginInfo.Visible = true;
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
				Session["UserID"] = null;
                Session["UserRole"] = null;
                this.lblErrorLoginFail.Visible = true;                
			}

			//Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);

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
	}
}