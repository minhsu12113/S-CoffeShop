using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO
{
	public class TM_FOOD_DAO
	{
		private static TM_FOOD_DAO instance;
		public static TM_FOOD_DAO Instance
		{
			get
			{
				if (instance == null)
					instance = new TM_FOOD_DAO();
				return instance;
			}
			private set => instance = value;
		}
		private TM_FOOD_DAO() { }
		//Gọi stored procedure
		public DataTable Get_All()
		{
			string query = "EXEC TM_FOOD_Select_All ";
			DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
			return dataTable;
		}
		public DataTable Get_All_By_Catelogy(string id_Catelogy)
		{
			string query = $"EXEC TM_FOOD_Select_By_Catelogy ' {id_Catelogy} ' ";
			DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
			return dataTable;
		}
		public int Update(int id, string food_Name, int unit_Cost, int discount, string id_Catelogy, string base64_Image)
		{
			string query = "EXEC TM_FOOD_Update ";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return (int)data;
		}
		public int Insert(string food_Name, int unit_Cost, int discount, string id_Catelogy, string base64_Image)
		{
			string query = $"EXEC TM_FOOD_Insert ";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return (int)data;
		}
	}
}
