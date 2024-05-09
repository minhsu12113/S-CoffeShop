using CoffeShop.DAO.Model;
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

        public int Insert(BILL bill)
        {
            string query = $"EXEC TM_BILL_Insert '{bill.Total}', '{bill.PaymentTime}', '{bill.ID_Area}', '{bill.ID_Table}', '{bill.ID_User}'";
            int res = DataProvider.Instance.ExecuteNonQuery(query);
            return res;
        }

        public int Delete(int billId)
        {
            string query = $"EXEC TM_Bill_Delete '{billId}'";
            int res = DataProvider.Instance.ExecuteNonQuery(query);
            return res;
        }

        public int Update(BILL bill) 
        {
            string query = $"EXEC [TM_BILL_Update] '{bill.ID_HoaDon}' ,'{bill.Total}', '{bill.PaymentTime}', '{bill.ID_Area}', '{bill.ID_Table}'";
            int res = DataProvider.Instance.ExecuteNonQuery(query);
            return res;
        }
    }
}
