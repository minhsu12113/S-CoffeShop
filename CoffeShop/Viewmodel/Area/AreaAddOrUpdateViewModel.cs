using CoffeShop.Model;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.Categorys
{
    public class AreaAddOrUpdateViewModel : BindableBase
    {
        public ICommand AddTableCMD { get { return new CommandHelper(AddTable); } }

        private ObservableCollection<String> _tableList;

        public ObservableCollection<String> TableList
        {
            get { return _tableList; }
            set { _tableList = value; }
        }

        public AreaAddOrUpdateViewModel(Action<AreaModel> addAreaOrEditCategoryCallback, Action closeDialog, AreaModel area = null) 
        {
            TableList = new ObservableCollection<string>() { "Ban 1", "Ban 2" };
        }

        public void AddTable()
        {
            TableList.Add($"Bàn {TableList.Count + 1}");
        }
    }
}
