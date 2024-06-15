using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

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
                else 
                {
                    bindPostedDates();
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
        private void bindPostedDates()
        {
            this.lblFromDate.Text = report.GetPropertyPostedDate(user.UserID, true).ToString("yyyy-MMM-dd");
            this.lblToDate.Text = report.GetPropertyPostedDate(user.UserID, false).ToString("yyyy-MMM-dd");
        }

        public string GetSalesTableValue()
        {
            return "[" +
              $"['Total Sales',  '{report.GetSalesPropertyCount(user.UserID, true)}', '{report.GetSalesPropertyPercentage(user.UserID, true).ToString("F1") + "%"}']," +
              $"['Active Listings', '{report.GetSalesPropertyCount(user.UserID, false)}', '{report.GetSalesPropertyPercentage(user.UserID, false).ToString("F1") + "%"}']" +
                "]";
        }

        public string GetSalesPieChartValue()
        {
			return "[" +
                "['Name', 'Value']," + 
                $"['Total Sales', {report.GetSalesPropertyCount(user.UserID, true)}]," +
                $"['Active Listings', {report.GetSalesPropertyCount(user.UserID, false)}]," + 
                "]";
        }        

        public string GetSalesPeriodChartValue()
        {
            string dateFormat = "yyyy MMM dd";

            //Get from posted date and to posted date
            DateTime startDate = report.GetPropertyPostedDate(user.UserID, true);
            DateTime endDate = report.GetPropertyPostedDate(user.UserID, false);

            //Calculate the report intervals
            Double interval = (endDate - startDate).TotalDays;
            Double intervalStep = interval/6;

            //Interval 01
            DateTime startDate01 = startDate;
            DateTime endDate01 = startDate01.AddDays(intervalStep);
            string interval01 = startDate01.ToString(dateFormat) + " - " + endDate01.ToString(dateFormat);
            int count01 = report.GetSoldPropertyCountByDateRange(user.UserID, startDate01, endDate01);

            //Interval 02
            DateTime startDate02 = endDate01.AddDays(1);
            DateTime endDate02 = startDate02.AddDays(intervalStep);
            string interval02 = startDate02.ToString(dateFormat) + " - " + endDate02.ToString(dateFormat);
            int count02 = report.GetSoldPropertyCountByDateRange(user.UserID, startDate02, endDate02);

            //Interval 03
            DateTime startDate03 = endDate02.AddDays(1);
            DateTime endDate03 = startDate03.AddDays(intervalStep);
            string interval03 = startDate03.ToString(dateFormat) + " - " + endDate03.ToString(dateFormat);
            int count03 = report.GetSoldPropertyCountByDateRange(user.UserID, startDate03, endDate03);

            //Interval 04
            DateTime startDate04 = endDate03.AddDays(1);
            DateTime endDate04 = startDate04.AddDays(intervalStep);
            string interval04 = startDate04.ToString(dateFormat) + " - " + endDate04.ToString(dateFormat);
            int count04 = report.GetSoldPropertyCountByDateRange(user.UserID, startDate04, endDate04);

            //Interval 05
            DateTime startDate05 = endDate04.AddDays(1);
            DateTime endDate05 = endDate;
            string interval05 = startDate04.ToString(dateFormat) + " - " + endDate05.ToString(dateFormat);
            int count05 = report.GetSoldPropertyCountByDateRange(user.UserID, startDate04, endDate05);

            return "[['Date Range', 'Value']," + 
                $"['{interval01}', {count01}]," + 
                $"['{interval02}', {count02}]," +
                $"['{interval03}', {count03}]," +
                $"['{interval04}', {count04}]," +
                $"['{interval05}', {count05}]]";
        }

        public string GetPropertyTypeChartValue() 
        {
            string chartValue = "[['Property Type', 'Value',],";

            DataTable dt = report.GetSoldPropertyByPropertyType(user.UserID);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chartValue += $"['{dt.Rows[i][0]}', {dt.Rows[i][1]}],";
            }

            chartValue += "]";

            return chartValue;
        }

    }
}