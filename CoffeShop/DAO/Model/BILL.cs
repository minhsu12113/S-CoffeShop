using CoffeShop.Viewmodel.Categorys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO.Model
{
	public class BILL
	{
		public int ID_HoaDon {  get; set; }
		public int Total {  get; set; }
		public string Table_Status {  get; set; }
        public string PaymentTime { get; set; }
        public int ID_Area { get; set; }
		public int ID_Table { get; set; }
		public int ID_User {  get; set; }
		BILL(int iD_HoaDon, int total, string table_Status, int iD_Area, int iD_Table, int iD_User)
		{
			ID_HoaDon = iD_HoaDon;
			Total = total;
			Table_Status = table_Status; 
			ID_Area = iD_Area;
			ID_Table = iD_Table;
			ID_User = iD_User;
		}
		public BILL() { }

		public static List<BILL> ParseDataTable(DataTable dt)
		{
            List<BILL> bill = null;
            if (dt != null)
            {
                bill = new List<BILL>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var table = new BILL();
                    table.ID_HoaDon = int.Parse(dt.Rows[i]["ID_Bill"].ToString()); 
                    table.ID_Table = int.Parse(dt.Rows[i]["ID_Table"].ToString()); 
                    table.Total = int.Parse(dt.Rows[i]["Total"].ToString()); 
                    table.PaymentTime = dt.Rows[i]["PaymentTime"].ToString(); 
                    bill.Add(table);
                }
            }
            return bill; 
		}
	}
}
