using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO.Model
{
	internal class CATELOGY
	{
		public string ID_Catelogy {  get; set; }
		public string Catelogy_Name {  get; set; }
		CATELOGY() { }
		CATELOGY(string iD_Catelogy, string catelogy_Name)
		{
			ID_Catelogy = iD_Catelogy;
			Catelogy_Name = catelogy_Name;
		}
	}
}
