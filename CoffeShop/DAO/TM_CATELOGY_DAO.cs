using CoffeShop.DAO.Model;
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
		public int Delete(CATELOGY catelogy)
		{
			string query = $"EXEC TM_CATELOGY_Delete '{catelogy.ID_Catelogy}'";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}
		public int Insert(CATELOGY catelogy)
		{
			string query = $"EXEC TM_CATELOGY_Insert N'{catelogy.Catelogy_Name}'";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}
		public int Update(CATELOGY catelogy)
		{
			string query = $"EXEC TM_CATELOGY_Update '{catelogy.ID_Catelogy}' , N'{catelogy.Catelogy_Name}'";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}


	}
}
