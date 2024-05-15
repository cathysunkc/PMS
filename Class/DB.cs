using System;
using System.Data;
using System.Diagnostics.Metrics;
using MySql.Data.MySqlClient;

namespace PMS
{
	//Implement the DB class

	public class DB 
	{
        //attributes
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Attributes with Getters and Setters
        public Property Property { get; set; }

		public User User { get; set; }

		public Message Message { get; set; }

		public Preference Preference { get; set; }

		//Constructors
		public DB() 
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "pms";
            uid = "root";
            password = "password";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; Convert Zero Datetime=true;";

            connection = new MySqlConnection(connectionString);

        }

        //Open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
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

        //Get User by ID and Password from DB
        public DataTable SelectUserByIDPassword(string userID, string password)
        {
            string query = $"SELECT * FROM pms_user WHERE user_id = '{userID}' and password = '{password}' LIMIT 1;";

            DataTable dt = new DataTable();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dt.Load(dataReader);

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return user
                return dt;
            }
            else
            {
                return dt;
            }
        }

        //Get User by ID from DB
        public DataTable SelectUserByID(string userID)
        {
            string query = $"SELECT * FROM pms_user WHERE user_id = '{userID}' LIMIT 1;";

            DataTable dt = new DataTable();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dt.Load(dataReader);

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return user
                return dt;
            }
            else
            {
                return dt;
            }
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