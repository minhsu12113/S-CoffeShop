using CoffeShop.DAO;
using CoffeShop.DAO.Model;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using static CoffeShop.Enums.ALL_ENUM;

namespace CoffeShop.Model
{
   public class FoodModel : BindableBase
   {
        public FoodModel() { }
        public FoodModel(FOOD fOOD) 
        {
            this.Data = fOOD;
        }

        public FOOD Data { get; set; } = new FOOD();

        public int Id 
        {
            get { return Data.ID; }
            set { Data.ID = value; OnPropertyChanged(); }
        }
        public int CategoryId
        {
            get { return Data.ID_Catelogy; }
            set { Data.ID_Catelogy = value; OnPropertyChanged(); }
        }

        public String Name
        {
            get { return Data.Food_Name; }
            set { Data.Food_Name = value; OnPropertyChanged(); }
        }
        
        public int Price
        {
            get { return Data.Unit_Cost; }
            set { Data.Unit_Cost = value; OnPropertyChanged(); }
        }

        public int PriceAfterDiscount => Price - Discount;


        private int _totalPrice;
        public int TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; OnPropertyChanged(); }
        }

        public int Discount
        {
            get { return Data.Discount; }
            set { Data.Discount = value; OnPropertyChanged(); }
        }

        private string _catName;
        public String CategoryName
        {
            get { return _catName; }
            set { _catName = value; OnPropertyChanged(); }
        }

        public String ImageData
        {
            get { return Data.Base64_Image; }
            set { Data.Base64_Image = value; OnPropertyChanged(); }
        }
        private int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); }
        }

        public static FoodModel ParseFood(DataTable dt)
        {
            try
            {
                FoodModel food = null;
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        food = new FoodModel();
                        food.Id = int.Parse(dt.Rows[i]["ID_Food"].ToString());
                        food.Name = dt.Rows[i]["Food_Name"].ToString();
                        food.Price = int.Parse(dt.Rows[i]["Unit_Cost"].ToString());
                        food.Discount = int.Parse(dt.Rows[i]["Discount"].ToString());
                        food.CategoryId = int.Parse(dt.Rows[i]["ID_Catelogy"].ToString());
                        food.ImageData = dt.Rows[i]["Base64_Image"].ToString();
                    }
                    return food;
                }
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public static List<FOOD> ParseFoods(DataTable dt)
        {
            try
            {
                var foods = new List<FOOD>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        FOOD food = new FOOD();
                        food.ID = int.Parse(dt.Rows[i]["ID_Food"].ToString());
                        food.Food_Name = dt.Rows[i]["Food_Name"].ToString();
                        food.Unit_Cost = int.Parse(dt.Rows[i]["Unit_Cost"].ToString());
                        food.Discount = int.Parse(dt.Rows[i]["Discount"].ToString());
                        food.ID_Catelogy = int.Parse(dt.Rows[i]["ID_Catelogy"].ToString());
                        food.Base64_Image = dt.Rows[i]["Base64_Image"].ToString();
                        foods.Add(food);
                    }
                }
                return foods;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return new List<FOOD>();
            }
        } 
   }
}
