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
        public int GetSalesListingNumber(string realtorID, DateTime startDate, DateTime endDate)
		{
            return db.GetDBSalesListingNumber(realtorID, startDate, endDate);

		}

        public int GetSalesNumber(string realtorID, bool isSold, DateTime startDate, DateTime endDate)
		{
			return db.GetDBSalesNumber(realtorID,  isSold, startDate, endDate);
		}

        public double GetSalesPercentage(string realtorID, bool isSold, DateTime startDate, DateTime endDate)
		{
            return db.GetDBSalesPercentage(realtorID, isSold, startDate, endDate);
		}

        public DateTime GetSalesListingDate(string realtorID, bool isFrom)
        {
            return db.GetDBSalesListingDate(realtorID, isFrom);
        }

        public int GetSalesByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            return db.GetDBSalesByPeriod(realtorID, startDate, endDate);
        }

        public DataTable GetSalesByPropertyType(string realtorID, DateTime startDate, DateTime endDate)
		{
            return db.GetDBSalesByPropertyType(realtorID, startDate, endDate);
		}
        public double GetSalesPriceByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            return db.GetDBSalesPriceByPeriod(realtorID, startDate, endDate);
        }

        // For Rent
        public int GetRentListingNumber(string realtorID, DateTime startDate, DateTime endDate)
		{			
            return db.GetDBRentListingNumber(realtorID, startDate, endDate);
		}

        public int GetRentNumber(string realtorID, bool isSold, DateTime startDate, DateTime endDate)
		{
            return db.GetDBRentNumber(realtorID, isSold, startDate, endDate);
		}

        public double GetRentPercentage(string realtorID, bool isSold, DateTime startDate, DateTime endDate)
		{
            return db.GetDBRentPercentage(realtorID, isSold, startDate, endDate);
        }

        public DateTime GetRentListingDate(string realtorID, bool isFrom)
        {
            return db.GetDBRentListingDate(realtorID, isFrom);
        }

        public int GetRentByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            return db.GetDBRentByPeriod(realtorID, startDate, endDate);
        }

        public DataTable GetRentByPropertyType(string realtorID, DateTime startDate, DateTime endDate)
        {
            return db.GetDBRentByPropertyType(realtorID, startDate, endDate);
		}
        public double GetRentPriceByPeriod(string realtorID, DateTime startDate, DateTime endDate)
        {
            return db.GetDBRentPriceByPeriod(realtorID, startDate, endDate);
        }
    }
}
