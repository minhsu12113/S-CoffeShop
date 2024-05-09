using CoffeShop.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO
{
	public class TM_FOOD_DAO:FOOD
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
		//int id, string food_Name, int unit_Cost, int discount, string id_Catelogy, string base64_Image
		public int Update(FOOD food)
		{
			string query = $"EXEC TM_FOOD_Update '{food.ID}', N'{food.Food_Name}','{food.Unit_Cost}','{food.Discount}','{food.ID_Catelogy}','{food.Base64_Image}'";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return (int)data;
		}
		public int Insert(FOOD food)
		{
			string query = $"EXEC TM_FOOD_Insert N'{food.Food_Name}','{food.Unit_Cost}','{food.Discount}','{food.ID_Catelogy}','{food.Base64_Image}' ";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return (int)data;
		}
		public int Delete(FOOD food)
		{
			string query = $"EXEC TM_FOOD_Delete '{food.ID}' ";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return (int)data;
		}
	}
}
