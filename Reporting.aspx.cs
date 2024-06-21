using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
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
                    bindData();
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
        private void bindData()
        {
            //For Sales Report
            DateTime salesStartDate = report.GetSalesListingPostedDate(user.UserID, true);
            DateTime salesEndDate = report.GetSalesListingPostedDate(user.UserID, false);
            this.lblSalesStartDate.Text = salesStartDate.ToString("yyyy-MMM-dd");
            this.lblSalesEndDate.Text = salesEndDate.ToString("yyyy-MMM-dd");
            this.lblSalesTotalListing.Text = report.GetSalesListingNumber(user.UserID).ToString();
            this.lblSalesTotalSales.Text = report.GetSalesNumber(user.UserID, true).ToString();
            this.lblSalesAvgPrice.Text = report.GetSalesPriceByPeriod(user.UserID, salesStartDate, salesEndDate).ToString("$#,###.##");

            //For Rent Report
            DateTime rentStartDate = report.GetRentListingPostedDate(user.UserID, true);
            DateTime rentEndDate = report.GetRentListingPostedDate(user.UserID, false);
            this.lblRentStartDate.Text = rentStartDate.ToString("yyyy-MMM-dd");
            this.lblRentEndDate.Text = rentEndDate.ToString("yyyy-MMM-dd");
            this.lblRentTotalListing.Text = report.GetRentListingNumber(user.UserID).ToString();
            this.lblRentTotalSales.Text = report.GetRentNumber(user.UserID, true).ToString();
            this.lblRentAvgPrice.Text = report.GetRentPriceByPeriod(user.UserID, rentStartDate, rentEndDate).ToString("$#,###.##");

        }

        // Table Value
        public string GetSalesTableValue()
        {
            return "[" +
              $"['Total Sales',  '{report.GetSalesNumber(user.UserID, true)}', '{report.GetSalesPercentage(user.UserID, true).ToString("F1") + "%"}']," +
              $"['Active Listings', '{report.GetSalesNumber(user.UserID, false)}', '{report.GetSalesPercentage(user.UserID, false).ToString("F1") + "%"}']" +
                "]";
        }

        public string GetRentTableValue()
        {
            return "[" +
              $"['Total Rent',  '{report.GetRentNumber(user.UserID, true)}', '{report.GetRentPercentage(user.UserID, true).ToString("F1") + "%"}']," +
              $"['Active Listings', '{report.GetRentNumber(user.UserID, false)}', '{report.GetRentPercentage(user.UserID, false).ToString("F1") + "%"}']" +
                "]";
        }

        // Pie Chart
        public string GetSalesPieChart()
        {
			return "[" +
                "['Name', 'Value']," + 
                $"['Total Sales', {report.GetSalesNumber(user.UserID, true)}]," +
                $"['Active Listings', {report.GetSalesNumber(user.UserID, false)}]," + 
                "]";
        }

        public string GetRentPieChart()
        {
            return "[" +
                "['Name', 'Value']," +
                $"['Total Rent', {report.GetRentNumber(user.UserID, true)}]," +
                $"['Active Listings', {report.GetRentNumber(user.UserID, false)}]," +
                "]";
        }

        // Period Chart
        public string GetSalesPeriodChart()
        {
            string dateFormat = "yyyy MMM";

            //Get from posted date and to posted date
            DateTime startDate = report.GetSalesListingPostedDate(user.UserID, true);
            DateTime endDate = report.GetSalesListingPostedDate(user.UserID, false);

            //Calculate the report intervals
            Double interval = (endDate - startDate).TotalDays;
            Double intervalStep = interval/6;

            //Interval 01
            DateTime startDate01 = startDate;
            DateTime endDate01 = startDate01.AddDays(intervalStep);
            string interval01 = startDate01.ToString(dateFormat) + " - " + endDate01.ToString(dateFormat);
            int count01 = report.GetSalesByPeriod(user.UserID, startDate01, endDate01);

            //Interval 02
            DateTime startDate02 = endDate01.AddDays(1);
            DateTime endDate02 = startDate02.AddDays(intervalStep);
            string interval02 = startDate02.ToString(dateFormat) + " - " + endDate02.ToString(dateFormat);
            int count02 = report.GetSalesByPeriod(user.UserID, startDate02, endDate02);

            //Interval 03
            DateTime startDate03 = endDate02.AddDays(1);
            DateTime endDate03 = startDate03.AddDays(intervalStep);
            string interval03 = startDate03.ToString(dateFormat) + " - " + endDate03.ToString(dateFormat);
            int count03 = report.GetSalesByPeriod(user.UserID, startDate03, endDate03);

            //Interval 04
            DateTime startDate04 = endDate03.AddDays(1);
            DateTime endDate04 = startDate04.AddDays(intervalStep);
            string interval04 = startDate04.ToString(dateFormat) + " - " + endDate04.ToString(dateFormat);
            int count04 = report.GetSalesByPeriod(user.UserID, startDate04, endDate04);

            //Interval 05
            DateTime startDate05 = endDate04.AddDays(1);
            DateTime endDate05 = endDate;
            string interval05 = startDate05.ToString(dateFormat) + " - " + endDate05.ToString(dateFormat);
            int count05 = report.GetSalesByPeriod(user.UserID, startDate05, endDate05);

            return "[['Date Range', 'Value']," + 
                $"['{interval01}', {count01}]," + 
                $"['{interval02}', {count02}]," +
                $"['{interval03}', {count03}]," +
                $"['{interval04}', {count04}]," +
                $"['{interval05}', {count05}]]";
        }

        public string GetRentPeriodChart()
        {
            string dateFormat = "yyyy MMM";

            //Get from posted date and to posted date
            DateTime startDate = report.GetRentListingPostedDate(user.UserID, true);
            DateTime endDate = report.GetRentListingPostedDate(user.UserID, false);

            //Calculate the report intervals
            Double interval = (endDate - startDate).TotalDays;
            Double intervalStep = interval / 6;

            //Interval 01
            DateTime startDate01 = startDate;
            DateTime endDate01 = startDate01.AddDays(intervalStep);
            string interval01 = startDate01.ToString(dateFormat) + " - " + endDate01.ToString(dateFormat);
            int count01 = report.GetRentByPeriod(user.UserID, startDate01, endDate01);

            //Interval 02
            DateTime startDate02 = endDate01.AddDays(1);
            DateTime endDate02 = startDate02.AddDays(intervalStep);
            string interval02 = startDate02.ToString(dateFormat) + " - " + endDate02.ToString(dateFormat);
            int count02 = report.GetRentByPeriod(user.UserID, startDate02, endDate02);

            //Interval 03
            DateTime startDate03 = endDate02.AddDays(1);
            DateTime endDate03 = startDate03.AddDays(intervalStep);
            string interval03 = startDate03.ToString(dateFormat) + " - " + endDate03.ToString(dateFormat);
            int count03 = report.GetRentByPeriod(user.UserID, startDate03, endDate03);

            //Interval 04
            DateTime startDate04 = endDate03.AddDays(1);
            DateTime endDate04 = startDate04.AddDays(intervalStep);
            string interval04 = startDate04.ToString(dateFormat) + " - " + endDate04.ToString(dateFormat);
            int count04 = report.GetRentByPeriod(user.UserID, startDate04, endDate04);

            //Interval 05
            DateTime startDate05 = endDate04.AddDays(1);
            DateTime endDate05 = endDate;
            string interval05 = startDate05.ToString(dateFormat) + " - " + endDate05.ToString(dateFormat);
            int count05 = report.GetRentByPeriod(user.UserID, startDate05, endDate05);

            return "[['Date Range', 'Value']," +
                $"['{interval01}', {count01}]," +
                $"['{interval02}', {count02}]," +
                $"['{interval03}', {count03}]," +
                $"['{interval04}', {count04}]," +
                $"['{interval05}', {count05}]]";
        }

        // Property Type Chart
        public string GetSalesPropertyTypeChart() 
        {
            string chartValue = "[['Property Type', 'Value',],";

            DataTable dt = report.GetSalesByPropertyType(user.UserID);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chartValue += $"['{dt.Rows[i][0]}', {dt.Rows[i][1]}],";
            }

            chartValue += "]";

            return chartValue;
        }

        public string GetRentPropertyTypeChart()
        {
            string chartValue = "[['Property Type', 'Value',],";

            DataTable dt = report.GetRentByPropertyType(user.UserID);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chartValue += $"['{dt.Rows[i][0]}', {dt.Rows[i][1]}],";
            }

            chartValue += "]";

            return chartValue;
        }

        // Price Chart
        public string GetSalesPriceChart()
        {
            string dateFormat = "yyyy MMM";

            //Get from posted date and to posted date
            DateTime startDate = report.GetSalesListingPostedDate(user.UserID, true);
            DateTime endDate = report.GetSalesListingPostedDate(user.UserID, false);

            //Calculate the report intervals
            Double interval = (endDate - startDate).TotalDays;
            Double intervalStep = interval / 6;

            //Interval 01
            DateTime startDate01 = startDate;
            DateTime endDate01 = startDate01.AddDays(intervalStep);
            string interval01 = startDate01.ToString(dateFormat) + " - " + endDate01.ToString(dateFormat);
            double price01 = report.GetSalesPriceByPeriod(user.UserID, startDate01, endDate01);

            //Interval 02
            DateTime startDate02 = endDate01.AddDays(1);
            DateTime endDate02 = startDate02.AddDays(intervalStep);
            string interval02 = startDate02.ToString(dateFormat) + " - " + endDate02.ToString(dateFormat);
            double price02 = report.GetSalesPriceByPeriod(user.UserID, startDate02, endDate02);

            //Interval 03
            DateTime startDate03 = endDate02.AddDays(1);
            DateTime endDate03 = startDate03.AddDays(intervalStep);
            string interval03 = startDate03.ToString(dateFormat) + " - " + endDate03.ToString(dateFormat);
            double price03 = report.GetSalesPriceByPeriod(user.UserID, startDate03, endDate03);

            //Interval 04
            DateTime startDate04 = endDate03.AddDays(1);
            DateTime endDate04 = startDate04.AddDays(intervalStep);
            string interval04 = startDate04.ToString(dateFormat) + " - " + endDate04.ToString(dateFormat);
            double price04 = report.GetSalesPriceByPeriod(user.UserID, startDate04, endDate04);

            //Interval 05
            DateTime startDate05 = endDate04.AddDays(1);
            DateTime endDate05 = endDate;
            string interval05 = startDate05.ToString(dateFormat) + " - " + endDate05.ToString(dateFormat);
            double price05 = report.GetSalesPriceByPeriod(user.UserID, startDate05, endDate05);

            return "[['Date Range', 'Value']," +
                $"['{interval01}', {price01}]," +
                $"['{interval02}', {price02}]," +
                $"['{interval03}', {price03}]," +
                $"['{interval04}', {price04}]," +
                $"['{interval05}', {price05}]]";
        }

        public string GetRentPriceChart()
        {
            string dateFormat = "yyyy MMM";

            //Get from posted date and to posted date
            DateTime startDate = report.GetRentListingPostedDate(user.UserID, true);
            DateTime endDate = report.GetRentListingPostedDate(user.UserID, false);

            //Calculate the report intervals
            Double interval = (endDate - startDate).TotalDays;
            Double intervalStep = interval / 6;

            //Interval 01
            DateTime startDate01 = startDate;
            DateTime endDate01 = startDate01.AddDays(intervalStep);
            string interval01 = startDate01.ToString(dateFormat) + " - " + endDate01.ToString(dateFormat);
            double price01 = report.GetRentPriceByPeriod(user.UserID, startDate01, endDate01);

            //Interval 02
            DateTime startDate02 = endDate01.AddDays(1);
            DateTime endDate02 = startDate02.AddDays(intervalStep);
            string interval02 = startDate02.ToString(dateFormat) + " - " + endDate02.ToString(dateFormat);
            double price02 = report.GetRentPriceByPeriod(user.UserID, startDate02, endDate02);

            //Interval 03
            DateTime startDate03 = endDate02.AddDays(1);
            DateTime endDate03 = startDate03.AddDays(intervalStep);
            string interval03 = startDate03.ToString(dateFormat) + " - " + endDate03.ToString(dateFormat);
            double price03 = report.GetRentPriceByPeriod(user.UserID, startDate03, endDate03);

            //Interval 04
            DateTime startDate04 = endDate03.AddDays(1);
            DateTime endDate04 = startDate04.AddDays(intervalStep);
            string interval04 = startDate04.ToString(dateFormat) + " - " + endDate04.ToString(dateFormat);
            double price04 = report.GetRentPriceByPeriod(user.UserID, startDate04, endDate04);

            //Interval 05
            DateTime startDate05 = endDate04.AddDays(1);
            DateTime endDate05 = endDate;
            string interval05 = startDate05.ToString(dateFormat) + " - " + endDate05.ToString(dateFormat);
            double price05 = report.GetRentPriceByPeriod(user.UserID, startDate05, endDate05);

            return "[['Date Range', 'Value']," +
                $"['{interval01}', {price01}]," +
                $"['{interval02}', {price02}]," +
                $"['{interval03}', {price03}]," +
                $"['{interval04}', {price04}]," +
                $"['{interval05}', {price05}]]";
        }

        protected void rbReportType_CheckedChanged(object sender, EventArgs e)
        {
            if (rbForRent.Checked == true)
            {
                this.panelSalesReport.Visible = false;
                this.panelRentReport.Visible = true;
            }
            else
            {
                this.panelSalesReport.Visible = true;
                this.panelRentReport.Visible = false;
            }

        }
    }
}