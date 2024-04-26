using CoffeShop.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO.Model
{
	public class FOOD
	{
		public int ID { get; set; }
		public string Food_Name { get; set; }
		public int Unit_Cost {  get; set; }
		public int Discount { get; set; }
		public int ID_Catelogy { get; set; }

		public string Base64_Image {  get; set; }
		public FOOD() { }
		FOOD(int iD, string food_Name, int unit_Cost, int discount, int iD_Catelogy, string base64_Image)
		{
			ID = iD;
			Food_Name = food_Name;
			Unit_Cost = unit_Cost;
			Discount = discount;
			ID_Catelogy = iD_Catelogy;
			Base64_Image = base64_Image;
		}
	}
}
