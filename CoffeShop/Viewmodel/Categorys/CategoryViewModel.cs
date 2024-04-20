using CoffeShop.Model;
using CoffeShop.Viewmodel.Base;
using System;
using System.Linq;
using System.Windows.Input;
using CoffeShop.View.Categorys;
using CoffeShop.View.Dialog;
using CoffeShop.Internationalization;
using System.Collections.ObjectModel;

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
            set { _nameSearch = value; OnPropertyChanged(); }
        }
        public PagingViewmodel PagingViewmodel
        {
            get { return _pagingViewmodel; }
            set { _pagingViewmodel = value; OnPropertyChanged(); }
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


        #endregion

        #region [Command]
        public ICommand AddNewCMD { get { return new CommandHelper(OpenDialogAddNew); } }
        public ICommand EditCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, OpenDialogEdit); } }
        public ICommand DeleteCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, DeleteCategory); } }
        public ICommand SearchCMD { get { return new CommandHelper(LoadCategory); } }
        #endregion

        public CategoryViewModel()
        {
            NameSearch = String.Empty;
            CategoryList = new ObservableCollection<CategoryModel>();
        }
       
        public int LoadTotalCount()
        {
            return 0;
        }

        public void LoadCategory()
        {
            PagingViewmodel.TotalCountItem = LoadTotalCount();
        }   
        
        public void SearchCategory(int pageIndex, int pageSize)
        {
           
        }   
        
        public void OpenDialogAddNew()
        {
            OpenDialog(new CategoryAddOrUpdateUC(new CateroryAddOrUpdateViewModel(AddCategoryOrEdit, CloseDialog)));           
        }

        public void OpenDialogEdit(CategoryModel category)
        {
            OpenDialog(new CategoryAddOrUpdateUC(new CateroryAddOrUpdateViewModel(AddCategoryOrEdit, CloseDialog,category)));
        }

        public void DeleteCategory(CategoryModel category)
        {
            string question = String.Format(StringResources.Find("CATEGORY_CONFIRM_DELETE"), category.Name);
            OpenDialog(new ConfirmUC(question,
                () =>
                {
                    CategoryList.Remove(category);
                    CloseDialog();

                }, CloseDialog));
            
        }

        public void AddCategoryOrEdit(CategoryModel category)
        {
            var addOrUpdateObj = CategoryList.Where(c => c.Id == category.Id)?.FirstOrDefault();
            if(addOrUpdateObj != null)
            {
                CategoryList.Remove(addOrUpdateObj);
                CategoryList.Add(category);
            }
            else
            {
                CategoryList.Add(category);
            }
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
