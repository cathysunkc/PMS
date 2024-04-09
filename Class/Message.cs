using System;
using System.Data;

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

        public Message(string messageID, User Sender, User Recipent, Property property, DateTime SendOutDate, bool IsImportant, string content, bool isChecked)
        {
            this.MessageID = messageID;
            this.Sender = Sender;
            this.Recipent = Recipent;
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