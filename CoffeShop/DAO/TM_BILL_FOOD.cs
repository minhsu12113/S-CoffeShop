using CoffeShop.DAO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO
{
	public class TM_BILL_FOOD
	{
		private static TM_BILL_FOOD instance;
		public static TM_BILL_FOOD Instance
		{
			get
			{
				if (instance == null)
					instance = new TM_BILL_FOOD();
				return instance;
			}
			private set => instance = value;
		}
		private TM_BILL_FOOD() { }

		public DataTable GetAll()
		{
			string query = $"EXEC TM_BILL_FOOD_GetAll";
			DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
			return dataTable;
		}

        public int Insert(int billId ,int foodId, int foodQuantity)
        {
            string query = $"EXEC TM_BILL_FOOD_Insert '{billId}', {foodId}, {foodQuantity} ";
            int res = DataProvider.Instance.ExecuteNonQuery(query);
            return res;
        }

		
    }
}
