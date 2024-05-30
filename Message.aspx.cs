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

                string realtorID = Request.QueryString["realtor_id"];

                if (realtorID != null)
                {
                    User realtor = new User();
                    realtor = realtor.GetUserByID(realtorID);

                    if (realtor != null)
                    {

                        this.txtRecipientEmail.Text = realtor.Email;
                        this.txtRecipientEmail.ReadOnly = true;
                    }
                }

                // Edited by Wilson
                BindMessageList();
            }
            else
            {
                Response.Redirect("Login");
            }
        }

        // Edited by Wilson
        private void BindMessageList()
        {
            DataSet ds = new DataSet();
            DataTable dt = Message.GetMessageByUserID((String)Session["UserID"]);
            ds.Tables.Add(dt);

            listMessage.DataSource = ds;
            listMessage.DataBind();

        }

        protected void Message_Click(Object sender, CommandEventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = Message.GetMessageByUserIDAndPropID((String)Session["UserID"], (String)e.CommandArgument);
            ds.Tables.Add(dt);

            gridMessage.DataSource = ds;
            gridMessage.DataBind();

            this.panelSelectMessage.Visible = true;
            sendMessage.Value = (String)e.CommandArgument;
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            Message message = new Message((String)Session["UserID"], Property.GetPropertyByID(sendMessage.Value).RealtorID, sendMessage.Value,
    DateTime.Now.ToString("yyyy'-'MM'-'dd"), true, this.txtMessage.Text, false);
            message.SendMessage(message);
            //dt.Rows.Add("M0000XX", user.GetUserByID((String)Session["UserID"]), user.GetUserByID(property.RealtorID), property,DateTime.Now.ToString("yyyy'-'MM'-'dd"),false, this.txtMessage.Text, false);

            DataSet ds = new DataSet();
            DataTable dt = Message.GetMessageByUserIDAndPropID((String)Session["UserID"], sendMessage.Value);
            ds.Tables.Add(dt);

            gridMessage.DataSource = ds;
            gridMessage.DataBind();

            //Console.WriteLine($"{dv}");
            //System.Windows.Forms.MessageBox.Show($"{sendMessage.Value}");
   
            this.txtMessage.Text = null;
        }
    }
}