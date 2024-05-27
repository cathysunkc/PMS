using System;
using System.Data;
using System.Linq;

namespace PMS
{
	//Implement the Message class

	public class Message 
	{
		//Attributes with Getters and Setters
		public string MessageID { get; set; }

        public User Sender { get; set; }

        public User Recipent { get; set; }

        public Property Property { get; set; }

        public DateTime SendOutDate { get; set; }

        public bool IsImportant { get; set; }

        public string Content { get; set; }

        public bool IsChecked { get; set; }


        //Constructors
        public Message() 
        { 
        
        }

        public Message(string messageID) 
        { 
            this.MessageID = messageID;
        }

        public Message(string messageID, User sender, User recipent, Property property, DateTime SendOutDate, bool IsImportant, string content, bool isChecked)
        {
            this.MessageID = messageID;
            this.Sender = sender;
            this.Recipent = recipent;
            this.Property = property;
            this.SendOutDate = SendOutDate;
            this.IsImportant = IsImportant;
            this.Content = content;
            this.IsChecked = isChecked;
        }

        //Methods
        public Message GetMessageByID(string messageID)
        {
            //To be implemented
            return null;
        }

        public DataTable GetMessageByUserID(string userID)
        {
            DataTable dt = new DataTable();

            DataRow[] dr = (TempMessageRecords()).AsEnumerable()
                            .Where(row => ((User)row["sender"]).UserID == $"{userID}" || ((User)row["recipent"]).UserID == $"{userID}")
                            .GroupBy(row => ((Property)row["property"]).PropertyID)
                            .Select(group => group.First())
                            .ToArray();

            if (dr.Length > 0)
                dt = dr.CopyToDataTable();
            else if (dr.Length == 0)
                dt = new DataTable();

            return dt;
        }

        public DataTable GetMessageByPropID(string userID, string propertyID)
        {
            DataTable dt = new DataTable();

            DataRow[] dr = (TempMessageRecords()).AsEnumerable()
                            .Where(row => (((User)row["sender"]).UserID == $"{userID}" || ((User)row["recipent"]).UserID == $"{userID}") && ((Property)row["property"]).PropertyID == $"{propertyID}")
                            .ToArray();

            if (dr.Length > 0)
                dt = dr.CopyToDataTable();
            else if (dr.Length == 0)
                dt = new DataTable();

            return dt;
        }

        //Records for Prototype
        public DataTable TempMessageRecords()
        {
            DataTable dt = new DataTable("Messages");
            User user = new User();
            Property property = new Property();

            dt.Columns.Add("message_id", typeof(string));
            dt.Columns.Add("sender", typeof(User));
            dt.Columns.Add("recipent", typeof(User));
            dt.Columns.Add("property", typeof(Property));
            dt.Columns.Add("sendout_date", typeof(DateTime));
            dt.Columns.Add("is_important", typeof(bool));
            dt.Columns.Add("content", typeof(string));
            dt.Columns.Add("is_checked", typeof(bool));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["message_id"] };

            dt.Rows.Add("M000001", user.GetUserByID("client01"), user.GetUserByID("realtor01"), property.GetPropertyByID("P000001"),
                "2024-04-01", 
                true, "Hi, I'm client01 and want to ask about P000001", false);
            dt.Rows.Add("M000002", user.GetUserByID("client01"), user.GetUserByID("realtor01"), property.GetPropertyByID("P000002"),
                "2024-04-03", 
                true, "Hi, I'm client01 and want to ask about P000002", false);
            dt.Rows.Add("M000003", user.GetUserByID("client01"), user.GetUserByID("realtor01"), property.GetPropertyByID("P000003"),
                "2024-04-02", 
                false, "Hi, I'm client01 and want to ask about P000003", false);
            dt.Rows.Add("P000004", user.GetUserByID("client01"), user.GetUserByID("realtor01"), property.GetPropertyByID("P000004"),
                "2024-04-02", 
                false, "Hi, I'm client01 and want to ask about P000004", false);
            dt.Rows.Add("P000005", user.GetUserByID("client01"), user.GetUserByID("realtor01"), property.GetPropertyByID("P000005"),
               "2024-04-02",
               false, "Hi, I'm client01 and want to ask about P000005", false);
            dt.Rows.Add("P000006", user.GetUserByID("client01"), user.GetUserByID("realtor01"), property.GetPropertyByID("P000006"),
               "2024-04-03", 
               false, "Hi, I'm client01 and want to ask about P000006", false);

            dt.Rows.Add("M000011", user.GetUserByID("realtor01"), user.GetUserByID("client01"), property.GetPropertyByID("P000001"),
                "2024-05-01",
                true, "Nice to meet you. I'm realtor01 and want to reply about P000001", false);
            dt.Rows.Add("M000012", user.GetUserByID("realtor01"), user.GetUserByID("client01"), property.GetPropertyByID("P000002"),
                "2024-05-05",
                true, "Nice to meet you. I'm realtor01 and want to reply about P000002", false);
            dt.Rows.Add("M000013", user.GetUserByID("realtor01"), user.GetUserByID("client01"), property.GetPropertyByID("P000003"),
                "2024-05-10",
                false, "Nice to meet you. I'm realtor01 and want to reply about P000003", false);
            dt.Rows.Add("M000014", user.GetUserByID("realtor01"), user.GetUserByID("client01"), property.GetPropertyByID("P000004"),
                "2024-05-30",
                false, "Nice to meet you. I'm realtor01 and want to reply about P000004", false);
            dt.Rows.Add("M000015", user.GetUserByID("realtor01"), user.GetUserByID("client01"), property.GetPropertyByID("P000005"),
               "2024-05-30",
               false, "Nice to meet you. I'm realtor01 and want to reply about P000005", false);
            dt.Rows.Add("M000016", user.GetUserByID("realtor01"), user.GetUserByID("client01"), property.GetPropertyByID("P000006"),
                "2024-05-15",
               false, "Nice to meet you. I'm realtor01 and want to reply about P000006", false);


            return dt;
        }

        public void CheckMessage(string messageID)
        {
            //To be implemented
            this.IsChecked = true;
        }

        public void SendMessage()
        {
            //To be implemented
        }

        public void AlertMessage()
        {
            //To be implemented
        }
    }
}