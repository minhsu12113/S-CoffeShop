using CoffeShop.DAO;
using CoffeShop.ExtentionCommon;
using CoffeShop.Model;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.Categorys
{
    public class AreaAddOrUpdateViewModel : BindableBase
    {
        public ICommand AddTableCMD { get { return new CommandHelper(AddTable); } }
        public ICommand SaveAreaCMD { get { return new CommandHelper(SaveArea); } }
        public ICommand CloseDialogCMD { get { return new CommandHelper(() => CloseDialogParent.Invoke()); } }
        public Action<AreaModel, List<TableViewModel>> AddAreaOrEditCategoryCallback { get; set; }
        public Action CloseDialogParent { get; set; } 

        private AreaModel _currentArea;
        public AreaModel CurrentArea
        {
            get { return _currentArea; }
            set { _currentArea = value; OnPropertyChanged(); }
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set { _header = value; OnPropertyChanged(); }
        }

        public List<TableViewModel> TableListMaster { get; set; } = new List<TableViewModel>();
        private ObservableCollection<TableViewModel> _tableList;
        public ObservableCollection<TableViewModel> TableList
        {
            get { return _tableList; }
            set { _tableList = value; }
        }

        public AreaAddOrUpdateViewModel(Action<AreaModel, List<TableViewModel>> addAreaOrEditCategoryCallback,
            Action closeDialog, List<TableViewModel> tables = null , AreaModel area = null) 
        {
            AddAreaOrEditCategoryCallback = addAreaOrEditCategoryCallback;
            CloseDialogParent = closeDialog;
            if (area == null)
            {
                Header = "Thêm Mới Khu Vực";
                TableListMaster = new List<TableViewModel>() 
                { 
                    new TableViewModel() { Name = "Ban 1"},
                    new TableViewModel() { Name = "Ban 2"},
                };

                TableList = new ObservableCollection<TableViewModel>(TableListMaster);
                CurrentArea = new AreaModel();
            }

            if(area != null)
            {
                Header = "Chỉnh Sửa Khu Vực";
                CurrentArea = MyExtention.CloneData<AreaModel>(area);
                var tablesClone = ObjectCopier.Clone<List<TableViewModel>>(tables);
                TableListMaster = new List<TableViewModel>(tablesClone);
                TableList = new ObservableCollection<TableViewModel>(TableListMaster);
            }
        }

        public void AddTable()
        {
            var table = new TableViewModel() { Name = $"Ban {TableList.Count + 1}" };
            TableList.Add(table);
            TableListMaster.Add(table);
        }

        public void RemoveTable(TableViewModel table)
        {
            TableList.Remove(table);
            table.IsDelete = true;
        }

        public void SaveArea() => AddAreaOrEditCategoryCallback.Invoke(CurrentArea, TableListMaster);
    }

    public class TableViewModel : BindableBase
    {
        public TABLE Data { get; set; } = new TABLE();
        
        public int Id
        {
            get { return Data.ID_Table; }
            set { Data.ID_Table = value; OnPropertyChanged(); }
        }
         
        public string Name
        {
            get { return Data.Table_Name; }
            set { Data.Table_Name = value; OnPropertyChanged(); }
        }

        public int AreaId
        {
            get { return Data.ID_Area; }
            set { Data.ID_Area = value; OnPropertyChanged(); }
        }

        public string Status
        {
            get { return Data.Status; }
            set { Data.Status = value; OnPropertyChanged(); }
        }

        public int IdBill
        {
            get { return Data.ID_Bill; }
            set { Data.ID_Bill = value; OnPropertyChanged(); }
        }
        public bool IsUpdateFromDb { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public bool IsActive => Status == "ON";
        public bool IsEmpty => !IsActive;

        public static TableViewModel ParseTable(DataTable dt)
        {
            TableViewModel table = null;
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    table = new TableViewModel();
                    table.Id = int.Parse(dt.Rows[i]["ID_Table"].ToString());
                    table.AreaId = int.Parse(dt.Rows[i]["ID_Area"].ToString());
                    table.Name = dt.Rows[i]["Table_Name"].ToString();
                    table.Status = dt.Rows[i]["Table_Status"].ToString();
                    table.IdBill = int.Parse(dt.Rows[i]["ID_Bill"].ToString());
                }
            }
            return table;
        }

        public static List<TableViewModel> ParseTableList(DataTable dt)
        {
            List<TableViewModel> tables = null;
            if (dt != null)
            {
                tables = new List<TableViewModel>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var table = new TableViewModel();
                    table.Id = int.Parse(dt.Rows[i]["ID_Table"].ToString());
                    table.AreaId = int.Parse(dt.Rows[i]["ID_Area"].ToString());

                    var IDBill = dt.Rows[i]["ID_Bill"].ToString();
                    if (IDBill != null)
                    {
                        int id;
                        var resParse = int.TryParse(IDBill, out id);
                        table.IdBill = resParse ? id : -1;
                    }

                    table.Name = dt.Rows[i]["Table_Name"].ToString();
                    table.Status = dt.Rows[i]["Table_Status"].ToString();
                    tables.Add(table);
                }
            }
            return tables;
        }
    }
}
