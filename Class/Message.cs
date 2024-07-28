using Google.Protobuf.WellKnownTypes;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.DynamicData;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PMS
{
	//Implement the Message class

	public class Message 
	{
		//Attributes with Getters and Setters
		public string MessageID { get; set; }

        public string SenderID { get; set; }

        public string RecipentID { get; set; }

        public string PropertyID { get; set; }

        public string SendOutDate { get; set; }

        public bool IsImportant { get; set; }

        public string Content { get; set; }

        public bool IsChecked { get; set; }

        //Constructors
        public Message() 
        {
            this.MessageID = Message.GererateMessageID();
        }

        public Message(string messageID) 
        {
            this.MessageID = Message.GererateMessageID();
        }

        public Message(string senderID, string recipentID, string propertyID, string SendOutDate, bool IsImportant, string content, bool isChecked)
        {
            this.MessageID = Message.GererateMessageID();
            this.SenderID = senderID;
            this.RecipentID = recipentID;
            this.PropertyID = propertyID;
            this.SendOutDate = SendOutDate;
            this.IsImportant = IsImportant;
            this.Content = content;
            this.IsChecked = isChecked;
        }

        //Methods

        public static string GererateMessageID()
        {
            // example: last message ID "M000001", new message ID "M000002"
            DB dB = new DB();
            string lastStringId = dB.SelectMessageLastID();
            int lastIntId = int.Parse(lastStringId.Substring(1)) + 1;
            return "M" + lastIntId.ToString("D6");
        }

        public Message GetMessageByID(string messageID)
        {
            //To be implemented
            return null;
        }

        public static DataTable GetMessageByUserID(string userID)
        {
            DB dB = new DB();
            DataTable dtDB = dB.SelectMessageByUserID(userID);
            DataTable dt = new DataTable();

            DataRow[] dr = dtDB.AsEnumerable()
                .OrderByDescending(row => row["sendout_date"])
                .GroupBy(row => row["property_id"])
                .Select(group => group.First())
                .ToArray();

            /*
            DataRow[] dr = (TempMessageRecords()).AsEnumerable()
                            .Where(row => (string)row["sender_id"] == $"{userID}" || (string)row["recipent_id"] == $"{userID}")
                            .GroupBy(row => row["property_id"])
                            .Select(group => group.First())
                            .ToArray();
            */

            if (dr.Length > 0)
            {
                dt = dr.CopyToDataTable();
                User user = new User();

                DataColumn tempColSend = new DataColumn("tempSend", typeof(User));
                DataColumn tempColRecip = new DataColumn("tempRecip", typeof(User));
                DataColumn tempColProp = new DataColumn("tempProp", typeof(Property));
                dt.Columns.Add(tempColSend);
                dt.Columns.Add(tempColRecip);
                dt.Columns.Add(tempColProp);
                foreach (DataRow row in dt.Rows)
                {
                    row["tempSend"] = Convert.ChangeType(user.GetUserByID((string)row["sender_id"]), typeof(User));
                    row["tempRecip"] = Convert.ChangeType(user.GetUserByID((string)row["recipent_id"]), typeof(User));
                    row["tempProp"] = Convert.ChangeType(Property.GetPropertyByID((string)row["property_id"]), typeof(Property));
                }
                tempColSend.SetOrdinal(1);
                tempColRecip.SetOrdinal(2);
                tempColProp.SetOrdinal(3);
                dt.Columns.Remove("sender_id");
                dt.Columns.Remove("recipent_id");
                dt.Columns.Remove("property_id");
                tempColSend.ColumnName = "sender";
                tempColRecip.ColumnName = "recipent";
                tempColProp.ColumnName = "property";
            }
            else if (dr.Length == 0)
                dt = new DataTable();

            return dt;
        }


        public static DataTable GetMessageByUserIDAndPref(string userID, char transactionType, double bedNum, double bathNum)
        {
            DB dB = new DB();
            DataTable dtDB = dB.SelectMessageByUserID(userID);

            // Get all PropertyID by Pref
            DataTable dtPropDB = Property.FindProperty(dB, transactionType, bedNum, bathNum);
            string[] propIDs = dtPropDB.AsEnumerable()
                .Select(row => (string)row["property_id"])
                .ToArray();

            DataTable dt = new DataTable();


            DataRow[] dr = dtDB.AsEnumerable()
                .Where(row => propIDs.Contains((string)row["property_id"]))
                .OrderByDescending(row => row["sendout_date"])
                .GroupBy(row => row["property_id"])
                .Select(group => group.First())
                .ToArray();


            if (dr.Length > 0)
            {
                dt = dr.CopyToDataTable();
                User user = new User();

                DataColumn tempColSend = new DataColumn("tempSend", typeof(User));
                DataColumn tempColRecip = new DataColumn("tempRecip", typeof(User));
                DataColumn tempColProp = new DataColumn("tempProp", typeof(Property));
                dt.Columns.Add(tempColSend);
                dt.Columns.Add(tempColRecip);
                dt.Columns.Add(tempColProp);
                foreach (DataRow row in dt.Rows)
                {
                    row["tempSend"] = Convert.ChangeType(user.GetUserByID((string)row["sender_id"]), typeof(User));
                    row["tempRecip"] = Convert.ChangeType(user.GetUserByID((string)row["recipent_id"]), typeof(User));
                    row["tempProp"] = Convert.ChangeType(Property.GetPropertyByID((string)row["property_id"]), typeof(Property));
                }
                tempColSend.SetOrdinal(1);
                tempColRecip.SetOrdinal(2);
                tempColProp.SetOrdinal(3);
                dt.Columns.Remove("sender_id");
                dt.Columns.Remove("recipent_id");
                dt.Columns.Remove("property_id");
                tempColSend.ColumnName = "sender";
                tempColRecip.ColumnName = "recipent";
                tempColProp.ColumnName = "property";
            }
            else if (dr.Length == 0)
                dt = new DataTable();

            return dt;
        }


        public static DataTable GetMessageByUserIDAndPropID(string userID, string propertyID)
        {
            DB dB = new DB();
            DataTable dtDB = dB.SelectMessageByUserID(userID);
            DataTable dt = new DataTable();

            DataRow[] dr = dtDB.AsEnumerable()
                            .Where(row => (string)row["property_id"] == $"{propertyID}")
                            .OrderBy(row => row["sendout_date"])
                            .ToArray();

            if (dr.Length > 0) 
            {
                dt = dr.CopyToDataTable();
                User user = new User();

                DataColumn tempColSend = new DataColumn("tempSend", typeof(User));
                DataColumn tempColRecip = new DataColumn("tempRecip", typeof(User));
                DataColumn tempColProp = new DataColumn("tempProp", typeof(Property));
                DataColumn tempColSendout = new DataColumn("tempSendOut", typeof(DateTime));
                dt.Columns.Add(tempColSend);
                dt.Columns.Add(tempColRecip);
                dt.Columns.Add(tempColProp);
                dt.Columns.Add(tempColSendout);
                foreach (DataRow row in dt.Rows)
                {
                    row["tempSend"] = Convert.ChangeType(user.GetUserByID((string)row["sender_id"]), typeof(User));
                    row["tempRecip"] = Convert.ChangeType(user.GetUserByID((string)row["recipent_id"]), typeof(User));
                    row["tempProp"] = Convert.ChangeType(Property.GetPropertyByID(propertyID), typeof(Property));
                    row["tempSendOut"] = Convert.ChangeType(row["sendout_date"], typeof(DateTime));
                }
                tempColSend.SetOrdinal(1);
                tempColRecip.SetOrdinal(2);
                tempColProp.SetOrdinal(3);
                tempColSendout.SetOrdinal(4);
                dt.Columns.Remove("sender_id");
                dt.Columns.Remove("recipent_id");
                dt.Columns.Remove("property_id");
                dt.Columns.Remove("sendout_date");
                tempColSend.ColumnName = "sender";
                tempColRecip.ColumnName = "recipent";
                tempColProp.ColumnName = "property";
                tempColSendout.ColumnName = "sendout_date";

                /*
                HashSet<DateTime> sendoutDates = new HashSet<DateTime>();
                foreach (DataRow row in dt.Rows)
                {
                    sendoutDates.Add(DateTime.Parse(row["SendoutDate"].ToString()));
                }
                */
            }
            else if (dr.Length == 0)
                dt = new DataTable();

            return dt;
        }

        /*
        //Records for Prototype
        public DataTable TempMessageRecords()
        {
            DataTable dt = new DataTable("Messages");
            User user = new User();

            dt.Columns.Add("message_id", typeof(string));
            dt.Columns.Add("sender_id", typeof(string));
            dt.Columns.Add("recipent_id", typeof(string));
            dt.Columns.Add("property_id", typeof(string));
            dt.Columns.Add("sendout_date", typeof(DateTime));
            dt.Columns.Add("is_important", typeof(bool));
            dt.Columns.Add("content", typeof(string));
            dt.Columns.Add("is_checked", typeof(bool));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["message_id"] };

            dt.Rows.Add("M000001", "client01", "realtor01", "P000001",
                "2024-04-01", 
                true, "Hi, I am client01 and want to ask about P000001", false);
            dt.Rows.Add("M000002", "client01", "realtor01", "P000002",
                "2024-04-03", 
                true, "Hi, I am client01 and want to ask about P000002", false);
            dt.Rows.Add("M000003", "client01", "realtor01", "P000003",
                "2024-04-02", 
                false, "Hi, I am client01 and want to ask about P000003", false);
            dt.Rows.Add("P000004", "client01", "realtor01", "P000004",
                "2024-04-02", 
                false, "Hi, I am client01 and want to ask about P000004", false);
            dt.Rows.Add("P000005", "client01", "realtor01", "P000005",
               "2024-04-02",
               false, "Hi, I am client01 and want to ask about P000005", false);
            dt.Rows.Add("P000006", "client01", "realtor01", "P000006",
               "2024-04-03", 
               false, "Hi, I am client01 and want to ask about P000006", false);

            dt.Rows.Add("M000011", "realtor01", "client01", "P000001",
                "2024-05-01",
                true, "Nice to meet you. I am realtor01 and want to reply about P000001", false);
            dt.Rows.Add("M000012", "realtor01", "client01", "P000002",
                "2024-05-05",
                true, "Nice to meet you. I am realtor01 and want to reply about P000002", false);
            dt.Rows.Add("M000013", "realtor01", "client01", "P000003",
                "2024-05-10",
                false, "Nice to meet you. I am realtor01 and want to reply about P000003", false);
            dt.Rows.Add("M000014", "realtor01", "client01", "P000004",
                "2024-05-30",
                false, "Nice to meet you. I am realtor01 and want to reply about P000004", false);
            dt.Rows.Add("M000015", "realtor01", "client01", "P000005",
               "2024-05-30",
               false, "Nice to meet you. I am realtor01 and want to reply about P000005", false);
            dt.Rows.Add("M000016", "realtor01", "client01", "P000006",
                "2024-05-15",
               false, "Nice to meet you. I am realtor01 and want to reply about P000006", false);


            return dt;
        }
        */

        public void CheckMessage(string messageID)
        {
            //To be implemented
            this.IsChecked = true;
        }

        public void SendMessage(Message message)
        {
            DB dB = new DB();
            dB.AddMessage(message);
        }

        public static int? AlertMessage(string userID)
        {
            DB dB = new DB();
            DataTable dtDB = dB.SelectMessageByUserID(userID);
            DataRow[] dr = dtDB.AsEnumerable()
                .Where(row => (bool)row["is_checked"] == false && (string)row["recipent_id"] == $"{userID}")
                .ToArray();
            return dr.Length;
        }

        public static bool IsValidated(string content) 
        {

            if (string.IsNullOrWhiteSpace(content))
            {
                System.Windows.Forms.MessageBox.Show($"Please enter a message.");
                return false;
            }

            return true;
        }
    }
}