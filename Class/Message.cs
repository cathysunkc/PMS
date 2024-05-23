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
            DB db = new DB();

            dt.Columns.Add("message_id", typeof(string));
            dt.Columns.Add("sender", typeof(User));
            dt.Columns.Add("recipent", typeof(User));
            dt.Columns.Add("property", typeof(Property));
            dt.Columns.Add("sendout_date", typeof(DateTime));
            dt.Columns.Add("is_important", typeof(bool));
            dt.Columns.Add("content", typeof(string));
            dt.Columns.Add("is_checked", typeof(bool));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["message_id"] };

            dt.Rows.Add("M000001", user.GetUserByID("client01"), user.GetUserByID("realtor01"), Property.GetPropertyByID(db, "P000001"),
                "2024-04-01", 
                true, "Welcome to Arrive at Bowness, where innovation meets elegance!", false);
            dt.Rows.Add("M000002", user.GetUserByID("client01"), user.GetUserByID("realtor01"), Property.GetPropertyByID(db, "P000002"),
                "2024-04-03", 
                true, "LOCATED IN THE HIGHLY SOUGHT AFTER COMMUNITY OF LAKE MAHOGANY. ", false);
            dt.Rows.Add("M000003", user.GetUserByID("client01"), user.GetUserByID("realtor01"), Property.GetPropertyByID(db, "P000003"),
                "2024-04-02", 
                false, "Nestled within the vibrant community of Copperfield, this perfectly upgraded END UNIT townhome invites you to indulge in a lifestyle of convenience and style.", false);
            dt.Rows.Add("P000004", user.GetUserByID("client01"), user.GetUserByID("realtor01"), Property.GetPropertyByID(db, "P000004"),
                "2024-04-02", 
                false, "3 Bed 1.5 Bath House in Temple with Double Car Garage ", false);
            dt.Rows.Add("P000005", user.GetUserByID("client01"), user.GetUserByID("realtor01"), Property.GetPropertyByID(db, "P000005"),
               "2024-04-02",
               false, "Legal Suite BASEMENT with 2 bedrooms 1 bathroom - Tenants will have their separate entrance, kitchen, washer and dryer, microwave and parking space.", false);
            dt.Rows.Add("P000006", user.GetUserByID("client01"), user.GetUserByID("realtor01"), Property.GetPropertyByID(db, "P000006"),
               "2024-04-03", 
               false, "Introducing a Spectacular Residence in Hamptons for Rent:Property Size: 3400 SQ FT **Breathtaking Views**: Enjoy stunning vistas of the golf course pond, adding a serene and tranquil backdrop to your daily life.", false);

            dt.Rows.Add("M000011", user.GetUserByID("realtor01"), user.GetUserByID("client01"), Property.GetPropertyByID(db, "P000001"),
                "2024-05-01",
                true, "This townhouse, honoured with the esteemed 2017 Mayors Urban Design Award for Housing Innovation, is a beacon of contemporary living.", false);
            dt.Rows.Add("M000012", user.GetUserByID("realtor01"), user.GetUserByID("client01"), Property.GetPropertyByID(db, "P000002"),
                "2024-05-05",
                true, "2 BED PLUS DEN, 2 BATH, TITLED UNDERGROUND PARKING, AND STORAGE LOCKER!", false);
            dt.Rows.Add("M000013", user.GetUserByID("realtor01"), user.GetUserByID("client01"), Property.GetPropertyByID(db, "P000003"),
                "2024-05-10",
                false, "Nestled within the vibrant community of Copperfield, this perfectly upgraded END UNIT townhome invites you to indulge in a lifestyle of convenience and style.", false);
            dt.Rows.Add("M000014", user.GetUserByID("realtor01"), user.GetUserByID("client01"), Property.GetPropertyByID(db, "P000004"),
                "2024-05-30",
                false, "- More Photos Coming Soon!Located in the established community of Temple, this updated 1100+ square foot detached house with a double car garage is the perfect home with a variety of features. ", false);
            dt.Rows.Add("M000015", user.GetUserByID("realtor01"), user.GetUserByID("client01"), Property.GetPropertyByID(db, "P000005"),
               "2024-05-30",
               false, "Legal Suite BASEMENT with 2 bedrooms 1 bathroom - Tenants will have their separate entrance, kitchen, washer and dryer, microwave and parking space.", false);
            dt.Rows.Add("M000016", user.GetUserByID("realtor01"), user.GetUserByID("client01"), Property.GetPropertyByID(db, "P000006"),
                "2024-05-15",
               false, "Introducing a Spectacular Residence in Hamptons for Rent:Property Size: 3400 SQ FT **Breathtaking Views**: Enjoy stunning vistas of the golf course pond, adding a serene and tranquil backdrop to your daily life.", false);


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