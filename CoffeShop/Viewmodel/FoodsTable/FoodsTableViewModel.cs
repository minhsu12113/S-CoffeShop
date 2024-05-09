using CoffeShop.DAO;
using CoffeShop.Model;
using CoffeShop.View.FoodsTable;
using CoffeShop.Viewmodel.Base;
using CoffeShop.Viewmodel.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;

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
            set 
            {
                _nameSearch = value; 
                OnPropertyChanged();
                SearchTable();
            }
        }

        private AreaModel _areaSelected;
        public AreaModel AreaSelected
        {
            get { return _areaSelected; }
            set 
            { 
                _areaSelected = value;
                LoadTable(_areaSelected == null ? -1 :_areaSelected.Id);
                OnPropertyChanged();
            }
        }

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

        private List<TableViewModel> _tableListMaster;
        public List<TableViewModel> TableListMaster
        {
            get { return _tableListMaster; }
            set { _tableListMaster = value; OnPropertyChanged(); }
        }
        #endregion

        public FoodsTebleViewModel()
        {
            LoadArea();
        }

        public  void LoadTable(int areaId)
        {
            if(areaId != -1) // -1 mean there is no area
            {
                var dt = TM_TABLE_DAO.Instance.Get_Table(areaId);
                var tables = TableViewModel.ParseTableList(dt);
                TableListMaster = new List<TableViewModel>(tables);
                TableList = new List<TableViewModel>(TableListMaster);
            }
        }

        public void SearchTable()
        {
            var filterFoodsSearchName = String.IsNullOrEmpty(NameSearch) ? TableListMaster
                : TableListMaster?.Where(f => f.Name.ToLower().Contains(NameSearch));
            TableList = new List<TableViewModel>(filterFoodsSearchName);
        }

        private void LoadArea()
        {
            var dt = TM_AREA_DAO.Instance.Get_Area();
            var areas = AreaModel.ParseAreaList(dt);
            AreaList = new List<AreaModel>(areas);
            AreaSelected = AreaList?.FirstOrDefault();
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

        public void CloseDialog(bool isReload)
        {
            IsOpenDialog = false;
            if(isReload)
                LoadTable(AreaSelected.Id);
        }

        public void AddOrUpdateFoodTabel(TableViewModel table)
        {
            OpenDialog(new AddOrUpdateFoodTabelUC(CloseDialog, table));
        }
    }
}
