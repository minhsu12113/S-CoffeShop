using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO.Model
{
	public class AREA
	{
		public int ID_Area {  get; set; }
		public string Area_Name {  get; set; }
		public AREA() { }
		AREA(int iD_Area, string area_Name)
		{
			ID_Area = iD_Area;
			Area_Name = area_Name;
		}
	}
}
