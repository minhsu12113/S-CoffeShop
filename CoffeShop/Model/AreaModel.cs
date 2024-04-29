using CoffeShop.DAO.Model;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Model
{
    public class AreaModel : BindableBase
    {
        public AREA Data { get; set; } = new AREA();		 
		public int  Id
		{
			get { return Data.ID_Area; }
			set { Data.ID_Area = value; OnPropertyChanged(); }
		}

        public string Name
        {
            get { return Data.Area_Name; }
            set { Data.Area_Name = value; OnPropertyChanged(); }
        }
    }
}
