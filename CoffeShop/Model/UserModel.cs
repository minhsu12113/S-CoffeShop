using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Model
{
   public class UserModel : BindableBase
   {
        public Guid Id { get; set; } = Guid.NewGuid();

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        private string permissiom;

        public string Permission
        {
            get { return permissiom; }
            set { permissiom = value; OnPropertyChanged(); }
        }
         
    }
}
