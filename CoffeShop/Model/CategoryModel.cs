using CoffeShop.DAO.Model;
using CoffeShop.Utility;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Model
{
    public class CategoryModel : BindableBase
    {
        public CATELOGY Data { get; set; } = new CATELOGY();

        private bool _isSelected = true; 
        public bool IsSelected 
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(); }
        }

        private int _id;
        public int Id 
        {
            get { return Data.ID_Catelogy; }
            set { Data.ID_Catelogy = value; OnPropertyChanged(); }
        }

         
        [MaxLength(39)]
        public string Name 
        {
            get { return Data.Catelogy_Name; }
            set { Data.Catelogy_Name = value; OnPropertyChanged(); }
        }


        public static CategoryModel ParseCategory(DataTable dt)
        {
            CategoryModel cat = null;
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cat = new CategoryModel();
                    cat.Id = int.Parse(dt.Rows[i]["ID_Categogy"].ToString());
                    cat.Name = dt.Rows[i]["Category_Name"].ToString(); 
                }
                return cat;
            }

            return null;
        }

        public bool IsDefault { get; set; }

        public static List<CategoryModel> ParseCategoryList(DataTable dt)
        {
            var users = new List<CategoryModel>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var cat = new CategoryModel();
                    cat.Id = int.Parse(dt.Rows[i]["ID_Catelogy"].ToString());
                    cat.Name = dt.Rows[i]["Catelogy_Name"].ToString();
                    users.Add(cat);
                }
            }
            return users;
        }
    }
}
