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
			DateTime startDate = this.GetStartDate();      //report.GetSalesListingPostedDate(user.UserID, true);
			DateTime endDate = this.GetEndDate();          //report.GetSalesListingPostedDate(user.UserID, false);


			//For Sales Report
			this.lblSalesStartDate.Text = startDate.ToString("yyyy-MMM-dd");
            this.lblSalesEndDate.Text = endDate.ToString("yyyy-MMM-dd");
            this.lblSalesTotalListing.Text = report.GetSalesListingNumber(user.UserID, startDate, endDate).ToString();
            this.lblSalesTotalSales.Text = report.GetSalesNumber(user.UserID, true, startDate, endDate).ToString();
            this.lblSalesAvgPrice.Text = report.GetSalesPriceByPeriod(user.UserID, startDate, endDate).ToString("$#,###.##");

            //For Rent Report
            this.lblRentStartDate.Text = startDate.ToString("yyyy-MMM-dd");
            this.lblRentEndDate.Text = endDate.ToString("yyyy-MMM-dd");
            this.lblRentTotalListing.Text = report.GetRentListingNumber(user.UserID, startDate, endDate).ToString();
            this.lblRentTotalSales.Text = report.GetRentNumber(user.UserID, true, startDate, endDate).ToString();
            this.lblRentAvgPrice.Text = report.GetRentPriceByPeriod(user.UserID, startDate, endDate).ToString("$#,###.##");

        }

        public DateTime GetStartDate()
        {
            if (rbLastYear.Checked)
            {
                return DateTime.Now.AddYears(-1);
            }
            else if (rbLastMonth.Checked)
            {
                return DateTime.Now.AddMonths(-1);
            }
            else if (rbLastWeek.Checked)
            {
                return DateTime.Now.AddDays(-7);
            }
            else 
            {
                if (rbForSales.Checked)
                {
					return report.GetSalesListingPostedDate(user.UserID, true);
				}
                else
                {
					return report.GetRentListingPostedDate(user.UserID, true);
				}
				
			}

        }

        public DateTime GetEndDate()
        {
            if (rbAllTime.Checked)
            {
				if (rbForSales.Checked)
				{
					return report.GetSalesListingPostedDate(user.UserID, false);
				}
				else
				{
					return report.GetRentListingPostedDate(user.UserID, false);
				}
			}
            else 
            {
				return DateTime.Now;				
			}

        }

        // Table Value
        public string GetSalesTableValue()
        {
			DateTime startDate = this.GetStartDate();      
			DateTime endDate = this.GetEndDate();

			return "[" +
              $"['Total Sales',  '{report.GetSalesNumber(user.UserID, true, startDate, endDate)}', '{report.GetSalesPercentage(user.UserID, true, startDate, endDate).ToString("F1") + "%"}']," +
              $"['Active Listings', '{report.GetSalesNumber(user.UserID, false, startDate, endDate)}', '{report.GetSalesPercentage(user.UserID, false, startDate, endDate).ToString("F1") + "%"}']" +
                "]";
        }

        public string GetRentTableValue()
        {
			DateTime startDate = this.GetStartDate();
			DateTime endDate = this.GetEndDate();

			return "[" +
              $"['Total Rent',  '{report.GetRentNumber(user.UserID, true, startDate, endDate)}', '{report.GetRentPercentage(user.UserID, true, startDate, endDate).ToString("F1") + "%"}']," +
              $"['Active Listings', '{report.GetRentNumber(user.UserID, false, startDate, endDate)}', '{report.GetRentPercentage(user.UserID, false, startDate, endDate).ToString("F1") + "%"}']" +
                "]";
        }

        // Pie Chart
        public string GetSalesPieChart()
        {
			DateTime startDate = this.GetStartDate();
			DateTime endDate = this.GetEndDate();

			return "[" +
                "['Name', 'Value']," + 
                $"['Total Sales', {report.GetSalesNumber(user.UserID, true, startDate, endDate)}]," +
                $"['Active Listings', {report.GetSalesNumber(user.UserID, false, startDate, endDate)}]," + 
                "]";
        }

        public string GetRentPieChart()
        {
			DateTime startDate = this.GetStartDate();
			DateTime endDate = this.GetEndDate();

			return "[" +
                "['Name', 'Value']," +
                $"['Total Rent', {report.GetRentNumber(user.UserID, true, startDate, endDate)}]," +
                $"['Active Listings', {report.GetRentNumber(user.UserID, false, startDate, endDate)}]," +
                "]";
        }

        // Period Chart
        public string GetSalesPeriodChart()
        {
            string dateFormat = "yyyy MMM dd";

			//Get from posted date and to posted date
			DateTime startDate = this.GetStartDate();   //report.GetSalesListingPostedDate(user.UserID, true);
            DateTime endDate = this.GetEndDate();       // report.GetSalesListingPostedDate(user.UserID, false);

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
            string dateFormat = "yyyy MMM dd";

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
			DateTime startDate = this.GetStartDate(); 
			DateTime endDate = this.GetEndDate();     
			string chartValue = "[['Property Type', 'Value',],";

            DataTable dt = report.GetSalesByPropertyType(user.UserID, startDate, endDate);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chartValue += $"['{dt.Rows[i][0]}', {dt.Rows[i][1]}],";
            }

            chartValue += "]";

            return chartValue;
        }

        public string GetRentPropertyTypeChart()
        {
			DateTime startDate = this.GetStartDate();
			DateTime endDate = this.GetEndDate();
			string chartValue = "[['Property Type', 'Value',],";

            DataTable dt = report.GetRentByPropertyType(user.UserID, startDate, endDate);

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
            string dateFormat = "yyyy MMM dd";

            //Get from posted date and to posted date
            DateTime startDate = this.GetStartDate();   // report.GetSalesListingPostedDate(user.UserID, true);
            DateTime endDate = this.GetEndDate();       // report.GetSalesListingPostedDate(user.UserID, false);

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
            string dateFormat = "yyyy MMM dd";

			//Get from posted date and to posted date
			DateTime startDate = this.GetStartDate();   // report.GetRentListingPostedDate(user.UserID, true);
            DateTime endDate = this.GetEndDate();       // report.GetRentListingPostedDate(user.UserID, false);

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
            LoadReportType();
		}

        private void LoadReportType()
        {
			// hide or show panel according to selected report type
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


        protected void rbReportPeriod_CheckedChanged(object sender, EventArgs e)
        {
            //re-bind data when report period change
            bindData();

            //sales reports
            GetSalesPeriodChart();
            GetSalesPropertyTypeChart();
			GetSalesPriceChart();
            GetSalesPieChart();

            //rent reports
			GetRentPeriodChart();
			GetRentPropertyTypeChart();            
			GetRentPriceChart();
			GetRentPieChart();


		}
    }
}