using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO.Model
{
	internal class BILL
	{
		public int ID_HoaDon {  get; set; }
		public int Total {  get; set; }
		public string Table_Status {  get; set; }
		public DateTime Day_Create { get; set; }
		public DateTime Hour_Create { get; set; }
		public DateTime Hour_Export { get; set; }
		public int ID_Area { get; set; }
		public int ID_Table { get; set; }
		public string ID_User {  get; set; }
		BILL(int iD_HoaDon, int total, string table_Status, DateTime day_Create, DateTime hour_Create, DateTime hour_Export, int iD_Area, int iD_Table, string iD_User)
		{
			ID_HoaDon = iD_HoaDon;
			Total = total;
			Table_Status = table_Status;
			Day_Create = day_Create;
			Hour_Create = hour_Create;
			Hour_Export = hour_Export;
			ID_Area = iD_Area;
			ID_Table = iD_Table;
			ID_User = iD_User;
		}
		BILL() { }
	}
}
