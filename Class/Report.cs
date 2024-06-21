using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS
{
	public class Report
	{
		DB db = new DB();

		public Report() { }

        // For Sales
        public int GetSalesListingNumber(string realtorID)
        {
            return db.GetDBSalesListingNumber(realtorID);
        }

        public int GetSalesNumber(string realtorID, bool isSold)
		{
			return db.GetDBSalesNumber(realtorID,  isSold);
		}

        public double GetSalesPercentage(string realtorID, bool isSold)
        {
            return db.GetDBSalesPercentage(realtorID, isSold);
        }

        public DateTime GetSalesListingPostedDate(string realtorID, bool isFrom)
        {
            return db.GetDBSalesListingPostedDate(realtorID, isFrom);
        }

        public int GetSalesByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            return db.GetDBSalesByPeriod(realtorID, startDate, endDate);
        }

        public DataTable GetSalesByPropertyType(string realtorID)
        {
            return db.GetDBSalesByPropertyType(realtorID);
        }
        public double GetSalesPriceByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            return db.GetDBSalesPriceByPeriod(realtorID, startDate, endDate);
        }

        // For Rent
        public int GetRentListingNumber(string realtorID)
        {
            return db.GetDBRentListingNumber(realtorID);
        }

        public int GetRentNumber(string realtorID, bool isSold)
        {
            return db.GetDBRentNumber(realtorID, isSold);
        }

        public double GetRentPercentage(string realtorID, bool isSold)
        {
            return db.GetDBRentPercentage(realtorID, isSold);
        }

        public DateTime GetRentListingPostedDate(string realtorID, bool isFrom)
        {
            return db.GetDBRentListingPostedDate(realtorID, isFrom);
        }

        public int GetRentByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            return db.GetDBRentByPeriod(realtorID, startDate, endDate);
        }

        public DataTable GetRentByPropertyType(string realtorID)
        {
            return db.GetDBRentByPropertyType(realtorID);
        }
        public double GetRentPriceByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            return db.GetDBRentPriceByPeriod(realtorID, startDate, endDate);
        }
    }
}
