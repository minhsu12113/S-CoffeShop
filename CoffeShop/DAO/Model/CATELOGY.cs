using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO.Model
{
	public class CATELOGY
	{
		public int ID_Catelogy {  get; set; }
		public string Catelogy_Name {  get; set; }
		CATELOGY() { }
		CATELOGY(int iD_Catelogy, string catelogy_Name)
		{
			ID_Catelogy = iD_Catelogy;
			Catelogy_Name = catelogy_Name;
		}
	}
}
