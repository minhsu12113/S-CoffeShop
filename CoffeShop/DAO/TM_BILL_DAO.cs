using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO
{
	public class TM_BILL_DAO
	{
		private static TM_BILL_DAO instance;
		public static TM_BILL_DAO Instance
		{
			get
			{
				if (instance == null)
					instance = new TM_BILL_DAO();
				return instance;
			}
			private set => instance = value;
		}
		private TM_BILL_DAO() { }

		public DataTable GetAll()
		{
			string query = $"EXEC TM_BILL_GetAll";
			DataTable dataTable=DataProvider.Instance.ExecuteQuery(query);
			return dataTable;
		}

	}
}
