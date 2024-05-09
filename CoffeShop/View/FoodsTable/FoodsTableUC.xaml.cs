using CoffeShop.Viewmodel.Categorys;
using CoffeShop.Viewmodel.DashBoard;
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

namespace CoffeShop.View.FoodsTable
{
    /// <summary>
    /// Interaction logic for FoodsTableUC1.xaml
    /// </summary>
    public partial class FoodsTableUC : UserControl
    {
        public FoodsTebleViewModel ViewModel { get; set; }
        public FoodsTableUC()
        {
            InitializeComponent();
            DataContext = ViewModel = new FoodsTebleViewModel();
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddOrUpdateFoodTabel((sender as Button).DataContext as TableViewModel);
        }

        private void btn_switch_click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenDialog(new SwitchTableUC(ViewModel.CloseDialog, (sender as Button).DataContext as TableViewModel));
        }

        private void btn_Payment_click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenDialog(new PaymentUC(ViewModel.CloseDialog, (sender as Button).DataContext as TableViewModel));
        }
    }
}
