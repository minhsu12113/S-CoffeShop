using CoffeShop.DAO.Model;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Model
{
   public class UserModel : BindableBase
   {
        public USER Data { get; set; } = new USER(); 
            
        public int Id
        {
            get { return Data.ID_User; }
            set { Data.ID_User = value; }
        }
         
        public string UserName
        {
            get { return Data.UserName; }
            set { Data.UserName = value; OnPropertyChanged(); }
        }
         
        public string Password
        {
            get { return Data.Pass; }
            set { Data.Pass = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get { return Data.PhoneNumber; }
            set { Data.PhoneNumber = value; OnPropertyChanged(); }
        }

        public string Permission
        {
            get { return Data.Position; }
            set { Data.Position = value; OnPropertyChanged(); }
        }

        public string PayrollLink
        {
            get { return Data.PayrollLink; }
            set { Data.PayrollLink = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get { return Data.Email; }
            set { Data.Email = value; OnPropertyChanged(); }
        }
         
        public string CCCD
        {
            get { return Data.CCCD; }
            set { Data.CCCD = value; OnPropertyChanged(); }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(); }
        }


        public bool IsAdminType => this.UserName == "admin" || Permission == "Quản Lý";
        public bool IsManagerType => this.Permission == "Quản Lý";

        public static UserModel ParseUser(DataTable dt)
        {
            UserModel user = null;
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    user = new UserModel();
                    user.Id = int.Parse(dt.Rows[i]["ID_User"].ToString());
                    user.UserName = dt.Rows[i]["UserName"].ToString();
                    user.PayrollLink = dt.Rows[i]["PayrollLink"].ToString();
                    user.Password = dt.Rows[i]["Pass"].ToString();
                    user.CCCD = dt.Rows[i]["CCCD"].ToString();
                    user.Email = dt.Rows[i]["Email"].ToString();
                    user.Permission = dt.Rows[i]["Posision"].ToString();
                    user.PhoneNumber = dt.Rows[i]["PhoneNumber"].ToString();
                }
                return user;
            }

            return null;
        }

        public static List<UserModel> ParseUsers(DataTable dt)
        {
            var users = new List<UserModel>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var user = new UserModel();
                    user.Id = int.Parse(dt.Rows[i]["ID_User"].ToString());
                    user.UserName = dt.Rows[i]["UserName"].ToString();
                    user.Password = dt.Rows[i]["Pass"].ToString();
                    user.CCCD = dt.Rows[i]["CCCD"].ToString();
                    user.Email = dt.Rows[i]["Email"].ToString();
                    user.PayrollLink = dt.Rows[i]["PayrollLink"].ToString();
                    user.Permission = dt.Rows[i]["Posision"].ToString();
                    user.PhoneNumber = dt.Rows[i]["PhoneNumber"].ToString();
                    users.Add(user);
                } 
            }
            return users;
        }
   }
}
