using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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

            Message message = new Message();
            DataTable dt = message.GetMessageByUserID((String)Session["UserID"]);

            ds.Tables.Add(dt);
            listMessage.DataSource = ds;
            listMessage.DataBind();
        }

        protected void Message_Click(Object sender, CommandEventArgs e)
        {
            DataSet ds = new DataSet();
            Message message = new Message();


            DataTable dt = message.GetMessageByPropID((String)Session["UserID"], (String)e.CommandArgument);

            ds.Tables.Add(dt);

            gridMessage.DataSource = ds;
            gridMessage.DataBind();
        }
    }
}