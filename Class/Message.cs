using Google.Protobuf.WellKnownTypes;
using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web.DynamicData;
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

        // initial messages are before 20
        private static int MessageINT = 20;

        //Constructors
        public Message() 
        {
            this.MessageID = "M" + MessageINT.ToString("D6");
            MessageINT++;
        }

        public Message(string messageID) 
        { 
            this.MessageID = "M" + MessageINT.ToString("D6");
            MessageINT++;
        }

        public Message(string senderID, string recipentID, string propertyID, string SendOutDate, bool IsImportant, string content, bool isChecked)
        {
            this.MessageID = "M" + MessageINT.ToString("D6");
            this.SenderID = senderID;
            this.RecipentID = recipentID;
            this.PropertyID = propertyID;
            this.SendOutDate = SendOutDate;
            this.IsImportant = IsImportant;
            this.Content = content;
            this.IsChecked = isChecked;
            MessageINT++;
        }

        //Methods
        public Message GetMessageByID(string messageID)
        {
            //To be implemented
            return null;
        }

        public static DataTable GetMessageByUserID(string userID)
        {
            DB dB = new DB();
            DataTable dtDB = dB.SelectMessageByID(userID);
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

        public static DataTable GetMessageByUserIDAndPropID(string userID, string propertyID)
        {
            DB dB = new DB();
            DataTable dtDB = dB.SelectMessageByID(userID);
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
                dt.Columns.Add(tempColSend);
                dt.Columns.Add(tempColRecip);
                dt.Columns.Add(tempColProp);
                foreach (DataRow row in dt.Rows)
                {
                    row["tempSend"] = Convert.ChangeType(user.GetUserByID((string)row["sender_id"]), typeof(User));
                    row["tempRecip"] = Convert.ChangeType(user.GetUserByID((string)row["recipent_id"]), typeof(User));
                    row["tempProp"] = Convert.ChangeType(Property.GetPropertyByID(propertyID), typeof(Property));
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

        public void AlertMessage()
        {
            //To be implemented
        }
    }
}