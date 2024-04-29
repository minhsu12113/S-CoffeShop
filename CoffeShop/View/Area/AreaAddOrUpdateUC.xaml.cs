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
    }
}
