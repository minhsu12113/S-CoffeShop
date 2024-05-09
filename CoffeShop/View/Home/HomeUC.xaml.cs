using CoffeShop.Viewmodel.Home;
using System.Windows.Controls;

namespace CoffeShop.View.Home
{
    /// <summary>
    /// Interaction logic for HomeUC.xaml
    /// </summary>
    public partial class HomeUC : UserControl
    {
        public HomeUC()
        {
            InitializeComponent();
            DataContext = new HomeViewmodel();
        }
    }
}
