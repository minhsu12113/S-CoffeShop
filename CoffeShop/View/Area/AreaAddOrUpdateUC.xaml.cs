using CoffeShop.Viewmodel.Categorys;
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

namespace CoffeShop.View.Area
{
    /// <summary>
    /// Interaction logic for AreaAddOrUpdateUC.xaml
    /// </summary>
    public partial class AreaAddOrUpdateUC : UserControl
    {
        public AreaAddOrUpdateViewModel ViewModel { get; set; }
        public AreaAddOrUpdateUC(AreaAddOrUpdateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = ViewModel = viewModel;
        }

        private void TextBox_Loaded(object sender, RoutedEventArgs e) => HandleCanEdit(sender as FrameworkElement);
        private void Button_Loaded(object sender, RoutedEventArgs e) => HandleCanEdit(sender as FrameworkElement);

        private void HandleCanEdit(FrameworkElement framework)
        { 
            var dataContetx = framework.DataContext as TableViewModel;
            framework.IsEnabled = dataContetx.Status != "ON";
        }
         

        private void RemoveTable_Click(object sender, RoutedEventArgs e)
        {
            var dataContetx = (sender as FrameworkElement).DataContext as TableViewModel;
            ViewModel.RemoveTable(dataContetx); 
        }
    }
}
