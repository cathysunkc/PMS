using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace PMS
{
    public class DB
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DB()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "pms";
            uid = "root";
            password = "password"; // Replace with your actual MySQL password
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};Convert Zero Datetime=true;";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
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

        public DataTable SelectUserByIDPassword(string userID, string password)
        {
            string query = $"SELECT * FROM pms_user WHERE user_id = '{userID}' and password = '{password}' LIMIT 1;";

            DataTable dt = new DataTable();

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dt.Load(dataReader);

                dataReader.Close();
                this.CloseConnection();

                return dt;
            }
            else
            {
                return dt;
            }
        }

        public DataTable SelectUserByID(string userID)
        {
            string query = $"SELECT * FROM pms_user WHERE user_id = '{userID}' LIMIT 1;";

            DataTable dt = new DataTable();

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dt.Load(dataReader);

                dataReader.Close();
                this.CloseConnection();

                return dt;
            }
            else
            {
                return dt;
            }
        }

        public User GetUserByID(string userID)
        {
            User user = null;
            string query = "SELECT * FROM pms_user WHERE user_id = @userID";

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userID", userID);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        user = new User
                        {
                            UserID = dataReader["user_id"].ToString(),
                            Password = dataReader["password"].ToString(),
                            FirstName = dataReader["first_name"].ToString(),
                            LastName = dataReader["last_name"].ToString(),
                            Email = dataReader["email"].ToString(),
                            Phone = dataReader["phone"].ToString(),
                            Role = dataReader["role"].ToString()
                        };
                    }
                }
                this.CloseConnection();
            }
            return user;
        }

        public bool UpdateUserPassword(User user)
        {
            string query = "UPDATE pms_user SET password = @password WHERE user_id = @userID";

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@userID", user.UserID);

                int result = cmd.ExecuteNonQuery();
                this.CloseConnection();
                return result > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
