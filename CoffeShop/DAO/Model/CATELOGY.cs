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
		public CATELOGY() { }
		public	CATELOGY( string catelogy_Name)
		{ 
			Catelogy_Name = catelogy_Name;
		}
	}
}
