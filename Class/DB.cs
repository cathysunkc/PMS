using System;
using System.Data;
using System.Diagnostics.Metrics;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using System.Windows.Forms;
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
                try
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
                    Console.WriteLine("Property added successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding property: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
            else
            {
                Console.WriteLine("Unable to open database connection");
            }
        }



        public void UpdateProperty(Property property)
        {
            string query = @"UPDATE Properties 
                    SET address = @Address, 
                        zip_code = @ZipCode, 
                        city = @City, 
                        property_type = @PropertyType, 
                        bed_num = @BedNum, 
                        bath_num = @BathNum, 
                        area = @Area, 
                        parking_type = @ParkingType, 
                        available_date = @AvailableDate, 
                        description = @Description, 
                        transaction_type = @TransactionType, 
                        price = @Price 
                    WHERE property_id = @PropertyID";

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
                cmd.Parameters.AddWithValue("@AvailableDate", property.AvailableDate);
                cmd.Parameters.AddWithValue("@Description", property.Description);
                cmd.Parameters.AddWithValue("@TransactionType", property.TransactionType);
                cmd.Parameters.AddWithValue("@Price", property.Price);

                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }


        public void DeleteProperty()
        {
            //To be implemented
        }

        //The following property listings were generated by ChatGPT, an AI language model developed by OpenAI. These listings are fictional and provided as sample data for demonstration purposes.
        //images of listing is from https://pixabay.com/images/search/
        // New method to get a property by ID
        //Edited by Wilson to change as static method
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

        // New method to find properties based on criteria
        public DataTable FindProperty(char transactionType, double bedNum, double bathNum)
        {
            string query = "SELECT * FROM Properties WHERE is_sold = 0";

            if (transactionType != ' ')
            {
                query += " AND transaction_type = @transaction_type";
            }
            if (bedNum != 0)
            {
                query += " AND bed_num = @bed_num";
            }
            if (bathNum != 0)
            {
                query += " AND bath_num = @bath_num";
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

        /************************************
         * User section
         * Reference: Chatgpt
        /************************************/
        public bool AddUser(User user)
        {
            string query = "INSERT INTO pms_user (user_id, password, first_name, last_name, email, phone, role) VALUES (@userID, @password, @firstName, @lastName, @Email, @Phone, @Role)";

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userID", user.UserID);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                cmd.Parameters.AddWithValue("@lastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Role", user.Role);

                int result = cmd.ExecuteNonQuery();
                this.CloseConnection();
                return result > 0;
            }
            else
            {
                return false;
            }
        }

        /************************************
        * updating user account
        * Reference chatgpt with error handling
       /************************************/

        public bool UpdateUser(User user)
        {
            string query = "UPDATE pms_user SET password = @password, first_name = @firstName, last_name = @lastName, email = @Email, phone = @Phone, role = @Role WHERE user_id = @userID";

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userID", user.UserID);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                cmd.Parameters.AddWithValue("@lastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@Role", user.Role);

                int result = cmd.ExecuteNonQuery();
                this.CloseConnection();
                return result > 0;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteUserAccount(string userID)
        {
            string query = "DELETE FROM pms_user WHERE user_id = @userID";

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);  
                cmd.Parameters.AddWithValue("@userID", userID);

                int result = cmd.ExecuteNonQuery();
                this.CloseConnection();
                return result > 0;
            }
            else
            {
                return false;
            }
        }

        // Method to select user by userID
        public User UserSelectByID(string userID)
        {
            string query = "SELECT * FROM pms_user WHERE user_id = @UserID";
            User user = null;

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserID", userID);
                MySqlDataReader dataReader = cmd.ExecuteReader();

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
                        Role = dataReader["role"].ToString(),
                        FailedAttempts = Convert.ToInt32(dataReader["failed_attempts"]),
                        IsLocked = Convert.ToBoolean(dataReader["is_locked"]),
                        LockTime = dataReader["lock_time"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["lock_time"])
                    };
                }

                dataReader.Close();
                CloseConnection();
            }
            return user;
        }


        // Method to update user account status///////////////////////////////////////////////
        public bool UpdateUserAccountStatus(User user)
        {
            string query = @"UPDATE pms_user SET 
                    failed_attempts = @FailedAttempts, 
                    is_locked = @IsLocked, 
                    lock_time = @LockTime 
                    WHERE user_id = @UserID";

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@FailedAttempts", user.FailedAttempts);
                cmd.Parameters.AddWithValue("@IsLocked", user.IsLocked);
                cmd.Parameters.AddWithValue("@LockTime", user.LockTime.HasValue ? (object)user.LockTime.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@UserID", user.UserID);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"User account status updated successfully. Rows affected: {rowsAffected}");
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating user: " + ex.Message);
                    return false;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return false;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////
        /************************************
         * Message
        /************************************/

        public string SelectMessageLastID()
        {
            string query = $"SELECT message_id FROM pms_message ORDER BY sendout_date DESC LIMIT 1 ;";

            string messageId = null;

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    messageId = dataReader["message_id"].ToString();
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return user
                return messageId;
            }
            else
            {
                return messageId;
            }
        }

        public DataTable SelectMessageByUserID(string userID)
        {
            string query = $"SELECT * FROM pms_message WHERE sender_id = '{userID}' OR recipent_id = '{userID}' ;";

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


        public void AddMessage(Message message)
        {
            string query = @"INSERT INTO pms_message 
                    (message_id, sender_id, recipent_id, property_id, sendout_date, is_important, content, is_checked) 
                    VALUES 
                    (@MessageID, @SenderID, @RecipentID, @PropertyID, @SendOutDate, @IsImportant, @Content, @IsChecked)";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MessageID", message.MessageID);
                cmd.Parameters.AddWithValue("@SenderID", message.SenderID);
                cmd.Parameters.AddWithValue("@RecipentID", message.RecipentID);
                cmd.Parameters.AddWithValue("@PropertyID", message.PropertyID);
                cmd.Parameters.AddWithValue("@SendOutDate", message.SendOutDate);
                cmd.Parameters.AddWithValue("@IsImportant", message.IsImportant);
                cmd.Parameters.AddWithValue("@Content", message.Content);
                cmd.Parameters.AddWithValue("@IsChecked", message.IsChecked);

                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public void UpdateMessageClicked(string userID, string propertyID)
        {
            string query = @"UPDATE pms_message 
                             SET is_checked = 1 
                             WHERE recipent_id = @RecipentID
                             AND property_id = @PropertyID";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@RecipentID", userID);
                cmd.Parameters.AddWithValue("@PropertyID", propertyID);

                cmd.ExecuteNonQuery();
                CloseConnection();
            }
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

        public string GetLastPropertyID()
        {
            string query = "SELECT property_id FROM Properties ORDER BY property_id DESC LIMIT 1";
            string lastPropertyID = string.Empty;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    lastPropertyID = result.ToString();
                }
                CloseConnection();
            }
            return lastPropertyID;
        }

        public string GenerateNewPropertyID()
        {
            string lastPropertyID = GetLastPropertyID();
            int numericPart = 0;

            if (!string.IsNullOrEmpty(lastPropertyID))
            {
                numericPart = int.Parse(lastPropertyID.Substring(1)); // Extract numeric part from "P000020"
            }

            int newNumericPart = numericPart + 1;
            return $"P{newNumericPart:D6}"; 
        }


        /************************************
         * SaveSearch
        /************************************/
        public DataTable LoadSavedSearches(string userID)
        {
            string query = "SELECT search_id, search_name FROM savesearches WHERE user_id = @user_id";

            DataTable dt = new DataTable();

            // https://csharp.hotexamples.com/examples/MySql.Data.MySqlClient/MySqlCommand/-/php-mysqlcommand-class-examples.html\
            //https://stackoverflow.com/questions/21110001/sqlcommand-parameters-add-vs-addwithvalue
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user_id", userID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                CloseConnection();
            }

            return dt;
        }

        public void SaveSearch(string userID, string searchName, string searchCriteria)
        {
            string query = "INSERT INTO savesearches (user_id, search_name, search_criteria, last_search_name) VALUES (@user_id, @search_name, @search_criteria, @search_name)";

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user_id", userID);
                cmd.Parameters.AddWithValue("@search_name", searchName);
                cmd.Parameters.AddWithValue("@search_criteria", searchCriteria);

                try
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Search saved successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving search: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
            else
            {
                Console.WriteLine("Unable to open database connection");
            }
        }


        public dynamic GetSavedSearchCriteriaByName(string searchName)
        {
            string query = "SELECT search_criteria FROM savesearches WHERE search_name = @search_name";
            string searchCriteriaJson = null;

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@search_name", searchName);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        searchCriteriaJson = reader["search_criteria"].ToString();
                    }
                }
                CloseConnection();
            }

            if (searchCriteriaJson != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Deserialize<dynamic>(searchCriteriaJson);
            }

            return null;
        }

        public int GetLastSearchName(string userID)
        {
            string query = "SELECT last_search_name FROM savesearches WHERE user_id = @user_id ORDER BY last_search_name DESC LIMIT 1";
            int lastSearchName = 0;

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user_id", userID);
                object result = cmd.ExecuteScalar(); //Executes the query using ExecuteScalar(), which returns the value of the first column of the first row in the result set.
                if (result != DBNull.Value)
                {
                    lastSearchName = Convert.ToInt32(result);
                }
                CloseConnection();
            }
            return lastSearchName;
        }

        public void UpdateLastSearchName(string userID, int newSearchName)
        {
            string query = "UPDATE savesearches SET last_search_name = @newSearchName WHERE user_id = @user_id";

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@newSearchName", newSearchName);
                cmd.Parameters.AddWithValue("@user_id", userID);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }




        /************************************
         * Reporting
        /************************************/
        //Get Listing number by realtorID
        public int GetDBSalesListingNumber(string realtorID, DateTime startDate, DateTime endDate)
        {
            //Sample query
            //SELECT count(1) FROM properties WHERE realtor_id = 'realtor02' AND transaction_type='S';
            string query = $"SELECT count(1) FROM properties WHERE realtor_id = '{realtorID}' AND transaction_type='S' AND " +
                $" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}'; ";

			int count = 0;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    count = Convert.ToInt16(result);
                }
                CloseConnection();
            }
            return count;
        }

        public int GetDBRentListingNumber(string realtorID, DateTime startDate, DateTime endDate)
        {
            //Sample query
            //SELECT count(1) FROM properties WHERE realtor_id = 'realtor02' AND transaction_type='R';
            string query = $"SELECT count(1) FROM properties WHERE realtor_id = '{realtorID}' AND transaction_type='R' AND " +
				$" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}';";
			
            int count = 0;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    count = Convert.ToInt16(result);
                }
                CloseConnection();
            }
            return count;
        }

        //Get sales number by realtorID
        public int GetDBSalesNumber(string realtorID, bool isSold, DateTime startDate, DateTime endDate)
		{
            //Sample query
            //SELECT count(1) FROM properties WHERE realtor_id = 'realtor02' AND is_sold = 1  AND transaction_type='S';
            int isSoldFlag = (isSold? 1: 0);
			string query = $"SELECT count(1) FROM properties WHERE realtor_id = '{realtorID}' AND is_sold = {isSoldFlag} AND transaction_type='S' AND " +
				$" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}';";

            int count = 0;

			if (OpenConnection() == true)
			{
				MySqlCommand cmd = new MySqlCommand(query, connection);
				object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
				 	count = Convert.ToInt16(result);
				}
				CloseConnection();
			}
			return count;
		}

        //Get sales number by realtorID
        public int GetDBRentNumber(string realtorID, bool isSold, DateTime startDate, DateTime endDate)
		{
            //Sample query
            //SELECT count(1) FROM properties WHERE realtor_id = 'realtor02' AND is_sold = 1  AND transaction_type='R';
            int isSoldFlag = (isSold ? 1 : 0);
            string query = $"SELECT count(1) FROM properties WHERE realtor_id = '{realtorID}' AND is_sold = {isSoldFlag} AND transaction_type='R' AND " +
				$" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}';";

            int count = 0;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    count = Convert.ToInt16(result);
                }
                CloseConnection();
            }
            return count;
        }

        //Get sales percentage by realtorID
        public double GetDBSalesPercentage(string realtorID, bool isSold, DateTime startDate, DateTime endDate)
		{
            //Sample query
            //SELECT (select count(1) from properties where realtor_id = 'realtor02' and is_sold = 1 AND transaction_type='S')/(select count(1) from properties where realtor_id = 'realtor02' AND transaction_type='S') * 100 from properties where realtor_id = 'realtor02' group by realtor_id;
            int isSoldFlag = (isSold ? 1 : 0);

            string query = $"SELECT (select count(1) FROM properties WHERE realtor_id = '{realtorID}' AND is_sold = {isSoldFlag} AND transaction_type='S' AND " +
				$" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}' " + 
			    ") / " +
                $" (select count(1) from properties where realtor_id = '{realtorID}' AND transaction_type='S' AND " +
				$" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}' " +
				") * 100 " +
                $" from properties where realtor_id = '{realtorID}' group by realtor_id;";

            double percentage = 0;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    percentage = Convert.ToDouble(result);
                }
                CloseConnection();
            }
            return percentage;
        }

        //Get rent percentage by realtorID
        public double GetDBRentPercentage(string realtorID, bool isSold, DateTime startDate, DateTime endDate)
		{
            //Sample query
            //SELECT (select count(1) from properties where realtor_id = 'realtor02' and is_sold = 1 AND transaction_type='R')/(select count(1) from properties where realtor_id = 'realtor02' AND transaction_type='R') * 100 from properties where realtor_id = 'realtor02' group by realtor_id;
            int isSoldFlag = (isSold ? 1 : 0);

            string query = $"SELECT (select count(1) FROM properties WHERE realtor_id = '{realtorID}' AND is_sold = {isSoldFlag} AND transaction_type='R' AND " +
				$" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}' " +
				") / " +
                $" (select count(1) from properties where realtor_id = '{realtorID}' AND transaction_type='R' AND " +
				$" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}' " +
				") * 100 " +
                $" from properties where realtor_id = '{realtorID}' group by realtor_id;";

            double percentage = 0;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (!Convert.IsDBNull(result))
                {
                    percentage = Convert.ToDouble(result);
                }
                CloseConnection();
            }
            return percentage;
        }

        //Get sales listing post date by realtorID
        public DateTime GetDBSalesListingPostedDate(string realtorID, bool isFrom)
        {
            //Sample query
            //SELECT min(posted_date) FROM `properties` WHERE realtor_id='realtor02'  AND transaction_type='S';
            //SELECT max(posted_date) FROM `properties` WHERE realtor_id='realtor02'  AND transaction_type='S';
            string isFromFlag = (isFrom ? "min" : "max");

            string query = $"SELECT {isFromFlag}(posted_date) from properties where realtor_id = '{realtorID}'  AND transaction_type='S'";

            DateTime date = DateTime.Now;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    date = Convert.ToDateTime(result);
                }
                CloseConnection();
            }
            return date;
        }

        //Get rent listing post date by realtorID
        public DateTime GetDBRentListingPostedDate(string realtorID, bool isFrom)
        {
            //Sample query
            //SELECT min(posted_date) FROM `properties` WHERE realtor_id='realtor02'  AND transaction_type='R';
            //SELECT max(posted_date) FROM `properties` WHERE realtor_id='realtor02'  AND transaction_type='R';
            string isFromFlag = (isFrom ? "min" : "max");

            string query = $"SELECT {isFromFlag}(posted_date) from properties where realtor_id = '{realtorID}'  AND transaction_type='R'";

            DateTime date = DateTime.Now;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    date = Convert.ToDateTime(result);
                }
                CloseConnection();
            }
            return date;
        }

        //Get sales number by realtorID and date range
        public int GetDBSalesByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            //Sample query
            //SELECT count(1) FROM properties WHERE realtor_id = 'realtor02' AND is_sold = 1 AND transaction_type='S' AND posted_date between '2023-05-01' AND '2024-04-30';
            string query = $"SELECT count(1) FROM properties WHERE realtor_id = '{realtorID}' AND is_sold = 1 AND transaction_type='S' AND " + 
                $" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}';";

            int count = 0;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    count = Convert.ToInt16(result);
                }
                CloseConnection();
            }
            return count;
        }

        //Get sales number by realtorID and date range
        public int GetDBRentByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            //Sample query
            //SELECT count(1) FROM properties WHERE realtor_id = 'realtor02' AND is_sold = 1 AND transaction_type='R' AND posted_date between '2023-05-01' AND '2024-04-30';
            string query = $"SELECT count(1) FROM properties WHERE realtor_id = '{realtorID}' AND is_sold = 1  AND transaction_type='R' AND " +
                $" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}';";

            int count = 0;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    count = Convert.ToInt16(result);
                }
                CloseConnection();
            }
            return count;
        }

        //Get sales by realtorID and group by property type
        public DataTable GetDBSalesByPropertyType(string userID, DateTime startDate, DateTime endDate)
		{
            //SELECT property_type, count(property_type) FROM properties WHERE is_sold = 1 AND realtor_id = 'realtor02' AND transaction_type='S' GROUP BY property_type;
            string query = $"SELECT property_type, count(property_type) as count FROM properties WHERE is_sold = 1 AND realtor_id = '{userID}' AND transaction_type='S' AND " +
				$" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}' " + 
			    " GROUP BY property_type";

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

                //return table
                return dt;
            }
            else
            {
                return dt;
            }
        }

        //Get rent by realtorID and group by property type
        public DataTable GetDBRentByPropertyType(string userID, DateTime startDate, DateTime endDate)
		{
            //SELECT property_type, count(property_type) FROM properties WHERE is_sold = 1 AND realtor_id = 'realtor02' AND transaction_type='R' GROUP BY property_type;
            string query = $"SELECT property_type, count(property_type) as count FROM properties WHERE is_sold = 1 AND realtor_id = '{userID}' AND transaction_type='R' AND " +
				$" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}' " +
				" GROUP BY property_type";

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

                //return table
                return dt;
            }
            else
            {
                return dt;
            }
        }

        //Get sales price by realtorID and date range
        public double GetDBSalesPriceByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            //Sample query
            ////SELECT AVG(price) FROM properties WHERE realtor_id = 'realtor02' AND is_sold = 1 AND transaction_type='S' AND posted_date between '2023-05-01' AND '2024-04-30';
            string query = $"SELECT AVG(price) FROM properties WHERE realtor_id = '{realtorID}' AND is_sold = 1 AND transaction_type='S' AND " +
                $" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}';";

            double price = 0;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    price = Convert.ToDouble(result);
                }
                CloseConnection();
            }
            return price;
        }

        //Get rent price by realtorID and date range
        public double GetDBRentPriceByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            //Sample query
            ////SELECT AVG(price) FROM properties WHERE realtor_id = 'realtor02' AND is_sold = 1 AND transaction_type='R' AND posted_date between '2023-05-01' AND '2024-04-30';
            string query = $"SELECT AVG(price) FROM properties WHERE realtor_id = '{realtorID}' AND is_sold = 1 AND transaction_type='R' AND " +
                $" posted_date between '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}';";

            double price = 0;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    price = Convert.ToDouble(result);
                }
                CloseConnection();
            }
            return price;
        }


    }
}