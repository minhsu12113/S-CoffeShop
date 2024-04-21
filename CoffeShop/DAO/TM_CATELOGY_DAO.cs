using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO
{
	public class TM_CATELOGY_DAO
	{
		private static TM_CATELOGY_DAO instance;
		public static TM_CATELOGY_DAO Instance
		{
			get
			{
				if (instance == null)
					instance = new TM_CATELOGY_DAO();
				return instance;
			}
			private set => instance = value;
		}
		private TM_CATELOGY_DAO() { }

		public DataTable GetAll()
		{
			string query = $"EXEC TM_CATELOGY_GetAll";
			DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
			return dataTable;
		}

	}
}
