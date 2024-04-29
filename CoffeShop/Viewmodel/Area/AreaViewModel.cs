using CoffeShop.Internationalization;
using CoffeShop.Model;
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
                SearchCategory();
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
        private ObservableCollection<AreaModel> _areaList;
        public ObservableCollection<AreaModel> AreaList
        {
            get { return _areaList; }
            set { _areaList = value; OnPropertyChanged(); }
        } 

        public List<AreaModel> MastetAreaList { get; set; }
        #endregion

        #region [Command]
        public ICommand AddNewCMD { get { return new CommandHelper(OpenDialogAddNew); } }
        public ICommand EditCMD { get { return new CommandHelper<AreaModel>((c) => { return c != null; }, OpenDialogEdit); } }
        public ICommand DeleteCMD { get { return new CommandHelper<AreaModel>((c) => { return c != null; }, DeleteCategory); } }
        public ICommand SearchCMD { get { return new CommandHelper(LoadCategory); } }
        #endregion

        public AreaViewModel()
        {
            AreaList = new ObservableCollection<AreaModel>();
            MastetAreaList = new List<AreaModel>();
        }

        public int LoadTotalCount()
        {
            return 0;
        }

        public void SearchCategory()
        {
            if (String.IsNullOrEmpty(NameSearch))
            {
                AreaList = new ObservableCollection<AreaModel>(MastetAreaList);
                return;
            }
            var searchData = MastetAreaList.Where(c => c.Name.ToLower().Contains(NameSearch.ToLower())).ToList();
            AreaList = new ObservableCollection<AreaModel>(searchData);
        }

        public void OpenDialogAddNew()
        {
            OpenDialog(new AreaAddOrUpdateUC(new AreaAddOrUpdateViewModel(AddAreaOrEdit, CloseDialog)));
        }

        public void OpenDialogEdit(AreaModel area)
        {
            OpenDialog(new AreaAddOrUpdateUC(new AreaAddOrUpdateViewModel(AddAreaOrEdit, CloseDialog)));
        }

        public void DeleteCategory(AreaModel area)
        {
            string question = String.Format(StringResources.Find("CATEGORY_CONFIRM_DELETE"), area.Name);
            OpenDialog(new ConfirmUC(question,
                () =>
                {
                    MastetAreaList.Remove(area);
                    LoadCategory();
                    CloseDialog();

                }, CloseDialog));

        }

        public void AddAreaOrEdit(AreaModel area)
        {
            var addOrUpdateObj = MastetAreaList.Where(c => c.Id == area.Id)?.FirstOrDefault();
            if (addOrUpdateObj != null)
            {
                MastetAreaList.Remove(addOrUpdateObj);
                MastetAreaList.Add(area);
            }
            else
                MastetAreaList.Add(area);
            LoadCategory();
        }

        private void LoadCategory()
        {
            AreaList = new ObservableCollection<AreaModel>(MastetAreaList);
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
    }
}
