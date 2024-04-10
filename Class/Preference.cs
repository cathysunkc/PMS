using System;
using System.Data;

namespace PMS
{
	//Implement the Preference class

	public class Preference 
	{
		//Attributes with Getters and Setters
		public User User { get; set; }

        public Property[] PropertyList { get; set; }

       
        //Constructors
        public Preference() 
        { 
        
        }

        public Preference(User user, Property[] propertyList) 
        { 
            this.User = user;
            this.PropertyList = propertyList;
        }

        
        //Methods
        public Preference GetPreference()
        {
            //To be implemented
            return null;
        }
         
    }
}