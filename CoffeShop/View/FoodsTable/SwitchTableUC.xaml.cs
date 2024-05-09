using CoffeShop.DAO;
using CoffeShop.ExtentionCommon;
using CoffeShop.Model;
using CoffeShop.Viewmodel.Base;
using CoffeShop.Viewmodel.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls; 
namespace CoffeShop.View.FoodsTable
{
    /// <summary>
    /// Interaction logic for SwitchTableUC.xaml
    /// </summary>
    public partial class SwitchTableUC : UserControl
    {
        public Action<bool> CallBackCloseDialog { get; set; }
        public SwitchTableUCModel ViewModel { get; set; }

        public SwitchTableUC(Action<bool> callBackCloseDialog, TableViewModel tableViewModel)
        {
            InitializeComponent();
            CallBackCloseDialog = callBackCloseDialog;
            DataContext = ViewModel = new SwitchTableUCModel(tableViewModel);
        }

        private void Close_Dialog_Click(object sender, RoutedEventArgs e)
        {
            CallBackCloseDialog.Invoke(false);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
            CallBackCloseDialog.Invoke(true);
        }
    }

    public class SwitchTableUCModel : BindableBase
    {
        public TableViewModel RootTable { get; set; }

        #region [Collection]
        private List<AreaModel> _areaList;
        public List<AreaModel> AreaList
        {
            get { return _areaList; }
            set { _areaList = value; OnPropertyChanged(); }
        }

        private List<TableViewModel> _tableList;
        public List<TableViewModel> TableList
        {
            get { return _tableList; }
            set { _tableList = value; OnPropertyChanged(); }
        }
        #endregion

        private AreaModel _areaSelected;
        public AreaModel AreaSelected
        {
            get { return _areaSelected; }
            set
            {
                _areaSelected = value;
                LoadTable(_areaSelected == null ? -1 : _areaSelected.Id);
                OnPropertyChanged();
            }
        }

        private TableViewModel _tableSelected;

        public TableViewModel TableSelected
        {
            get { return _tableSelected; }
            set { _tableSelected = value; OnPropertyChanged(); }
        }


        public SwitchTableUCModel(TableViewModel tableViewModel)
        {
            RootTable = tableViewModel;
            LoadArea();
        }

        public void LoadTable(int areaId)
        {
            if (areaId != -1) // -1 mean there is no area
            {
                var dt = TM_TABLE_DAO.Instance.Get_Table(areaId);
                var tables = TableViewModel.ParseTableList(dt);
                TableList = new List<TableViewModel>(tables);
                TableSelected = TableList.FirstOrDefault();
            }
        }

        private void LoadArea()
        {
            var dt = TM_AREA_DAO.Instance.Get_Area();
            var areas = AreaModel.ParseAreaList(dt);
            AreaList = new List<AreaModel>(areas);
            AreaSelected = AreaList?.FirstOrDefault();
        }

        public void Save()
        {
            var rootTableClone = ObjectCopier.Clone(RootTable);
            var targetTableClone = ObjectCopier.Clone(TableSelected);

            rootTableClone.Data.ID_Bill = targetTableClone.Data.ID_Bill;
            rootTableClone.Data.Status = targetTableClone.Data.Status;

            targetTableClone.Data.ID_Bill = RootTable.Data.ID_Bill;
            targetTableClone.Data.Status = RootTable.Data.Status;

            TM_TABLE_DAO.Instance.Update(rootTableClone.Data);
            TM_TABLE_DAO.Instance.Update(targetTableClone.Data);
        }
    }
}
