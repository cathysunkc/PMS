using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS
{
    public partial class SiteMaster : MasterPage
    {
		User user = new User();

        protected void Page_Load(object sender, EventArgs e)
        {
			OnLoad();
		}

		public void OnLoad()
		{
			if (Session["UserID"] != null)
			{
				user = user.GetUserByID(Session["UserID"].ToString());

                this.lblWelcome.Visible = true;
				this.lblWelcome.Text = "Welcome, " + user.GetFullName();

				this.lnkProfile.Visible = true;
				this.lnkMessage.Visible = true;
				if (Message.AlertMessage(Session["UserID"].ToString()) != 0)  
				{ 
					this.lblMsgCount.Text = "(" + Message.AlertMessage(Session["UserID"].ToString()) + ")"; 
				}
                

                if (user.IsRealtor())
				{
					this.lnkReporting.Visible = true;
				}
				
				this.lbnLogin.Text = "Logout";

			}
			else
			{
				this.lblWelcome.Visible = false;
				this.lblWelcome.Text = string.Empty;
				this.lnkProfile.Visible = false;
				this.lnkMessage.Visible = false;
				this.lnkReporting.Visible = false;
				this.lbnLogin.Text = "Login";
			}
		}

		protected void Login_Click(object sender, EventArgs e)
		{
			
            if (Session["UserID"] != null)
			{
				Session["UserID"] = null;
				Session["UserRole"] = null;
                OnLoad();
                Response.Redirect("Default");			
            }
			else
			{
                OnLoad();
                Response.Redirect("Login");
			}
		}
    }
}