using System;
using System.Data;

namespace PMS
{
	//Implement the User class

	public class User 
	{
		//Attributes with Getters and Setters
		public string UserID { get; set; }

		public string Password { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }

        public int FailedAttempts { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LockTime { get; set; }

        //Constructors
        public User() { }

		public User(string userID, string password) 
		{
			this.UserID = userID;
			this.Password = password;			
		}

		public User(string userID, string password, string firstName, string lastName, string email, string phone, string role)
		{
			this.UserID = userID;
			this.Password = password;
			this.FirstName = firstName;
			this.LastName = lastName;
			this.Email = email;			
			this.Phone = phone;
			this.Role = role;
		}

		// Methods
		public bool IsRealtor()
		{
			return this.Role == "realtor";		
		}

        public bool IsClient()
        {
            return this.Role == "client";

        }

        public string GetFullName()
		{
			return this.FirstName + " " + this.LastName;
		}

        public User Login(string userID, string password)
        {
            DB db = new DB();
            User user = db.UserSelectByID(userID);

            if (user == null)

            {
                Console.WriteLine("User not found.");
                return null;
            }
           

            // Change the duration from 24 hours to 2 minutes
            const double lockDurationMinutes = 2;

            Console.WriteLine($"User: {userID}, IsLocked: {user.IsLocked}, LockTime: {user.LockTime}, FailedAttempts: {user.FailedAttempts}");

            if (user.IsLocked && user.LockTime.HasValue && (DateTime.Now - user.LockTime.Value).TotalMinutes < lockDurationMinutes)
            {
                // User is locked and lock duration has not passed
                Console.WriteLine("Account is locked. Lock duration has not passed.");
                return null;
            }
            else if (user.IsLocked && user.LockTime.HasValue && (DateTime.Now - user.LockTime.Value).TotalMinutes >= lockDurationMinutes)
            {
                // Lock duration has passed, reset the lock
                Console.WriteLine("Lock duration has passed. Resetting lock status.");
                user.IsLocked = false;
                user.FailedAttempts = 0;
                user.LockTime = null;
            }

            if (user.Password == password)
            {
                Console.WriteLine("Login successful. Resetting failed attempts.");
                user.FailedAttempts = 0;
                user.IsLocked = false;
                user.LockTime = null;
                db.UpdateUserAccountStatus(user);
                return user;
            }
            else
            {
                user.FailedAttempts += 1;
                Console.WriteLine($"Failed login attempt {user.FailedAttempts}.");

                if (user.FailedAttempts >= 5)
                {
                    Console.WriteLine("Account is locked due to 5 failed attempts.");
                    user.IsLocked = true;
                    user.LockTime = DateTime.Now;
                }

                db.UpdateUserAccountStatus(user);
                return null;
            }
        }


        public User GetUserByID(string userID)
        {
            DB db = new DB();
            return db.UserSelectByID(userID);
        }



        public User GetUserDetails(string userID)
            {
                DB db = new DB();
                User user = null;
                DataTable dt = db.SelectUserByID(userID);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    user = new User(userID, dr["password"].ToString())
                    {
                        FirstName = dr["first_name"].ToString(),
                        LastName = dr["last_name"].ToString(),
                        Email = dr["email"].ToString(),
                        Phone = dr["phone"].ToString(),
                        Role = dr["role"].ToString()
                    };
                }
                return user;
        }


        public bool DeleteAccount(string userID)
        {
            DB db = new DB();
            return db.DeleteUserAccount(userID);
        }
    
    public void ChangePassword(string userID, string password)
        {
            this.UserID = userID;
            this.Password = password;
        }

        public void LockAccount()
        { 
            //To be implemented
        }

        public void UnlockAccount()
        {
            //To be implemented
        }

        
        //Records for prototype
        /*
        public DataTable TempUserRecords()
        {
            DataTable dt = new DataTable("Users");

            dt.Columns.Add("user_id", typeof(string));
            dt.Columns.Add("password", typeof(string));
            dt.Columns.Add("first_name", typeof(string));
            dt.Columns.Add("last_name", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("phone", typeof(string));
            dt.Columns.Add("role", typeof(string));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["user_id"] };

            dt.Rows.Add("client01", "password", "Client", "01", "client01@test.com", "", "client");
            dt.Rows.Add("realtor01", "password", "Realtor", "01", "realtor01@test.com", "", "realtor");
            dt.Rows.Add("client02", "password", "Client", "02", "client02@test.com", "", "client");
            dt.Rows.Add("realtor02", "password", "Realtor", "02", "realtor02@test.com", "", "realtor");

            return dt;
        }
        */
    }
}