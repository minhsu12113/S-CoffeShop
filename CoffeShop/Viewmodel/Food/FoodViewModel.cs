using CoffeShop.DAO;
using CoffeShop.DAO.Model;
using CoffeShop.Model;
using CoffeShop.View.Dialog;
using CoffeShop.View.Foods;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.Food
{
    public class FoodViewModel : BindableBase, ICustomDialog
    {
        #region [Variable]
        private object _dialogContent;
        private bool _isOpenDialog;
        private PagingViewmodel _pagingViewmodel;
        private string _searchFoodContent;
        private bool _isWaitLoadData;


        public bool IsWaitLoadData
        {
            get { return _isWaitLoadData; }
            set { _isWaitLoadData = value; OnPropertyChanged(); }
        }
        public string SearchFoodContent
        {
            get { return _searchFoodContent; }
            set { _searchFoodContent = value; OnPropertyChanged(); }
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
            get { return _isOpenDialog; }
            set { _isOpenDialog = value; OnPropertyChanged(); }
        }
        #endregion
        #region [Collection]
        private List<FoodModel> _foodList;
        private List<CategoryModel> _categoryList;
        private List<Guid> _categoryFilterList;


        public List<Guid> CategoryFilterList
        {
            get { return _categoryFilterList; }
            set { _categoryFilterList = value; OnPropertyChanged(); }
        }
        public List<CategoryModel> CategoryList
        {
            get { return _categoryList; }
            set { _categoryList = value; OnPropertyChanged(); }
        }
        public List<FoodModel> FoodList
        {
            get { return _foodList; }
            set { _foodList = value; OnPropertyChanged(); }
        }
        #endregion
        #region [Command]
        public ICommand AddNewCMD { get { return new CommandHelper(AddNew); } }
        public ICommand EditCMD { get { return new CommandHelper<FoodModel>((f) => { return f != null; }, Edit); } }
        public ICommand DeleteCMD { get { return new CommandHelper<FoodModel>((f) => { return f != null; }, Delete); } }
        public ICommand SearchCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, LoadFoodAsync); } }
        public ICommand CheckedCategoryTagCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, CheckedCategoryTag); } }
        public ICommand UnCheckedCategoryTagCMD { get { return new CommandHelper<CategoryModel>((c) => { return c != null; }, UnCheckedCategoryTag); } }
        #endregion

        public FoodViewModel()
        {
            SearchFoodContent = string.Empty;
            LoadFoodAsync();
        }

        public void AddNew()
        {
            OpenDialog(new FoodsAddOrUpdateUC(new FoodsAddOrUpdateViewModel(CloseDialog, AddOrUpdateFood)));
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

        public void LoadFoodAsync(CategoryModel category = null)
        {
            OpenDialog(new WaitingDialogUc());

            Task.Delay(400);
            Task.Run(() =>
            {
                if(CategoryList == null)
                {
                    var dtCat = TM_CATELOGY_DAO.Instance.GetAll();
                    var cats = CategoryModel.ParseCategoryList(dtCat);
                    CategoryList = new List<CategoryModel>(cats);
                }

                var dtFood = TM_FOOD_DAO.Instance.Get_All();
                var foods = (category == null) ? FoodModel.ParseFoods(dtFood) : FoodModel.ParseFoods(dtFood)?.Where(f => f.CategoryId == category.Id);
                FoodList = new List<FoodModel>(foods);
                CloseDialog();
            });
        }
        
        public void Edit(FoodModel foodModel)
        {
            OpenDialog(new FoodsAddOrUpdateUC(new FoodsAddOrUpdateViewModel(CloseDialog, AddOrUpdateFood, foodModel)));
        }

        public void Delete(FoodModel foodModel)
        {
            OpenDialog(new ConfirmUC($@"Bạn có muốn xóa [{foodModel.Name}] không?", () =>
            {
                TM_FOOD_DAO.Instance.Delete(foodModel.Data);
                LoadFoodAsync();
            }, CloseDialog));
        }

        public void AddOrUpdateFood(FoodModel food)
        {
            var objEdit = FoodList?.Where(f => f.Id == food.Id).FirstOrDefault();
            if(objEdit != null)
            {
                TM_FOOD_DAO.Instance.Update(food.Data);
            }
            else
            {
                TM_FOOD_DAO.Instance.Insert(food.Data);
            }

            LoadFoodAsync();
        }


        public void CheckedCategoryTag(CategoryModel category) => LoadFoodAsync(category);

        public void UnCheckedCategoryTag(CategoryModel category) => LoadFoodAsync(category);

    }
}
