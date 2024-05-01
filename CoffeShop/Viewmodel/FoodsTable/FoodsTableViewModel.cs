using CoffeShop.DAO;
using CoffeShop.DAO.Model;
using CoffeShop.ExtentionCommon;
using CoffeShop.Model;
using CoffeShop.View.Dialog;
using CoffeShop.View.FoodsTable;
using CoffeShop.Viewmodel.Base;
using CoffeShop.Viewmodel.Categorys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.DashBoard
{
    public class FoodsTebleViewModel : BindableBase,ICustomDialog
    {
        #region [Variable]
        private string _nameSearch;
        private PagingViewmodel _pagingViewmodel;
        private object _dialogContent;
        private bool _isOpenDialog;


        public object DialogContent
        {
            get { return _dialogContent; }
            set { _dialogContent = value; OnPropertyChanged(); }
        }
        public bool IsOpenDialog
        {
            get { return _isOpenDialog; }
            set { _isOpenDialog = value; OnPropertyChanged(); }
        }
        public string NameSearch
        {
            get { return _nameSearch; }
            set { _nameSearch = value; OnPropertyChanged(); }
        }

        private AreaModel _areaSelected;
        public AreaModel AreaSelected
        {
            get { return _areaSelected; }
            set 
            { 
                _areaSelected = value;
                LoadTable(_areaSelected.Id);
                OnPropertyChanged();
            }
        }

        #endregion

        #region [Command]
        public ICommand SearchCMD { get { return new CommandHelper(SearchTable); } }
        public ICommand AddOrEditFoodTabelCMD { get { return new CommandHelper<TableModel>((t) => { return t != null; }, AddOrUpdateFoodTabel); } }
        #endregion

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

        public FoodsTebleViewModel()
        {
            LoadArea();
        }

        public  void LoadTable(int areaId)
        {
            var dt = TM_TABLE_DAO.Instance.Get_Table(areaId);
            var tables = TableViewModel.ParseTableList(dt);
            TableList = new List<TableViewModel>(tables);
        }

        public void SearchTable()
        {

        }

        private void LoadArea()
        {
            var dt = TM_AREA_DAO.Instance.Get_Area();
            var areas = AreaModel.ParseAreaList(dt);
            AreaList = new List<AreaModel>(areas);
            AreaSelected = AreaList[0];
        }

        public void OpenDialog(object uc = null)
        {
            if (uc != null)
                DialogContent = uc;
            IsOpenDialog = true;
        }

        public void CloseDialog()
        {
            IsOpenDialog = false;
        }

        public void AddOrUpdateFoodTabel(TableModel table)
        {
            OpenDialog(new AddOrUpdateFoodTabelUC());
        }
    }
}
