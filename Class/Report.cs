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

		public int GetSoldPropertyCount(string realtorID)
		{
			return db.CountSoldPropertyByID(realtorID);
		}
	}
}
