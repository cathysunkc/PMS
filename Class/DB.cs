using System;
using System.Data;

namespace PMS
{
	//Implement the DB class

	public class DB 
	{
		//Attributes with Getters and Setters
		public Property Property { get; set; }

		public User User { get; set; }

		public Message Message { get; set; }

		public Preference Preference { get; set; }

		//Constructors
		public DB() 
        {
			//To be implemented
		}
		public void AddProperty(Property property)
		{
			//To be implemented
		}

		public void UpdateProperty()
		{
			//To be implemented
		}

		public void DeleteProperty()
		{
			//To be implemented
		}


		public void AddUser(User user)
		{
			//To be implemented
		}

		public void UpdateUser()
		{
			//To be implemented
		}

		public void DeleteUser()
		{
			//To be implemented
		}

		public void AddMessage(Message message)
		{
			//To be implemented
		}

		public void UpdateMessage()
		{
			//To be implemented
		}

		public void DeleteMessage()
		{
			//To be implemented
		}

		public void AddPreference()
		{
			//To be implemented
		}

		public void UpdatePreference()
		{
			//To be implemented
		}

		public void DeletePreference()
		{
			//To be implemented
		}

	}
}