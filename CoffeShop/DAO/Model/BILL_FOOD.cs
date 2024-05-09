using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO.Model
{
	public class BILL_FOOD
	{
		public int IDBill { get; set; }
		public int IdFood { get; set; }
		public string Name_Food { get; set; }
		public int Quantity {  get; set; }
		public int Unit_Cost {  get; set; }
		//Total của món ăn đó trong hóa đơn
		public int Total {  get; set; }
		public BILL_FOOD() { } 

        public static List<BILL_FOOD> ParseDataTable(DataTable dt)
        {
            List<BILL_FOOD> bill = null;
            if (dt != null)
            {
                bill = new List<BILL_FOOD>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var table = new BILL_FOOD();
                    table.IDBill = int.Parse(dt.Rows[i]["ID_Bill"].ToString());
                    table.IdFood = int.Parse(dt.Rows[i]["ID_Food"].ToString());
                    table.Quantity = int.Parse(dt.Rows[i]["Quantity"].ToString()); 
                    bill.Add(table);
                }
            }
            return bill;
        }
    }
}
