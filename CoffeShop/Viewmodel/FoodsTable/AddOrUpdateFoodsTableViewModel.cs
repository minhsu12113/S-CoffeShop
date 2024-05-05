using CoffeShop.DAO.Model;
using CoffeShop.DAO;
using CoffeShop.Model;
using CoffeShop.View.Dialog;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CoffeShop.ExtentionCommon;
using Newtonsoft.Json.Linq;

namespace CoffeShop.Viewmodel.FoodsTable
{
    public class AddOrUpdateFoodsTableViewModel : BindableBase
    {

        public List<FoodModel> FoodListMaster { get; set; } = new List<FoodModel>();
        private List<FoodModel> _foodList;
        public List<FoodModel> FoodList
        {
            get { return _foodList; }
            set { _foodList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<FoodModel> _foodListInTable;
        public ObservableCollection<FoodModel> FoodListInTable
        {
            get { return _foodListInTable; }
            set 
            { 
                _foodListInTable = value;
                OnPropertyChanged();

            }
        }

        private int _totalPriceInBill;
        public int TotalPriceInBill
        {
            get { return _totalPriceInBill; }
            set { _totalPriceInBill = value; OnPropertyChanged(); }
        }

        private bool _isShowTotalPriceInBill;
        public bool IsShowTotalPriceInBill
        {
            get { return _isShowTotalPriceInBill; }
            set { _isShowTotalPriceInBill = value; OnPropertyChanged(); }
        }

        private string _searchFoodContent;
        public string SearchFoodContent
        {
            get { return _searchFoodContent; }
            set
            {
                _searchFoodContent = value;
                SearchFood();
                OnPropertyChanged();
            }
        }

        public AddOrUpdateFoodsTableViewModel()
        {
            LoadFoodAsync();
            FoodListInTable = new ObservableCollection<FoodModel>();
        }

        public async void LoadFoodAsync()
        { 

            await Task.Delay(400);
            var rest = Task<List<FOOD>>.Factory.StartNew(() =>
            {
                var dtFood = TM_FOOD_DAO.Instance.Get_All();
                var foods = FoodModel.ParseFoods(dtFood);
                return foods;
            });


            //var dtFood = TM_FOOD_DAO.Instance.Get_All();
            //var foods = FoodModel.ParseFoods(dtFood);
            var foods = await rest;

            foreach (var item in foods)
            {
                FoodListMaster.Add(new FoodModel() { Data = item });
            }

            SearchFood();
        }

        public void AddFoodToTable(FoodModel food)
        {
            var fClone = MyExtention.CloneData<FoodModel>(food);

            // Check food if food was in table            
            var f = FoodListInTable.Where(f => f.Id == food.Id).FirstOrDefault();
            if(f == null)
            {
                fClone.Count = 1;
                fClone.TotalPrice = fClone.Count * fClone.Price;
                FoodListInTable.Add(fClone);
            }
            else
            {
                f.Count += 1;
                f.TotalPrice = f.Count * f.Price;
            }
            CalculateTotalPriceInBill();
        }

        public void RemoveFoodInTable(FoodModel food)
        {
            FoodListInTable.Remove(food);
            CalculateTotalPriceInBill();
        }

        public void DecreaseFoodInTable(FoodModel food)
        {
            food.Count -= 1;
            if(food.Count == 0)
                FoodListInTable.Remove(food);
            else
                food.TotalPrice = food.Count * food.Price;
            CalculateTotalPriceInBill();
        }

        private void CalculateTotalPriceInBill()
        {
            TotalPriceInBill = 0;
            foreach (var item in FoodListInTable)
            {
                TotalPriceInBill += item.TotalPrice;
            }
            IsShowTotalPriceInBill = FoodListInTable.Count != 0;
        }

        private void SearchFood()
        {
            var filterFoodsSearchName = String.IsNullOrEmpty(SearchFoodContent) ? FoodListMaster
                : FoodListMaster?.Where(f => f.Name.ToLower().Contains(SearchFoodContent.ToLower()));
            FoodList = new List<FoodModel>(filterFoodsSearchName);
        }
    }
}
