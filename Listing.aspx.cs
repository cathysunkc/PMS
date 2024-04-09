using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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

        private void BindListing()
        {   
            DataSet ds = new DataSet();

            char transactionType = Convert.ToChar(ddlTransactionType.SelectedValue);
            double bedNum = Convert.ToDouble(ddlBedNum.SelectedValue);
            double bathNum = Convert.ToDouble(ddlBathNum.SelectedValue);
            Property property = new Property();
            DataTable dt = property.FindProperty(transactionType, bedNum, bathNum);

            if (dt.Rows.Count == 0)
                this.lblNoPropertyFound.Visible = true;
            else
                this.lblNoPropertyFound.Visible = false;

            ds.Tables.Add(dt);
            listProperty.DataSource = ds;
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
            BindListing();
        }

        protected void AddProperty_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProperty");
        }
    }
}