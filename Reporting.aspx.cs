using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS
{
    public partial class Reporting : Page
    {
        Report report = new Report();
		User user = new User();

		protected void Page_Load(object sender, EventArgs e)
        {
            // Restrict only user with "Realtor" role can view Reporting
            if (Session["UserID"] != null)            {
                
                user = user.GetUserByID(Session["UserID"].ToString());

                if (!user.IsRealtor())
                {
                    RedirectToLogin();
                }                
            }            
            else
            {
                RedirectToLogin();
            }
        }

        public void RedirectToLogin()
        {
            Response.Redirect("Login");
        }

        public string GetPieChartValue()
        {
			return "[" +
                "['Sold', " + report.GetSoldPropertyCount(user.UserID) + "]," +
                "['Unsold', 12]" + 
                "]";
        }

        public string GetTableValue()
        {
            return "[" +
          "['Sold',  8, '40%']," +
          "['Unsold', 12, '60%']" +
            "]";
        }
        
    }
}