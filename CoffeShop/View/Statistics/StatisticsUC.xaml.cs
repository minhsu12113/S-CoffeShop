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

namespace CoffeShop.View.Statistics
{
    /// <summary>
    /// Interaction logic for StatisticsUC.xaml
    /// </summary>
    public partial class StatisticsUC : UserControl
    {
        public ReportViewModel  ViewModel { get; set; }
        public StatisticsUC()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new ReportViewModel();
        }

        private void btn_report_click(object sender, RoutedEventArgs e) => ViewModel.LoadReport();  
    }
}
