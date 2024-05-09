using CoffeShop.DAO;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Model
{
   public class TableModel : BindableBase
   {
        public TABLE Data { get; set; } = new TABLE();

        public int Id 
        {
            get { return Data.ID_Table; }
            set { Data.ID_Table = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return Data.Table_Name; }
            set { Data.Table_Name = value; OnPropertyChanged(); }
        }

        public string Status
        {
            get { return Data.Status; }
            set { Data.Status = value; OnPropertyChanged(); }
        }

        public int IdArea
        {
            get { return Data.ID_Area; }
            set { Data.ID_Area = value; OnPropertyChanged(); }
        }

        public bool IsActive => Status == "ON";
   }
}
