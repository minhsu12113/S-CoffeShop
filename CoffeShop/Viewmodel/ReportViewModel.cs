using CoffeShop.Viewmodel.Base;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using CoffeShop.DAO;
using CoffeShop.DAO.Model;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using CoffeShop.Model;
using CoffeShop.Utility;

namespace CoffeShop.Viewmodel
{
	public class ReportViewModel : BindableBase
	{
		private DateTime _startDate;
		public DateTime StartDate
		{
			get { return _startDate; }
			set { _startDate = value; OnPropertyChanged(); }
		}

		private DateTime _endDate;
		public DateTime EndDate
		{
			get { return _endDate; }
			set { _endDate = value; OnPropertyChanged(); }
		}

		private bool _isShowReportTopSallOfWeek;
		public bool IsShowReportTopSallOfWeek
		{
			get { return _isShowReportTopSallOfWeek; }
			set { _isShowReportTopSallOfWeek = value; OnPropertyChanged(); }
		}

		private bool _isShowCustomReport;
		public bool IsShowCustomReport
		{
			get { return _isShowCustomReport; }
			set
			{
				_isShowCustomReport = value;
				IsShowEmptyCustomReport = !value;
				OnPropertyChanged();
			}
		}

		private bool _isShowPaymentRecently;
		public bool IsShowPaymentRecently
		{
			get { return _isShowPaymentRecently; }
			set
			{
				_isShowPaymentRecently = value;
				IsShowEmptyPaymentRecently = !value;
				OnPropertyChanged();
			}
		}

		public bool IsShowEmptyPaymentRecently { get; set; }
		public bool IsShowEmptyCustomReport { get; set; }

		public List<RecentlyPaymentBill> RecentlyPaymentBills { get; set; }

		private string[] _arrayDates;

		public string[] ArrayDates
		{
			get { return _arrayDates; }
			set { _arrayDates = value; OnPropertyChanged(); }
		}

		private SeriesCollection _seriesCollection;

		public SeriesCollection SeriesCollection
		{
			get { return _seriesCollection; }
			set { _seriesCollection = value; OnPropertyChanged(); }
		}

		private TopSellInWeek _topSellInWeek;
		public TopSellInWeek TopSellInWeek
		{
			get { return _topSellInWeek; }
			set { _topSellInWeek = value; }
		}

		public Func<double, string> FormatterTop10 { get; set; }
		public ReportViewModel()
		{
			FormatterTop10 = value => value.ToString("N0");
			StartDate = DateTime.Now.AddDays(-7);
			EndDate = DateTime.Now;
			LoadReport();
			LoadReportTopSellInWeek();
			LoadReportPaymentBillRecently();
		}

		//Thong ke cot
		public void LoadReport()
		{

			if (StartDate > EndDate)
			{
				MessageBox.Show("Thời gian thống kê không hợp lệ");
				return;
			}

			var deltaDay = EndDate - StartDate;
			//if (deltaDay.Days > 31)
			//{
			//	MessageBox.Show("Thời gian thống kê không được vượt quá 31 ngày");
			//	return;
			//}

			var listDate = new List<string>();
			for (int i = 0; i < deltaDay.Days + 1; i++)
				listDate.Add(StartDate.AddDays(i).ToString("MM/dd/yyyy"));
			ArrayDates = listDate.ToArray();

			var dicReport = new Dictionary<DateTime, double>();

			var strFomat = "MM/dd/yyyy";
			var chartValues = new ChartValues<double>();
			foreach (var item in ArrayDates)
			{
				DateTime filterDate = DateTime.ParseExact(item, strFomat, CultureInfo.InvariantCulture);
				dicReport.Add(filterDate, 0);
			}

			var dtbills = TM_BILL_DAO.Instance.GetAll();
			var bills = BILL.ParseDataTable(dtbills)?.Where(b => !String.IsNullOrEmpty(b.PaymentTime)).ToList(); ;

			foreach (var date in listDate)
			{
				DateTime enteredDate = DateTime.Parse(date);
				foreach (var bill in bills)
				{
					if (!String.IsNullOrEmpty(bill.PaymentTime))
					{
						DateTime billDate = DateTime.ParseExact(bill.PaymentTime, "yyyy/MM/dd/ HH:mm:ss", CultureInfo.InvariantCulture);
						if (enteredDate.Date == billDate.Date)
						{
							if (dicReport.ContainsKey(enteredDate.Date))
								dicReport[billDate.Date] += bill.Total;
						}
					}
				}
			}

			foreach (var item in ArrayDates)
			{
				DateTime billDate = DateTime.ParseExact(item, strFomat, CultureInfo.InvariantCulture);
				chartValues.Add(dicReport[billDate.Date]);
			}

			SeriesCollection = new SeriesCollection
			{
				new ColumnSeries
				{
					Values = chartValues,
					DataLabels = true,
					Title = "Doanh thu"
				}
			};
			IsShowCustomReport = bills != null && bills.Count > 0;
		}

		//Ban chay nhat trong tuan
		public void LoadReportTopSellInWeek()
		{
			var foodDict = new Dictionary<int, int>();
			var dtFood = TM_FOOD_DAO.Instance.Get_All();
			var foods = FoodModel.ParseFoods(dtFood);
			foods?.ForEach(f => foodDict[f.ID] = 0);

			var dtBillFoods = TM_BILL_FOOD.Instance.GetAll();
			var billFoods = BILL_FOOD.ParseDataTable(dtBillFoods);
			var billFoodsInWeek = new List<BILL_FOOD>();

			var dtBills = TM_BILL_DAO.Instance.GetAll();
			var bills = BILL.ParseDataTable(dtBills);

			var timeBackToOneWeek = DateTime.ParseExact(DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd/ HH:mm:ss"), "yyyy/MM/dd/ HH:mm:ss", CultureInfo.InvariantCulture);
			foreach (var bill in bills)
			{
				if (!String.IsNullOrEmpty(bill.PaymentTime))
				{
					var payTime = DateTime.ParseExact(bill.PaymentTime, "yyyy/MM/dd/ HH:mm:ss", CultureInfo.InvariantCulture);
					if (payTime >= timeBackToOneWeek)
					{
						var billFood = billFoods.Where(bf => bf.IDBill == bill.ID_HoaDon);
						billFoodsInWeek.AddRange(billFood);
					}
				}

			}

			foreach (var billFood in billFoodsInWeek)
			{
				foodDict[billFood.IdFood] += billFood.Quantity;
			}

			FoodModel food = null;
			int maxCount = 0;
			foreach (var key in foodDict.Keys)
			{
				if (foodDict[key] >= maxCount)
				{
					maxCount = foodDict[key];
					var rawFood = foods.FirstOrDefault(f => f.ID == key);
					food = new FoodModel() { Data = rawFood };
				}
			}

			IsShowReportTopSallOfWeek = food != null && maxCount > 0;
			if (IsShowReportTopSallOfWeek)
			{
				TopSellInWeek = new TopSellInWeek()
				{
					Base64Img = food.ImageData,
					Name = food.Name,
					Count = maxCount
				};
			}
		}

		//Lich su thanh toan
		public void LoadReportPaymentBillRecently()
		{
			var dtBills = TM_BILL_DAO.Instance.GetAll();
			var bills = BILL.ParseDataTable(dtBills)?.Where(b =>
			!String.IsNullOrEmpty(b.PaymentTime))?.
			ToList().OrderByDescending(b => DateTime.ParseExact(b.PaymentTime, "yyyy/MM/dd/ HH:mm:ss", CultureInfo.InvariantCulture));


			int count = 1;
			var tempList = new List<RecentlyPaymentBill>();
			foreach (var bill in bills)
			{
				tempList.Add(new RecentlyPaymentBill()
				{
					AccountCheckout = CSGlobal.Instance.CurrentUser.UserName,
					PaymentTime = bill.PaymentTime,
					TotalPrice = bill.Total
				});
				count += 1;
			}
			IsShowPaymentRecently = tempList.Count > 0;
			RecentlyPaymentBills = tempList;
		}
	}

	public class TopSellInWeek : BindableBase
	{
		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value; OnPropertyChanged(); }
		}

		private string _base64Img;
		public string Base64Img
		{
			get { return _base64Img; }
			set { _base64Img = value; }
		}

		private int _count;
		public int Count
		{
			get { return _count; }
			set { _count = value; OnPropertyChanged(); }
		}
	}

	public class RecentlyPaymentBill : BindableBase
	{
		private int _stt;
		public int STT
		{
			get { return _stt; }
			set { _stt = value; OnPropertyChanged(); }
		}

		private int _totalPrice;
		public int TotalPrice
		{
			get { return _totalPrice; }
			set { _totalPrice = value; OnPropertyChanged(); }
		}


		private string _paymentTime;
		public string PaymentTime
		{
			get { return _paymentTime; }
			set { _paymentTime = value; OnPropertyChanged(); }
		}

		private string _accountCheckout;
		public string AccountCheckout
		{
			get { return _accountCheckout; }
			set { _accountCheckout = value; OnPropertyChanged(); }
		}
	}
}
