using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS
{
    public partial class Listing : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                User user = new User();
                user = user.GetUserByID(Session["UserID"].ToString());

                if (user.IsRealtor())
                {
                    this.panelAddListing.Visible = true;
                }


            }

            if (!this.IsPostBack)
            {
                BindDropDownList();
                BindSortType();
                BindListing();
            }

        }

        private void BindDropDownList()
        {
            var transactionType = new Dictionary<char, string>
            {
                { ' ', "Transaction Type" },
                { 'S', "For Sale" },
                { 'R', "For Rent" }
            };
            ddlTransactionType.DataSource = transactionType;
            ddlTransactionType.DataTextField = "Value";
            ddlTransactionType.DataValueField = "Key";
            ddlTransactionType.DataBind();

            var bedNum = new Dictionary<double, string>
            {
                { 0, "Beds" },
                { 1, "1" },
                { 2, "2" },
                { 3, "3" },
                { 4, "4" },
            };
            ddlBedNum.DataSource = bedNum;
            ddlBedNum.DataTextField = "Value";
            ddlBedNum.DataValueField = "Key";
            ddlBedNum.DataBind();

            var bathNum = new Dictionary<double, string>
            {
                { 0, "Baths" },
                { 1, "1" },
                { 2, "2" },
                { 3, "3" },
                { 4, "4" },
            };
            ddlBathNum.DataSource = bathNum;
            ddlBathNum.DataValueField = "Key";
            ddlBathNum.DataTextField = "Value";
            ddlBathNum.DataBind();

        }

        private void BindSortType()
        {
            var sortType = new Dictionary<double, string>
            {
                { 0, "Newest" },
                { 1, "Oldest" },
                { 2, "Lowest Price" },
                { 3, "Highest Price" },
            };
            ddlSortType.DataSource = sortType;
            ddlSortType.DataValueField = "Key";
            ddlSortType.DataTextField = "Value";
            ddlSortType.DataBind();
        }

        private void BindListing()
        {
            DataSet ds = new DataSet();
            DB db = new DB();

            char transactionType = Convert.ToChar(ddlTransactionType.SelectedValue);
            double bedNum = Convert.ToDouble(ddlBedNum.SelectedValue);
            double bathNum = Convert.ToDouble(ddlBathNum.SelectedValue);
            DataTable dt = Property.FindProperty(db, transactionType, bedNum, bathNum);

            if (dt.Rows.Count == 0)
                this.lblNoPropertyFound.Visible = true;
            else
                this.lblNoPropertyFound.Visible = false;

            //check sorting
            if (ddlSortType.SelectedIndex == 1)
                dt.DefaultView.Sort = "posted_date ASC";
            else if (ddlSortType.SelectedIndex == 2)
                dt.DefaultView.Sort = "price ASC";
            else if (ddlSortType.SelectedIndex == 3)
                dt.DefaultView.Sort = "price DESC";
            else 
                dt.DefaultView.Sort = "posted_date DESC"; //default            

            dt.AcceptChanges();
            //ds.Tables.Add(dt);           

            listProperty.DataSource = dt;
            listProperty.DataBind();
        }

        protected void TransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListing();
        }

        protected void BedNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListing();
        }

        protected void BathNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListing();
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            ddlTransactionType.SelectedIndex = 0;
            ddlBedNum.SelectedIndex = 0;
            ddlBathNum.SelectedIndex = 0;
            ddlSortType.SelectedIndex = 0;
            BindListing();
        }

        protected void AddProperty_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProperty");
        }

        protected void listProperty_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (this.listProperty.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.BindListing();
        }

        protected void ddlSortType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListing();
        }
    }
}