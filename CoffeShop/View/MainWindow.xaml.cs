using CoffeShop.Utility;
using CoffeShop.View.Dialog;
using CoffeShop.Viewmodel;
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

namespace CoffeShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CSGlobal.Instance.MainWindow = this;
            CSGlobal.Instance.MainViewmodel = this.DataContext as MainViewmodel;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            CSGlobal.Instance.MainViewmodel.OpenDialog(new ConfirmUC("Bạn có muốn đăng xuất không?", () => {
                (CSGlobal.Instance.LoginWindow.DataContext as LoginViewmodel).CurrentUser.UserName = "";
                (CSGlobal.Instance.LoginWindow.DataContext as LoginViewmodel).CurrentUser.Password = "";
                (CSGlobal.Instance.LoginWindow.DataContext as LoginViewmodel).SaveRememberData();
                this.Hide();
                CSGlobal.Instance.LoginWindow.Show();
            }, CSGlobal.Instance.MainViewmodel.CloseDialog));
        }
    }
}
