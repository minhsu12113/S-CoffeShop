using CoffeShop.Model;
using CoffeShop.Viewmodel.Base;
using System;
using System.Linq;
using System.Windows.Input;
using CoffeShop.View.Categorys;
using CoffeShop.View.Dialog;
using CoffeShop.Internationalization;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Collections.Generic;
using CoffeShop.DAO;
using CoffeShop.Utility;

namespace CoffeShop.Viewmodel.Categorys
{
   public class CategoryViewModel : BindableBase, ICustomDialog
   {
        #region [Varable]
        private object _dialogContent;
        private bool _isOpendialog;
        private PagingViewmodel _pagingViewmodel;
        private string _nameSearch;

        public string NameSearch
        {
            get { return _nameSearch; }
            set { 
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
        private ObservableCollection<CategoryModel> _categoryList;
        public ObservableCollection<CategoryModel> CategoryList
        {
            get { return _categoryList; }
            set { _categoryList = value; OnPropertyChanged(); }
        }

        public List<CategoryModel> MastetCategoryList { get; set; }
        #endregion

        #region [Command]
        public ICommand AddNewCMD { get { return new CommandHelper(OpenDialogAddNew); } }
        public ICommand EditCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, OpenDialogEdit); } }
        public ICommand DeleteCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, DeleteCategory); } }
        public ICommand SearchCMD { get { return new CommandHelper(LoadCategory); } }
        #endregion

        public CategoryViewModel()
        {
            CategoryList = new ObservableCollection<CategoryModel>();
            MastetCategoryList = new List<CategoryModel>();
            LoadCategory();
        }       
        
        public void SearchCategory()
        {
            if (String.IsNullOrEmpty(NameSearch))
            {
                CategoryList = new ObservableCollection<CategoryModel>(MastetCategoryList);
                return;
            }
                
            var searchData = MastetCategoryList.Where(c => c.Name.ToLower().Contains(NameSearch.ToLower())).ToList();
            CategoryList = new ObservableCollection<CategoryModel>(searchData);
        }   
        
        public void OpenDialogAddNew()
        {
            OpenDialog(new CategoryAddOrUpdateUC(new CateroryAddOrUpdateViewModel(AddCategoryOrEdit, CloseDialog)));           
        }

        public void OpenDialogEdit(CategoryModel category)
        {
            OpenDialog(new CategoryAddOrUpdateUC(new CateroryAddOrUpdateViewModel(AddCategoryOrEdit, CloseDialog, category)));
        }

        public void DeleteCategory(CategoryModel category)
        {
            string question = String.Format(StringResources.Find("CATEGORY_CONFIRM_DELETE"), category.Name);
            OpenDialog(new ConfirmUC(question,
                () =>
                {
                    TM_CATELOGY_DAO.Instance.Delete(category.Data);
                    LoadCategory();
                    CloseDialog();

                }, CloseDialog));
            
        }

        public void AddCategoryOrEdit(CategoryModel category)
        {
            var addOrUpdateObj = MastetCategoryList.Where(c => c.Id == category.Id)?.FirstOrDefault();
            if(addOrUpdateObj != null)
            {
                TM_CATELOGY_DAO.Instance.Update(category.Data);
                CSGlobal.Instance.MainWindow.Notify("Cập Nhật Danh Mục Thành Công");
            }
            else
            {
                TM_CATELOGY_DAO.Instance.Insert(category.Data);
                CSGlobal.Instance.MainWindow.Notify("Thêm Mới Danh Mục Thành Công");
            }
            LoadCategory();
        }

        private void LoadCategory()
        {
            var category = TM_CATELOGY_DAO.Instance.GetAll();
            var categorys = CategoryModel.ParseCategoryList(category);
            MastetCategoryList = new List<CategoryModel>(categorys);
            CategoryList = new ObservableCollection<CategoryModel>(MastetCategoryList);
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
