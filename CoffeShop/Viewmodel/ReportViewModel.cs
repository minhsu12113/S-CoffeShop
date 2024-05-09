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
         
        public Func<double, string> FormatterTop10 { get; set; } 
        public ReportViewModel() 
        {
            FormatterTop10 = value => value.ToString("N0"); 
            StartDate = DateTime.Now.AddDays(-7);
            EndDate = DateTime.Now;
            LoadReport();
        }


        public void LoadReport()
        {
           
            if(StartDate > EndDate)
            {
                MessageBox.Show("Thời gian thống kê không hợp lệ");
                return;
            }

            var deltaDay = EndDate - StartDate;
            if(deltaDay.Days > 31)
            {
                MessageBox.Show("Thời gian thống kê không được vượt quá 31 ngày");
                return;
            }

            var listDate = new List<string>();
            for (int i = 0; i < deltaDay.Days + 1; i++)
                listDate.Add(StartDate.AddDays(i).ToString("MM/dd/yyyy"));
            ArrayDates = listDate.ToArray(); 

            var dicReport = new Dictionary<DateTime, double>();

            var strFomat = "MM/dd/yyyy";
            var chartValues = new ChartValues<double>();
            foreach (var item in ArrayDates)
            {
                DateTime billDate = DateTime.ParseExact(item, strFomat, CultureInfo.InvariantCulture);
                dicReport.Add(billDate,0);
            }

            var dtbills = TM_BILL_DAO.Instance.GetAll();
            var bills = BILL.ParseDataTable(dtbills);

            foreach (var date in listDate)
            {
                DateTime enteredDate = DateTime.Parse(date);
                foreach (var bill in bills)
                {
                    DateTime billDate = DateTime.ParseExact(bill.PaymentTime, "yyyy/MM/dd/ HH:mm:ss", CultureInfo.InvariantCulture);
                    if(dicReport.ContainsKey(billDate.Date))
                        dicReport[billDate.Date] += bill.Total;
                }
            }

            foreach (var item in ArrayDates)
            {
                DateTime billDate = DateTime.ParseExact(item, strFomat, CultureInfo.InvariantCulture); 
                chartValues.Add(dicReport[billDate.Date]);
            }

            SeriesCollection =  new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = chartValues,
                    DataLabels = true,
                    Title = "Doanh thu"
                }
            };
        }
    }
}
