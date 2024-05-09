using CoffeShop.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO
{
	public class TM_AREA_DAO
	{
		private static TM_AREA_DAO instance;
		public static TM_AREA_DAO Instance
		{
			get
			{
				if (instance == null)
					instance = new TM_AREA_DAO();
				return instance;
			}
			private set => instance = value;
		}
		private TM_AREA_DAO() { }
		public DataTable Get_Area()
		{
			string query = $"exec TM_AREA_Get_All";
			DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
			return dataTable;
		}
		public int Insert(AREA area)
		{
			string query = $"EXEC TM_AREA_Insert N'{area.Area_Name}'";
			int data=DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}
		public int Update(AREA area)
		{
			string query = $"EXEC TM_AREA_Update '{area.ID_Area}', N'{area.Area_Name}'";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}

		public int Delete(AREA area)
		{
			string query = $"EXEC TM_AREA_Delete '{area.ID_Area}'";
			int data = DataProvider.Instance.ExecuteNonQuery(query);
			return data;
		}
	}
}
