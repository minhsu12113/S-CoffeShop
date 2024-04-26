﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.DAO.Model
{
	internal class BILL_FOOD
	{
		public int ID { get; set; }
		public string Name_Food { get; set; }
		public int Quantity {  get; set; }
		public int Unit_Cost {  get; set; }
		//Total của món ăn đó trong hóa đơn
		public int Total {  get; set; }
		public BILL_FOOD() { }
		BILL_FOOD(int iD, string name_Food, int quantity, int unit_Cost, int total)
		{
			ID = iD;
			Name_Food = name_Food;
			Quantity = quantity;
			Unit_Cost = unit_Cost;
			Total = total;
		}
	}
}
