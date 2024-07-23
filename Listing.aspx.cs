using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Script.Serialization;
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
                if (Session["UserID"] != null)
                {
                    LoadSavedSearches();
                }
            }

        }

        /************************************
         * SaveSearch
        /************************************/
        private void LoadSavedSearches()
        {
            string userID = Session["UserID"]?.ToString();
            if (string.IsNullOrEmpty(userID))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            DB db = new DB();
            DataTable dt = db.LoadSavedSearches(userID);

            if (dt.Rows.Count > 0)
            {
                ddlSavedSearches.DataSource = dt;
                ddlSavedSearches.DataTextField = "search_name"; //text to shown in ddl
                ddlSavedSearches.DataValueField = "search_name"; // value for the field
                ddlSavedSearches.DataBind();
            }

            ddlSavedSearches.Items.Insert(0, new ListItem("Select a saved search", ""));
        }

        protected void btnSaveSearch_Click(object sender, EventArgs e)
        {
            // Debug message
            lblDebug.Text = "Save button clicked";

            string userID = Session["UserID"]?.ToString();
            if (string.IsNullOrEmpty(userID))
            {
                lblDebug.Text = "User is not logged in.";
                return;
            }

            DB db = new DB();
            int lastSearchName = db.GetLastSearchName(userID);
            int newSearchName = lastSearchName + 1;
            string searchName = newSearchName.ToString();

            string transactionType = ddlTransactionType.SelectedValue; // Get selected value from transaction type dropdown
            string bedNum = ddlBedNum.SelectedValue; // Get selected value from bed number dropdown
            string bathNum = ddlBathNum.SelectedValue; // Get selected value from bath number dropdown

            // Create an object to hold the search criteria
            var searchCriteriaObj = new
            {
                transactionType,
                bedNum,
                bathNum
            };

            // https://csharp.hotexamples.com/examples/MySql.Data.MySqlClient/MySqlCommand/-/php-mysqlcommand-class-examples.html\
            //https://stackoverflow.com/questions/21110001/sqlcommand-parameters-add-vs-addwithvalue
            // Serialize the search criteria object to JSON
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string searchCriteria = serializer.Serialize(searchCriteriaObj);

            // Debug message
            lblDebug.Text += $" | Search Criteria: {searchCriteria}";

            db.SaveSearch(userID, searchName, searchCriteria);

            // Update the last search name
            db.UpdateLastSearchName(userID, newSearchName);

            // Confirm save
            lblDebug.Text += " | Search saved successfully.";

            LoadSavedSearches();
        }

        protected void ddlSavedSearches_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDebug.Text = "Selected saved search changed"; // Debug message

            if (!string.IsNullOrEmpty(ddlSavedSearches.SelectedValue))
            {
                string searchName = ddlSavedSearches.SelectedValue;
                DB db = new DB();
                dynamic searchCriteria = db.GetSavedSearchCriteriaByName(searchName);

                if (searchCriteria != null)
                {
                    ddlTransactionType.SelectedValue = searchCriteria["transactionType"];
                    ddlBedNum.SelectedValue = searchCriteria["bedNum"].ToString();
                    ddlBathNum.SelectedValue = searchCriteria["bathNum"].ToString();

                    BindListing();
                }
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
            //reset dropdownlist
            ddlTransactionType.SelectedIndex = 0;
            ddlBedNum.SelectedIndex = 0;
            ddlBathNum.SelectedIndex = 0;
            ddlSortType.SelectedIndex = 0;

            //reset paging
            DataPager pager = listProperty.FindControl("DataPager1") as DataPager;
            pager.SetPageProperties(0, pager.PageSize, true);
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