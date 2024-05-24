using CoffeShop.DAO;
using CoffeShop.Internationalization;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.View.Area;
using CoffeShop.View.Categorys;
using CoffeShop.View.Dialog;
using CoffeShop.Viewmodel.Base;
using CoffeShop.Viewmodel.Categorys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.Area
{
    public class AreaViewModel : BindableBase
    {
        #region [Varable]
        private object _dialogContent;
        private bool _isOpendialog;
        private PagingViewmodel _pagingViewmodel;
        private string _nameSearch;

        public string NameSearch
        {
            get { return _nameSearch; }
            set
            {
                _nameSearch = value;
                OnPropertyChanged();
                SearchArea();
            }
        }

        public object DialogContent
        {
            get { return _dialogContent; }
            set { _dialogContent = value; OnPropertyChanged(); }
        }
        public bool IsOpenDialog
        {
            get { return _isOpendialog; }
            set { _isOpendialog = value; OnPropertyChanged(); }
        }
        #endregion

        #region [Collection]
        private List<AreaModel> _areaList;
        public List<AreaModel> AreaList
        {
            get { return _areaList; }
            set { _areaList = value; OnPropertyChanged(); }
        } 

        public List<AreaModel> MastetAreaList { get; set; }
        #endregion

        #region [Command]
        public ICommand AddNewCMD { get { return new CommandHelper(OpenDialogAddNew); } }
        public ICommand EditCMD { get { return new CommandHelper<AreaModel>((c) => { return c != null; }, OpenDialogEdit); } }
        public ICommand DeleteCMD { get { return new CommandHelper<AreaModel>((c) => { return c != null; }, DeleteArea); } }
        public ICommand SearchCMD { get { return new CommandHelper(LoadArea); } }
        #endregion

        public AreaViewModel()
        {
            AreaList = new List<AreaModel>();
            MastetAreaList = new List<AreaModel>();
            LoadArea();
        }

        public void SearchArea()
        {
            if (String.IsNullOrEmpty(NameSearch))
            {
                AreaList = new List<AreaModel>(MastetAreaList);
                return;
            }
            var searchData = MastetAreaList.Where(c => c.Name.ToLower().Contains(NameSearch.ToLower())).ToList();
            AreaList = new List<AreaModel>(searchData);
        }

        public void OpenDialogAddNew()
        {
            OpenDialog(new AreaAddOrUpdateUC(new AreaAddOrUpdateViewModel(AddAreaOrEdit, CloseDialog)));
        }

        public void OpenDialogEdit(AreaModel area)
        {
            var dt = TM_TABLE_DAO.Instance.Get_Table(area.Id);
            var tables = TableViewModel.ParseTableList(dt);
            tables?.ForEach(t => t.IsUpdateFromDb = true);
            OpenDialog(new AreaAddOrUpdateUC(new AreaAddOrUpdateViewModel(AddAreaOrEdit, CloseDialog, tables, area)));
        }

        public void DeleteArea(AreaModel area)
        {

            var dt = TM_TABLE_DAO.Instance.Get_Table(area.Id);
            var tables = TableViewModel.ParseTableList(dt);

            var isCannotDelete = tables?.Where(t => t.Status == "ON").FirstOrDefault() != null;
            if(isCannotDelete)
            {
                MessageBox.Show("Không thể xoá khu vực này vì có bàn chưa thanh toán!");
                return;
            }

            string question = String.Format(StringResources.Find("CATEGORY_CONFIRM_DELETE"), area.Name);
            OpenDialog(new ConfirmUC(question,
                () =>
                { 
                    TM_AREA_DAO.Instance.Delete(area.Data);
                    LoadArea();
                    CloseDialog();

                }, CloseDialog));

        }

        public void AddAreaOrEdit(AreaModel area, List<TableViewModel> tables)
        {

            if (String.IsNullOrEmpty(area.Name))
            {
                MessageBox.Show("Tên khu vực không được để trống!");
                return;
            }


            var isEmptyTableName = tables.Where(t => String.IsNullOrEmpty(t.Name) == true).FirstOrDefault() != null;
            if (isEmptyTableName)
            {
                MessageBox.Show("Tên bàn không được để trống!");
                return;
            }

            var addOrUpdateObj = MastetAreaList.Where(c => c.Id == area.Id)?.FirstOrDefault();
            if (addOrUpdateObj != null)
            {
                TM_AREA_DAO.Instance.Update(area.Data); 
                CSGlobal.Instance.MainWindow.Notify("Cập Nhật Khu Vực Thành Công");
            }
            else
            {
                var isSameName = MastetAreaList.Where(a => a.Name.ToLower() == area.Name.ToLower()).FirstOrDefault() != null;
                if (isSameName)
                {
                    MessageBox.Show("Tên khu vực đã tồn tại!");
                    return;
                }

                TM_AREA_DAO.Instance.Insert(area.Data);
                var td = TM_AREA_DAO.Instance.Get_Area();
                area = AreaModel.ParseAreaList(td).Where(a => a.Name == area.Name).FirstOrDefault();
                CSGlobal.Instance.MainWindow.Notify("Thêm Khu Vực Thành Công");
            }

            foreach (var table in tables)
            { 
                table.AreaId = area.Id;
                if(table.IsDelete)
                {
                    TM_TABLE_DAO.Instance.Delete(table.Data);
                    continue;
                }

                if (table.IsUpdateFromDb)
                    TM_TABLE_DAO.Instance.Update(table.Data);
                else
                    TM_TABLE_DAO.Instance.Insert(table.Data);
            }
            CloseDialog();
            LoadArea();
        }

        private void LoadArea()
        {
            var dt = TM_AREA_DAO.Instance.Get_Area();
            var areas = AreaModel.ParseAreaList(dt);
            MastetAreaList = new List<AreaModel>(areas);
            AreaList = new List<AreaModel>(MastetAreaList);
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

        public int GetNextIdArea() => MastetAreaList.Count == 0 ? 1 : MastetAreaList.Max(c => c.Id);
    }
}
