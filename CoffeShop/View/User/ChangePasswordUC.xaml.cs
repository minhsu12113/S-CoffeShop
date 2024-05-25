using CoffeShop.Model;
using CoffeShop.Viewmodel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoffeShop.View.User
{
    /// <summary>
    /// Interaction logic for ChangePasswordUC.xaml
    /// </summary>
    public partial class ChangePasswordUC : UserControl
    {
        public ChangePassworldUCViewModel ViewModel { get; set; }
        public Action<bool> CallBackCloseDialog { get; set; }
        public ChangePasswordUC(UserModel user, Action<bool> callBackCloseDialog)
        {
            InitializeComponent();
            user.Password = "";
            DataContext = ViewModel = new ChangePassworldUCViewModel(user);
            CallBackCloseDialog = callBackCloseDialog;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(ViewModel.CurrentUser.Password) || String.IsNullOrEmpty(ViewModel.CurrentUser.UserName))
            {
                MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin!");
                return;
            }

            if (ViewModel.CurrentUser.Password != ViewModel.ReEnterPassord)
            {
                MessageBox.Show("Mật khẩu không khớp!");
                return;
            }

            if (ViewModel.CurrentUser.Password.Contains(" "))
            {
                MessageBox.Show("Mật Khẩu Không Hợp Lệ!");
                return;
            }

            ViewModel?.UpdatePassword();
            CallBackCloseDialog?.Invoke(true);
        }

        private void cancle_Click(object sender, RoutedEventArgs e)
        {
            CallBackCloseDialog?.Invoke(false);
        }
    }
}
