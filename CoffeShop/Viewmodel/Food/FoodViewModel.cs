using CoffeShop.DAO;
using CoffeShop.DAO.Model;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.View.Dialog;
using CoffeShop.View.Foods;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

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
            set { 
                _searchFoodContent = value;
                SearchFoods();
                OnPropertyChanged();
            }
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

        private CategoryModel _catogorySelected;
        public CategoryModel CatogorySelected
        {
            get { return _catogorySelected; }
            set
            {
                _catogorySelected = value;
                SearchFoods();
                OnPropertyChanged();
            }
        }
        #endregion

        #region [Collection]

        private List<CategoryModel> _categoryList;          
        public List<CategoryModel> CategoryList
        {
            get { return _categoryList; }
            set { _categoryList = value; OnPropertyChanged(); }
        }

        private List<FoodModel> _foodList;
        public List<FoodModel> FoodList
        {
            get { return _foodList; }
            set { _foodList = value; OnPropertyChanged(); }
        }

        public List<FoodModel> FoodListMaster { get; set; } = new List<FoodModel>();
        #endregion

        #region [Command]
        public ICommand AddNewCMD { get { return new CommandHelper(AddNew); } }
        public ICommand EditCMD { get { return new CommandHelper<FoodModel>((f) => { return f != null; }, Edit); } }
        public ICommand DeleteCMD { get { return new CommandHelper<FoodModel>((f) => { return f != null; }, Delete); } }
        #endregion

        public FoodViewModel()  
        {
            LoadCategory();
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

        public async void LoadFoodAsync()
        {
            OpenDialog(new WaitingDialogUc());

            await Task.Delay(400);
            var rest = Task<List<FOOD>>.Factory.StartNew(() =>
            {
                var dtFood = TM_FOOD_DAO.Instance.Get_All();
                var foods = FoodModel.ParseFoods(dtFood);
                return foods;
            });


            //var dtFood = TM_FOOD_DAO.Instance.Get_All();
            //var foods = FoodModel.ParseFoods(dtFood);
            FoodListMaster.Clear();
            var foods = await rest;

            foreach (var item in foods)
            {
                FoodListMaster.Add(new FoodModel() { Data = item });
            }

            var filterFoodsBycat = CatogorySelected.IsDefault ? FoodListMaster
                : FoodListMaster.Where(f => f.CategoryId == CatogorySelected.Id)?.ToList();

            var filterFoodsSearchName = String.IsNullOrEmpty(SearchFoodContent) ? filterFoodsBycat
                : filterFoodsBycat?.Where(f => f.Name.ToLower().Contains(SearchFoodContent));
            FoodList = new List<FoodModel>(filterFoodsSearchName);
            CloseDialog();
        }

        public void SearchFoods()
        { 
            var filterFoodsBycat = (bool)(CatogorySelected?.IsDefault) ? FoodListMaster
               : FoodListMaster.Where(f => f.CategoryId == CatogorySelected.Id)?.ToList();

            var filterFoodsSearchName = String.IsNullOrEmpty(SearchFoodContent) ? filterFoodsBycat
                : filterFoodsBycat?.Where(f => f.Name.ToLower().Contains(SearchFoodContent.ToLower()));
            FoodList = new List<FoodModel>(filterFoodsSearchName); 
        }

        public void LoadCategory()
        {
            var dtCat = TM_CATELOGY_DAO.Instance.GetAll();
            var cats = CategoryModel.ParseCategoryList(dtCat);
            cats.Add(new CategoryModel() { Name = "Tất Cả", IsDefault = true });

            CategoryList = new List<CategoryModel>(cats);
            CatogorySelected = CategoryList?.Where(c => c.IsDefault == true)?.FirstOrDefault();
        }
        
        public void Edit(FoodModel foodModel)
        {
            OpenDialog(new FoodsAddOrUpdateUC(new FoodsAddOrUpdateViewModel(CloseDialog, AddOrUpdateFood, foodModel)));
        }

        public void Delete(FoodModel foodModel)
        {
            OpenDialog(new ConfirmUC($@"Bạn có muốn xóa [{foodModel.Name}] không?", async () =>
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
                CSGlobal.Instance.MainWindow.Notify("Cập Nhật Thức Ăn Thành Công");
            }
            else
            {
                TM_FOOD_DAO.Instance.Insert(food.Data);
                CSGlobal.Instance.MainWindow.Notify("Thêm Mới Thức Ăn Thành Công");
            }

           LoadFoodAsync();
        }
    }
}
