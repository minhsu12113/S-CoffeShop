using CoffeShop.Model;
using CoffeShop.Viewmodel.Base;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeShop.ExtentionCommon;
using CoffeShop.DAO;
using CoffeShop.DAO.Model;

namespace CoffeShop.Viewmodel.Food
{
    public class FoodsAddOrUpdateViewModel : BindableBase
    {
        #region [Variable]
        private FoodModel _foodCurrent;
        private CategoryModel _categoryCurrent;


        public CategoryModel CategoryCurrent
        {
            get { return _categoryCurrent; }
            set 
            { 
                _categoryCurrent = value;
                FoodCurrent.CategoryName = _categoryCurrent.Name;
                FoodCurrent.CategoryId = _categoryCurrent.Id;
                OnPropertyChanged(); 
            }
        }
        public FoodModel FoodCurrent
        {
            get { return _foodCurrent; }
            set { _foodCurrent = value; }
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set { _header = value; OnPropertyChanged(); }
        }
        #endregion

        #region [Collection]
        private List<CategoryModel> _categoryList;

        public List<CategoryModel> CategoryList
        {
            get { return _categoryList; }
            set { _categoryList = value; OnPropertyChanged(); }
        }

        #endregion

        #region [Command]
        public ICommand OpenDialogChooseImageCMD { get { return new CommandHelper(OpenDialogChooseImage); } }
        public ICommand CloseDialogParentCMD { get { return new CommandHelper(CloseDialogParent.Invoke); } }
        public ICommand SaveCMD { get { return new CommandHelper(Save); } }
        #endregion

        #region [Action]
        public Action CloseDialogParent { get; set; }
        public Action<FoodModel> CallbackAddFood { get; set; }
        #endregion

        public FoodsAddOrUpdateViewModel(Action closeDialog,
            Action<FoodModel> callbackAddFood, FoodModel foodModel = null)
        {

            LoadCategoryList();
            if (foodModel != null)
            {
                FoodCurrent = foodModel.Clone();
                CategoryCurrent = CategoryList?.Where(c => c.Id == foodModel.CategoryId).FirstOrDefault();
                Header = "Chỉnh Sửa Thức Ăn";

            }
            else
            {
                FoodCurrent = new FoodModel();
                Header = "Thêm Mới Thức Ăn";
            }    

            CloseDialogParent = closeDialog;
            CallbackAddFood = callbackAddFood;            
            
        }

        public void OpenDialogChooseImage()
        {
            OpenFileDialog openFile = new OpenFileDialog() { Filter = "Image files (*.jpg, *.png) | *.jpg; *.png" };
            if (openFile.ShowDialog() == DialogResult.OK)
                FoodCurrent.ImageData = MyExtention.ConvertImageToBase64(openFile.FileName);
        }

        public void LoadCategoryList()
        {
            var dt = TM_CATELOGY_DAO.Instance.GetAll();
            var cats = CategoryModel.ParseCategoryList(dt);
            CategoryList = new List<CategoryModel>(cats);
        }

        public void Save()
        {
            if (FoodCurrent.Price <= 0)
            {
                MessageBox.Show("Giá tiền món ăn không hợp lệ!");
                return;
            }

            if (FoodCurrent.Discount > FoodCurrent.Price)
            {
                MessageBox.Show("Số tiền giảm giá không hợp lệ!");
                return;
            }

            CallbackAddFood(FoodCurrent);
            CloseDialogParent();
        }
    }
}
