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
    /// Interaction logic for AddOrUpdateUser.xaml
    /// </summary>
    public partial class AddOrUpdateUserUC : UserControl
    {
        public AddOrUpdateUserUC(AddOrUpdateUserViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
