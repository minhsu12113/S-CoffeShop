using CoffeShop.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Viewmodel.Base
{
    public class BindableBase : INotifyPropertyChanged
    {

        public BindableBase() 
        {
            var LoginViewModel = CSGlobal.Instance.LoginWindow?.DataContext as LoginViewmodel;
            if(LoginViewModel != null) LoginViewModel.Logged += UserViewModel_Logged; 
        }

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;       


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));           
        }

        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            member = val;
            OnPropertyChanged(propertyName);
        } 

        private void UserViewModel_Logged(object sender, LoggedEventArgs e)
        {
            OnPropertyChanged("IsAdmin");
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return CSGlobal.Instance.CurrentUser.IsAdminType; }
            set { _isAdmin = CSGlobal.Instance.CurrentUser.IsAdminType; OnPropertyChanged(); }
        }

        private bool _isManager;
        public bool IsManager
        {
            get { return CSGlobal.Instance.CurrentUser.IsManagerType; }
            set { _isManager = CSGlobal.Instance.CurrentUser.IsManagerType; OnPropertyChanged(); }
        }
    }

}
