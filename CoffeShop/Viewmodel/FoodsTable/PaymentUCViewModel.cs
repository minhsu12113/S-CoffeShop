using CoffeShop.DAO.Model;
using CoffeShop.DAO;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.Viewmodel.Base;
using CoffeShop.Viewmodel.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Viewmodel.FoodsTable
{
    public class PaymentUCViewModel : BindableBase
    {
        public PaymentUCViewModel(TableViewModel table)
        {
            CurrentTable = table;
            FoodListInTable = new List<FoodModel>();
            LoadFoodFromBill();
            Header = $"Hoá đơn bàn {CurrentTable.Name}";

            BillInfoViewModel = new List<BillInfoViewModel>()
            {
                new BillInfoViewModel()
                {
                    TotalPriceInBill = TotalPriceInBill,
                    CurrentTime = CurrentTime,
                    FoodModels = new List<FoodModel>(FoodListInTable),
                    UserPayment = CSGlobal.Instance.CurrentUser.UserName
                }
            };
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set { _header = value; OnPropertyChanged(); }
        }
        private int _totalPriceInBill;

        public int TotalPriceInBill
        {
            get { return _totalPriceInBill; }
            set { _totalPriceInBill = value; OnPropertyChanged(); }
        }

        private string _currentTime;
        public string CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; OnPropertyChanged(); }
        }

        public List<BillInfoViewModel> BillInfoViewModel { get; set; }

        public TableViewModel CurrentTable { get; set; }

        private List<FoodModel> _foodListInTable;
        public List<FoodModel> FoodListInTable
        {
            get { return _foodListInTable; }
            set
            {
                _foodListInTable = value;
                OnPropertyChanged();

            }
        }

        private void LoadFoodFromBill()
        {
            var tbBill = TM_BILL_DAO.Instance.GetAll();
            var bills = BILL.ParseDataTable(tbBill);

            var tbBillFood = TM_BILL_FOOD.Instance.GetAll();
            var billsFood = BILL_FOOD.ParseDataTable(tbBillFood);

            var dtFood = TM_FOOD_DAO.Instance.Get_All();
            var foods = FoodModel.ParseFoods(dtFood);
            var foodsModel = new List<FoodModel>();

            foreach (var item in foods)
                foodsModel.Add(new FoodModel() { Data = item });

            foreach (var bill in bills)
            {
                if (bill.ID_HoaDon == CurrentTable.IdBill && String.IsNullOrEmpty(bill.PaymentTime))
                {
                    foreach (var foodBill in billsFood)
                    {
                        var food = foodsModel.Where(f => f.Id == foodBill.IdFood).FirstOrDefault();
                        if (food != null && foodBill.IDBill == CurrentTable.IdBill)
                        {
                            food.Count = foodBill.Quantity;
                            food.TotalPrice = (food.Count * food.Price) - (food.Count * food.Discount);
                            FoodListInTable.Add(food);
                        }
                    }
                    break;
                }
            }
            CalculateTotalPriceInBill();
            CurrentTime = DateTime.Now.ToString("yyyy/MM/dd/ HH:mm:ss");
        }

        private void CalculateTotalPriceInBill()
        {
            TotalPriceInBill = 0;
            foreach (var item in FoodListInTable)
            {
                TotalPriceInBill += item.TotalPrice;
            }
        }
    }

    public class BillInfoViewModel : BindableBase
    {
        public BillInfoViewModel()
        {

        }

        private int _totalPriceInBill;
        public int TotalPriceInBill
        {
            get { return _totalPriceInBill; }
            set { _totalPriceInBill = value; OnPropertyChanged(); }
        }

        private string _currentTime;
        public string CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; OnPropertyChanged(); }
        }

        public List<FoodModel> FoodModels { get; set; }

        private string _userPayment;

        public string UserPayment
        {
            get { return _userPayment; }
            set { _userPayment = value; OnPropertyChanged(); }
        }

    }
}
