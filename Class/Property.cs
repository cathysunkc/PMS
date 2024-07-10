using Microsoft.Ajax.Utilities;
using System;
using System.Data;
using System.Transactions;
using System.Web.WebSockets;

namespace PMS
{
    //Implement the Property class

    public class Property
    {
        //Attributes with Getters and Setters
        public string PropertyID { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string PropertyType { get; set; }

        public double BedNum { get; set; }

        public double BathNum { get; set; }

        public string Area { get; set; }

        public string ParkingType { get; set; }

        public DateTime PostedDate { get; set; }

        public DateTime AvailableDate { get; set; }

        public string Description { get; set; }

        public bool IsFeatured { get; set; }

        public char TransactionType { get; set; }

        public double Price { get; set; }

        public string ImagePath { get; set; }

        public string RealtorID { get; set; }

        public bool IsSold { get; set; }

        //Constructors
        public Property() { }

        public Property(string propertyID)
        {
            this.PropertyID = propertyID;
        }

        public Property(string propertyID, string address, string zipCode, string city, string propertyType,
            double bedNum, double bathNum, string area, string parkingType,
            DateTime postedDate, DateTime availableDate,
            string description, bool isFeatured, char transactionType, double price,
            string imagePath, string realtorID, bool isSold)
        {
            this.PropertyID = propertyID;
            this.Address = address;
            this.ZipCode = zipCode;
            this.City = city;
            this.PropertyType = propertyType;
            this.BedNum = bedNum;
            this.BathNum = bathNum;
            this.Area = area;
            this.ParkingType = parkingType;
            this.PostedDate = postedDate;
            this.AvailableDate = availableDate;
            this.Description = description;
            this.IsFeatured = isFeatured;
            this.TransactionType = transactionType;
            this.Price = price;
            this.ImagePath = imagePath;
            this.RealtorID = realtorID;
            this.IsSold = isSold;
        }

        public bool IsSale()
        {
            return this.TransactionType == 'S';

        }

        public bool IsRent()
        {
            return this.TransactionType == 'R';
        }

        //Methods
        public string GetFullAddress()
        {
            return this.Address + "\n" + this.City + "\n" + this.ZipCode;
        }


        //Edited by Harry
        // New static method to get a property by ID using DB instance
        //Edited by Wilson to minus db argument
        public static Property GetPropertyByID(string propertyID)
        {
            DB db = new DB();
            return db.GetPropertyByID(propertyID);
        }

        // New static method to get featured properties using DB instance
        public static DataTable GetFeaturedProperty(DB db)
        {
            return db.GetFeaturedProperty();
        }

        // New static method to find properties using DB instance
        public static DataTable FindProperty(DB db, char transactionType, double bedNum, double bathNum, int minPrice, int maxPrice)
        {
            return db.FindProperty(transactionType, bedNum, bathNum, minPrice, maxPrice);
        }


        // New method to add the property to the database using DB instance
        public void AddProperty(DB db)
        {
            db.AddProperty(this);
        }

        // New method to update the property in the database using DB instance
        public void UpdateProperty(DB db)
        {

        }

        // New method to delete the property from the database using DB instance
        public void DeleteProperty(DB db)
        {

        }

        public class PropertyImages
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }

            public string PropertyID { get; set; }

            public int Index { get; set; }
        }


        //Records for prototype
        /*
		public Property GetPropertyByID(string propertyID)
>>>>>>> main
        {
            
            DataTable dt = (TempPropertyRecords().Select($"property_id = '{propertyID}'").CopyToDataTable());

            DataRow dr = dt.Rows[0];
            Property property = new Property(propertyID);
            property.PropertyID = propertyID;
            property.Address = dr["address"].ToString();
            property.ZipCode = dr["zip_code"].ToString();
            property.City = dr["city"].ToString();
            property.PropertyType = dr["property_type"].ToString();
            property.BedNum = Convert.ToDouble(dr["bed_num"]);
            property.BathNum = Convert.ToDouble(dr["bath_num"]);
            property.Area = Convert.ToDouble(dr["area"]);
            property.ParkingType = dr["parking_type"].ToString();
            property.PostedDate = Convert.ToDateTime(dr["posted_date"]);
            property.AvailableDate = Convert.ToDateTime(dr["available_date"]);
            property.Description = dr["description"].ToString();
            property.IsFeatured = Convert.ToBoolean(dr["is_featured"]);
            property.TransactionType = Convert.ToChar(dr["transaction_type"]);
            property.Price = Convert.ToDouble(dr["price"]);
            property.ImagePath = dr["image_path"].ToString();
            property.RealtorID = dr["realtor_id"].ToString();
            property.IsSold = Convert.ToBoolean(dr["is_sold"]);

            return property;
        }

        public DataTable GetFeaturedProperty()
        {
            DataTable dt = (TempPropertyRecords()).Select("is_featured = true AND is_sold = false").CopyToDataTable();

            return dt;
        }


        public DataTable FindProperty(char transactionType, double bedNum, double bathNum)
        {
            DataTable dt = (TempPropertyRecords()).Select("is_sold = false").CopyToDataTable();

            string query = string.Empty;

            if (transactionType != ' ')
            {
                query += $"transaction_type = '{transactionType}'";               
            }

            if (bedNum != 0)
            {
                if (query != string.Empty)
                    query += $" AND bed_num = {bedNum}";
                else
                    query += $" bed_num = {bedNum}";

            }

            if (bathNum != 0)
            {
                if (query != string.Empty)
                    query += $" AND bath_num = {bathNum}";
                else
                    query += $" bath_num = {bathNum}";
                                       
            }

            DataRow[] dr = (dt).Select(query);

            if (dr.Length > 0)
                dt = dr.CopyToDataTable();
            else if (dr.Length == 0)
                dt = new DataTable();

            return dt;
        }

        
        //Records for Prototype
        public static DataTable TempPropertyRecords()
        {
            DataTable dt = new DataTable("Properties");

            dt.Columns.Add("property_id", typeof(string));
            dt.Columns.Add("address", typeof(string));
            dt.Columns.Add("zip_code", typeof(string));
            dt.Columns.Add("city", typeof(string));
            dt.Columns.Add("property_type", typeof(string));
            dt.Columns.Add("bed_num", typeof(double));
            dt.Columns.Add("bath_num", typeof(double));
            dt.Columns.Add("area", typeof(double));
            dt.Columns.Add("parking_type", typeof(string));
            dt.Columns.Add("posted_date", typeof(DateTime));
            dt.Columns.Add("available_date", typeof(DateTime));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("is_featured", typeof(bool));
            dt.Columns.Add("transaction_type", typeof(char));
            dt.Columns.Add("price", typeof(double));
            dt.Columns.Add("image_path", typeof(string));
            dt.Columns.Add("realtor_id", typeof(string));
            dt.Columns.Add("is_sold", typeof(bool));

            dt.PrimaryKey = new DataColumn[] { dt.Columns["property_id"] };

            dt.Rows.Add("P000001", "7038 34 Avenue NW", "T3B6E8", "Calgary, AB",
                "Single Family", 2, 3, 1386, "Attached Garage (1)",
                "2024-04-01", "2024-05-01", "Welcome to Arrive at Bowness, where innovation meets elegance! This townhouse, honoured with the esteemed 2017 Mayors Urban Design Award for Housing Innovation, is a beacon of contemporary living.",
                true, 'S', 449900, "https://robbreport.com/wp-content/uploads/2022/08/The-Grand-by-Gehry_Great-Room_Image-Courtesy-of-Peter-Christiansen-Valli-for-The-Grand-by-Gehry.jpg", "realtor01", false);
            dt.Rows.Add("P000002", "7224 Bow Crescent NW", "T3B2B9", "Calgary, AB",
                "Vacant Land", 3, 3, 32000, "Underground",
                "2024-04-03", "2024-05-05", "LOCATED IN THE HIGHLY SOUGHT AFTER COMMUNITY OF LAKE MAHOGANY. 2 BED PLUS DEN, 2 BATH, TITLED UNDERGROUND PARKING, AND STORAGE LOCKER!",
                true, 'S', 379900, "https://www.idesignarch.com/wp-content/uploads/2686-Eagleridge-Drive-Coquitlam_1.jpg", "realtor01", false);
            dt.Rows.Add("P000003", "56 Martingrove Way NE", "T3J2T2", "Calgary, AB",
                "Single Family", 4, 2, 1605, "Attached Garage (2)",
                "2024-04-02", "2024-05-10", "Nestled within the vibrant community of Copperfield, this perfectly upgraded END UNIT townhome invites you to indulge in a lifestyle of convenience and style.",
                false, 'S', 450000, "https://www.homestratosphere.com/wp-content/uploads/2020/03/1-country-home-shiplap-walls-march232020-min.jpg", "realtor01", false);
            dt.Rows.Add("P000004", "15 Templegreen Road NE", "T1Y4Y9", "Calgary, AB",
                "Single Family", 4, 4, 1317, "Detached Garage (2)",
                "2024-04-02", "2024-05-30", "3 Bed 1.5 Bath House in Temple with Double Car Garage - More Photos Coming Soon!Located in the established community of Temple, this updated 1100+ square foot detached house with a double car garage is the perfect home with a variety of features. ",
                false, 'R', 2200, "https://www.real-samui-properties.com/prodimages/Villa-Ayundra---February-2015-121.jpg", "realtor01", false);
            dt.Rows.Add("P000005", "2356 Northmount Drive NW", "T2L0C1", "Calgary, AB",
               "Single Family", 2, 1, 2500, "Parking Pad",
               "2024-04-02", "2024-05-30", "Legal Suite BASEMENT with 2 bedrooms 1 bathroom - Tenants will have their separate entrance, kitchen, washer and dryer, microwave and parking space.",
               false, 'R', 1000, "https://hgtvhome.sndimg.com/content/dam/images/hgtv/fullset/2016/7/21/0/FOD16_Decor-Aid_Urban_9.jpg.rend.hgtvcom.616.493.suffix/1469125819106.jpeg", "realtor01", false);
            dt.Rows.Add("P000006", "50 Hamptons Manor NW", "T3A6K2", "Calgary, AB",
               "Single Family", 2, 1, 3400, "Attached Garage (3)",
               "2024-04-03", "2024-05-15", "Introducing a Spectacular Residence in Hamptons for Rent:Property Size: 3400 SQ FT **Breathtaking Views**: Enjoy stunning vistas of the golf course pond, adding a serene and tranquil backdrop to your daily life.",
               false, 'R', 4995, "https://cdn.britannica.com/05/157305-050-FD9CB47C.jpg", "realtor01", false);

            return dt;
        } */
    }
}