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

        public int GetListingNumber(string realtorID)
        {
            return db.GetDBListingNumber(realtorID);
        }

        public int GetSalesNumber(string realtorID, bool isSold)
		{
			return db.GetDBSalesNumber(realtorID,  isSold);
		}

        public double GetSalesPercentage(string realtorID, bool isSold)
        {
            return db.GetDBSalesPercentage(realtorID, isSold);
        }

        public DateTime GetListingPostedDate(string realtorID, bool isFrom)
        {
            return db.GetDBListingPostedDate(realtorID, isFrom);
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
    }
}
