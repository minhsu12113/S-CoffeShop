using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO
{
	public class TABLE
	{
		public int ID_Table {  get; set; }
		public string Table_Name {  get; set; }
		public string Status {  get; set; }
		public int ID_Area {  get; set; }
		public int ID_Bill {  get; set; }
		public TABLE(int iD_Table, string table_Name, string status, int iD_Area)
		{
			ID_Table = iD_Table;
			Table_Name = table_Name;
			Status = status;
			ID_Area = iD_Area;
		}
	 	public TABLE() { }
	}
}
