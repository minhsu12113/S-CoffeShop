using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.Viewmodel.FoodsTable;
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
    /// Interaction logic for AddOrUpdateFoodTabelUC.xaml
    /// </summary>
    public partial class AddOrUpdateFoodTabelUC : UserControl
    {
        public AddOrUpdateFoodsTableViewModel ViewModel { get; set; }
        public Action CloseDialogCallback { get; set; }
        public AddOrUpdateFoodTabelUC(Action closeDialog)
        {
            InitializeComponent();
            this.Loaded += AddOrUpdateFoodTabelUC_Loaded;
            this.DataContext = ViewModel = new AddOrUpdateFoodsTableViewModel();
            this.CloseDialogCallback = closeDialog;
        }

        private void AddOrUpdateFoodTabelUC_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = CSGlobal.Instance.MainWindow.Width * 0.37;
            this.Height = CSGlobal.Instance.MainWindow.Height * 0.87;
        }

        private void Close_Dialog_Click(object sender, RoutedEventArgs e) => CloseDialogCallback?.Invoke();

        private void Add_Food_To_Table_Click(object sender, RoutedEventArgs e) => ViewModel.AddFoodToTable((sender as Button).DataContext as FoodModel);

        private void Remove_Food_Click(object sender, RoutedEventArgs e) => ViewModel.RemoveFoodInTable((sender as Button).DataContext as FoodModel);

        private void Decrease_Food_InTable_Click(object sender, RoutedEventArgs e) => ViewModel.DecreaseFoodInTable((sender as Button).DataContext as FoodModel);
    }
}
