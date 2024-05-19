using CoffeShop.DAO.Model;
using CoffeShop.DAO;
using CoffeShop.Model;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CoffeShop.ExtentionCommon;
using CoffeShop.Viewmodel.Categorys;

namespace CoffeShop.Viewmodel.FoodsTable
{
    public class AddOrUpdateFoodsTableViewModel : BindableBase
    {
        public TableViewModel CurrentTable { get; set; }
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

        public int CurrentBillId { get; set; }
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

        public AddOrUpdateFoodsTableViewModel(TableViewModel currentTable)
        {
            LoadFoodAsync();
            FoodListInTable = new ObservableCollection<FoodModel>();
            CurrentTable = currentTable;
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
            var foods = await rest;

            foreach (var item in foods)
                FoodListMaster.Add(new FoodModel() { Data = item });

            SearchFood();
            LoadFoodFromBill();
        }

        public void AddFoodToTable(FoodModel food)
        {
            var fClone = MyExtention.CloneData<FoodModel>(food);

            // Check food if food was in table            
            var f = FoodListInTable.Where(f => f.Id == food.Id).FirstOrDefault();
            if(f == null)
            {
                fClone.Count = 1;
                fClone.TotalPrice = (fClone.Count * fClone.Price) - (fClone.Count *  food.Discount); 
                FoodListInTable.Add(fClone);
            }
            else
            {
                f.Count += 1;
                f.TotalPrice = (f.Count * f.Price) - (f.Discount * f.Count);
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
                food.TotalPrice = (food.Count * food.Price) - food.Discount;
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

        public void Save()
        {
            // Delelet
            TM_BILL_DAO.Instance.Delete(CurrentBillId);

            if(FoodListInTable.Count > 0)
            { 
                // Insert
                var bill = new BILL()
                {
                    ID_Area = CurrentTable.AreaId,
                    ID_Table = CurrentTable.Id,
                    ID_User = 1,
                    Table_Status = "ON"
                };
                TM_BILL_DAO.Instance.Insert(bill);
                CurrentTable.Data.Status = "ON";
                CurrentTable.Data.ID_Bill = GetBillIdAfterInsert();
                TM_TABLE_DAO.Instance.Update(CurrentTable.Data);
                foreach (var food in FoodListInTable)
                    TM_BILL_FOOD.Instance.Insert(CurrentTable.Data.ID_Bill, food.Id, food.Count);
            } 
        }

        private int GetBillIdAfterInsert()
        {
            int id = 0;
            var tb = TM_BILL_DAO.Instance.GetAll();
            var bills = BILL.ParseDataTable(tb);

            foreach (var bill in bills)
            {
                if (String.IsNullOrEmpty(bill.PaymentTime) && CurrentTable.Id == bill.ID_Table)
                {
                    id = bill.ID_HoaDon;
                    break;
                }
            }
            return id;
        }

        private void LoadFoodFromBill()
        {
            var tbBill = TM_BILL_DAO.Instance.GetAll();
            var bills = BILL.ParseDataTable(tbBill);
            var tbBillFood = TM_BILL_FOOD.Instance.GetAll();
            var billsFood = BILL_FOOD.ParseDataTable(tbBillFood);

            foreach (var bill in bills)
            {
                if (bill.ID_HoaDon == CurrentTable.IdBill && String.IsNullOrEmpty(bill.PaymentTime))
                {
                    CurrentBillId = bill.ID_HoaDon;
                    foreach (var foodBill in billsFood)
                    {
                        var food = FoodListMaster.Where(f => f.Id == foodBill.IdFood).FirstOrDefault();
                        if(food != null && foodBill.IDBill == CurrentTable.IdBill)
                        {
                            var fClone = MyExtention.CloneData<FoodModel>(food);
                            fClone.Count = foodBill.Quantity;
                            fClone.TotalPrice = (fClone.Count * fClone.Price) - (fClone.Discount * fClone.Count);
                            FoodListInTable.Add(fClone);
                        }
                    }
                    break;
                }
            }
            CalculateTotalPriceInBill();
        }
    }
}
