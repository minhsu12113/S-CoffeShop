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
using CoffeShop.Viewmodel.FoodsTable;

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
}
