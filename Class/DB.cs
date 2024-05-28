using System;
using System.Data;
using System.Diagnostics.Metrics;
using Google.Protobuf.WellKnownTypes;
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

        /************************************
         * Property
        /************************************/
        public void AddProperty(Property property)
        {
            string query = @"INSERT INTO Properties 
                    (property_id, address, zip_code, city, property_type, bed_num, bath_num, area, parking_type, posted_date, available_date, description, is_featured, transaction_type, price, realtor_id, is_sold) 
                    VALUES 
                    (@PropertyID, @Address, @ZipCode, @City, @PropertyType, @BedNum, @BathNum, @Area, @ParkingType, @PostedDate, @AvailableDate, @Description, @IsFeatured, @TransactionType, @Price, @RealtorID, @IsSold)";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PropertyID", property.PropertyID);
                cmd.Parameters.AddWithValue("@Address", property.Address);
                cmd.Parameters.AddWithValue("@ZipCode", property.ZipCode);
                cmd.Parameters.AddWithValue("@City", property.City);
                cmd.Parameters.AddWithValue("@PropertyType", property.PropertyType);
                cmd.Parameters.AddWithValue("@BedNum", property.BedNum);
                cmd.Parameters.AddWithValue("@BathNum", property.BathNum);
                cmd.Parameters.AddWithValue("@Area", property.Area);
                cmd.Parameters.AddWithValue("@ParkingType", property.ParkingType);
                cmd.Parameters.AddWithValue("@PostedDate", property.PostedDate);
                cmd.Parameters.AddWithValue("@AvailableDate", property.AvailableDate);
                cmd.Parameters.AddWithValue("@Description", property.Description);
                cmd.Parameters.AddWithValue("@IsFeatured", property.IsFeatured);
                cmd.Parameters.AddWithValue("@TransactionType", property.TransactionType);
                cmd.Parameters.AddWithValue("@Price", property.Price);
                cmd.Parameters.AddWithValue("@RealtorID", property.RealtorID);
                cmd.Parameters.AddWithValue("@IsSold", property.IsSold);

                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public void UpdateProperty()
        {
            //To be implemented
        }

        public void DeleteProperty()
        {
            //To be implemented
        }

        //The following property listings were generated by ChatGPT, an AI language model developed by OpenAI. These listings are fictional and provided as sample data for demonstration purposes.
        //images of listing is from https://pixabay.com/images/search/
        //Edited by Harry
        // New method to get a property by ID
        public Property GetPropertyByID(string propertyID)
        {
            string query = "SELECT * FROM Properties WHERE property_id = @property_id";
           
            Property property = null;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@property_id", propertyID);

                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        property = new Property
                        {
                            PropertyID = dataReader["property_id"].ToString(),
                            Address = dataReader["address"].ToString(),
                            ZipCode = dataReader["zip_code"].ToString(),
                            City = dataReader["city"].ToString(),
                            PropertyType = dataReader["property_type"].ToString(),
                            BedNum = Convert.ToDouble(dataReader["bed_num"]),
                            BathNum = Convert.ToDouble(dataReader["bath_num"]),
                            Area = dataReader["area"].ToString(),
                            ParkingType = dataReader["parking_type"].ToString(),
                            PostedDate = Convert.ToDateTime(dataReader["posted_date"]),
                            AvailableDate = Convert.ToDateTime(dataReader["available_date"]),
                            Description = dataReader["description"].ToString(),
                            IsFeatured = Convert.ToBoolean(dataReader["is_featured"]),
                            TransactionType = Convert.ToChar(dataReader["transaction_type"]),
                            Price = Convert.ToDouble(dataReader["price"]),
                            ImagePath = "~/Images/" + propertyID + "/" + propertyID + "01.jpg",
                            RealtorID = dataReader["realtor_id"].ToString(),
                            IsSold = Convert.ToBoolean(dataReader["is_sold"])
                        };

                        // ImagePath = dataReader["image_path"].ToString(),
                    }
                }
                CloseConnection();
            }
            return property;
        }

        //Edited by Harry
        // New method to get featured properties
        public DataTable GetFeaturedProperty()
        {
            string query = "SELECT * FROM Properties WHERE is_featured = 1 AND is_sold = 0";
            
            DataTable dt = new DataTable();

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                
                //close Connection
                this.CloseConnection();
            }
            return dt;
        }

        //Edited by Harry
        // New method to find properties based on criteria
        public DataTable FindProperty(char transactionType, double bedNum, double bathNum)
        {
            string query = "SELECT * FROM Properties WHERE is_sold = 0";
            
            if (transactionType != ' ')
            {
                query += " AND p.transaction_type = @transaction_type";
            }
            if (bedNum != 0)
            {
                query += " AND p.bed_num = @bed_num";
            }
            if (bathNum != 0)
            {
                query += " AND p.bath_num = @bath_num";
            }

            DataTable dt = new DataTable();

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                if (transactionType != ' ')
                {
                    cmd.Parameters.AddWithValue("@transaction_type", transactionType);
                }
                if (bedNum != 0)
                {
                    cmd.Parameters.AddWithValue("@bed_num", bedNum);
                }
                if (bathNum != 0)
                {
                    cmd.Parameters.AddWithValue("@bath_num", bathNum);
                }
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                CloseConnection();
            }
            return dt;
        }

        /************************************
         * User
        /************************************/

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