using CoffeShop.DAO;
using CoffeShop.ExtentionCommon;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoffeShop.Viewmodel.User
{
    public class ChangePassworldUCViewModel : BindableBase
    {
        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(); }
        }

        private string _reEnterPassord;
        public string ReEnterPassord
        {
            get { return _reEnterPassord; }
            set { _reEnterPassord = value; OnPropertyChanged(); }
        }

        public ChangePassworldUCViewModel(UserModel user) => CurrentUser = ObjectCopier.Clone(user);

        public void UpdatePassword()
        { 
            AccountDAO.Instance.Update(CurrentUser.Data);
            CSGlobal.Instance.MainWindow.Notify("Thay Đổi Mật Khẩu Thành Công!");
        }
    }
}
