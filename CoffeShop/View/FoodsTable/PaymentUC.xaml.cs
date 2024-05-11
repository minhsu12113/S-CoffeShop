using CoffeShop.DAO.Model;
using CoffeShop.DAO; 
using CoffeShop.Model;
using CoffeShop.Viewmodel.Categorys;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Windows;
using System.Windows.Controls; 
using System.Collections.ObjectModel;
using CoffeShop.Viewmodel.Base;
using CoffeShop.Utility;
using System.Printing;
using System.Windows.Media;

namespace CoffeShop.View.FoodsTable
{
    /// <summary>
    /// Interaction logic for PaymentUC.xaml
    /// </summary>
    public partial class PaymentUC : UserControl
    {
        public PaymentUCViewModel ViewModel { get; set; }
        public Action<bool> CloseDialogCallback { get; set; }
        public PaymentUC(Action<bool> closeDialogCallback, TableViewModel table)
        {
            InitializeComponent();
            this.DataContext = ViewModel = new PaymentUCViewModel(table);
            CloseDialogCallback = closeDialogCallback;
            this.Loaded += PaymentUC_Loaded;
        }

        private void PaymentUC_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = CSGlobal.Instance.MainWindow.Width * 0.3;
            this.Height = CSGlobal.Instance.MainWindow.Height * 0.8;
        }

        private void Close_Dialog_Click(object sender, RoutedEventArgs e)
        {
            CloseDialogCallback.Invoke(false);
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            btn_Close.Visibility = Visibility.Collapsed;
            btn_payment.Visibility = Visibility.Collapsed;
            CSGlobal.Instance.MainWindow.Hide();
            Print(this);
            btn_Close.Visibility = Visibility.Visible;
            btn_payment.Visibility = Visibility.Visible;
            CSGlobal.Instance.MainWindow.Show();

            var bill = GetBillById(ViewModel.CurrentTable.Data.ID_Bill);
            bill.Total = ViewModel.TotalPriceInBill;
            bill.PaymentTime = ViewModel.CurrentTime;
            TM_BILL_DAO.Instance.Update(bill);

            ViewModel.CurrentTable.Data.Status = "OFF";
            ViewModel.CurrentTable.Data.ID_Bill = -1;
            TM_TABLE_DAO.Instance.Update(ViewModel.CurrentTable.Data);
            CloseDialogCallback.Invoke(true);
        }

        private void Print(Visual v)
        {
            System.Windows.FrameworkElement e = v as System.Windows.FrameworkElement;
            if (e == null)
                return;

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                PageMediaSize pageSize = null;

                pageSize = new PageMediaSize(PageMediaSizeName.ISOA4Extra);

                pd.PrintTicket.PageMediaSize = pageSize;

                //store original scale
                Transform originalScale = e.LayoutTransform;
                //get selected printer capabilities
                System.Printing.PrintCapabilities capabilities = pd.PrintQueue.GetPrintCapabilities(pd.PrintTicket);

                //get scale of the print wrt to screen of WPF visual
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / e.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                               e.ActualHeight);

                //Transform the Visual to scale
                e.LayoutTransform = new ScaleTransform(scale, scale);

                //get the size of the printer page
                System.Windows.Size sz = new System.Windows.Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                //update the layout of the visual to the printer page size.
                e.Measure(sz);
                e.Arrange(new System.Windows.Rect(new System.Windows.Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                //now print the visual to printer to fit on the one page.
                pd.PrintVisual(v, "My Print");

                //apply the original transform.
                e.LayoutTransform = originalScale;
            }            
        }

        public BILL GetBillById(int id)
        {
            var tb = TM_BILL_DAO.Instance.GetAll();
            var bills = BILL.ParseDataTable(tb);

            foreach (var item in bills)
            {
                if(item.ID_HoaDon == id)
                {
                    return item;
                }
            }

            return null;
        }
    }

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
                            food.TotalPrice = food.Count * food.Price;
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
