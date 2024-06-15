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

		public int GetSalesPropertyCount(string realtorID, bool isSold)
		{
			return db.GetDBSalesPropertyCount(realtorID,  isSold);
		}

        public double GetSalesPropertyPercentage(string realtorID, bool isSold)
        {
            return db.GetDBSalesPropertyPercentage(realtorID, isSold);
        }

        public DateTime GetPropertyPostedDate(string realtorID, bool isFrom)
        {
            return db.GetDBPropertyPostedDate(realtorID, isFrom);
        }

        public int GetSoldPropertyCountByDateRange(string realtorID, DateTime startDate, DateTime endDate)
        {
            return db.GetDBSoldPropertyCountByDateRange(realtorID, startDate, endDate);
        }

        public DataTable GetSoldPropertyByPropertyType(string realtorID)
        {
            return db.GetDBSoldPropertyByPropertyType(realtorID);
        }
    }
}
