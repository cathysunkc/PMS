using MySqlX.XDevAPI.Relational;
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
    public partial class ContactRealtor : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] != null)
            {
                User user = new User();
                user = user.GetUserByID(Session["UserID"].ToString());

                if (user.IsRealtor())
                {
                    this.lblNote.Text = "Message to Client";
                    this.lblRecipientEmail.Text = "Client Email:";
                }
                else if (user.IsClient())
                {
                    this.lblNote.Text = "Message to Realtor";
                    this.lblRecipientEmail.Text = "Realtor Email:";
                }

                this.txtSenderEmail.Text = user.Email;

                string propertyID = Request.QueryString["property_id"];

                if (propertyID != null)
                {
                    
                    sendMessage.Value = propertyID;
                    BindMessageGrid(propertyID);

                    this.panelSelectMessageNull.Visible = false;
                    this.panelSelectMessage.Visible = true;

                    /* edited by Wilson 
                    //User realtor = new User();
                    //realtor = realtor.GetUserByID(realtorID);

                    if (realtor != null)
                    {
                        this.txtRecipientEmail.Text = realtor.Email;
                        this.txtRecipientEmail.ReadOnly = true;
                    }
                    */
                }

                if (!this.IsPostBack)
                {
                    BindDropDownList();
                }
                BindMessageList();
            }
            else
            {
                Response.Redirect("Login");
            }
        }

        // Edited by Wilson
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

        protected void TransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindMessageList();
        }

        protected void BedNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindMessageList();
        }

        protected void BathNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindMessageList();
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            //reset dropdownlist
            ddlTransactionType.SelectedIndex = 0;
            ddlBedNum.SelectedIndex = 0;
            ddlBathNum.SelectedIndex = 0;

            BindMessageList();
        }


        private void BindMessageList()
        {
            char transactionType = Convert.ToChar(ddlTransactionType.SelectedValue);
            double bedNum = Convert.ToDouble(ddlBedNum.SelectedValue);
            double bathNum = Convert.ToDouble(ddlBathNum.SelectedValue);
            DataTable dt = Message.GetMessageByUserIDAndPref((String)Session["UserID"], transactionType, bedNum, bathNum);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            listMessage.DataSource = ds;
            listMessage.DataBind();

        }

        private void BindMessageGrid(string propertyID)
        {
            DataSet ds = new DataSet();
            DataTable dt = Message.GetMessageByUserIDAndPropID((String)Session["UserID"], propertyID);
            ds.Tables.Add(dt);

            if (dt.Rows.Count == 0)
            {
                this.gridMessageNull.Visible = true;
                this.gridMessageNull.Text = "Please enter message to communicate for " + Property.GetPropertyByID(sendMessage.Value).Address;
            }

            gridMessage.DataSource = ds;
            gridMessage.DataBind();

        }

        protected void Message_Click(Object sender, CommandEventArgs e)
        {
            // e.CommandArgument == PropertyID
            BindMessageGrid((String)e.CommandArgument);

            this.panelSelectMessageNull.Visible = false;
            this.panelSelectMessage.Visible = true;
            sendMessage.Value = (String)e.CommandArgument;

            DB dB = new DB();
            dB.UpdateMessageClicked((String)Session["UserID"], (String)e.CommandArgument);
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            // sendMessage.Value == PropertyID
            Message message = new Message((String)Session["UserID"], Property.GetPropertyByID(sendMessage.Value).RealtorID, sendMessage.Value,
    DateTime.Now.ToString("yyyy'-'MM'-'dd"), true, this.txtMessage.Text, false);
            message.SendMessage(message);

            BindMessageGrid(sendMessage.Value);
            BindMessageList();

            this.txtMessage.Text = null;

            //Console.WriteLine($"{dv}");
            //System.Windows.Forms.MessageBox.Show($"{sendMessage.Value}");

            
        }

    }
}