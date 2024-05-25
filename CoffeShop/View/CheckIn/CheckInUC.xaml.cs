using CoffeShop.View.Dialog;
using CoffeShop.Viewmodel.CheckIn;
using Infragistics.Documents.Excel;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls; 

namespace CoffeShop.View.CheckIn
{
    /// <summary>
    /// Interaction logic for CheckInUC.xaml
    /// </summary>
    public partial class CheckInUC : UserControl
    {
        public CheckInViewModel ViewModel { get; set; }
        public CheckInUC()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new CheckInViewModel();
            this.Loaded += CheckInUC_Loaded;
        }

        private async void CheckInUC_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.OpenDialog(new WaitingDialogUc());
            await Task.Delay(100);
            LoadExcelFile();
        }

        private async void LoadExcelFile()
        {
            var wk = await ViewModel.LoadWorkBook();
            if(wk != null)
            {
                await App.Current.Dispatcher.BeginInvoke(new Action(() => {
                    excelControl.Workbook = wk;
                    ViewModel.IsShowExcel = true;
                    ViewModel.CloseDialog();
                }));
            }
            else
            {
                ViewModel.IsShowError = true;
                ViewModel.IsShowFunc = false;
            }
            ViewModel.CloseDialog();
        }

        private void Save_Check_In_Click(object sender, RoutedEventArgs e)
        {
            excelControl.Workbook.Save(ViewModel.FileNameExcel);
            MessageBox.Show("Đã lưu bảng chấm công!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "BangChamCong"; // Default file name
            dialog.DefaultExt = ".xlsx"; // Default file extension
            dialog.Filter = "Excel documents (.xlsx)|*.xlsx"; // Filter files by extension

            // Show save file dialog box
            bool? result = dialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dialog.FileName;
                if (File.Exists(filename)) 
                    File.Delete(filename);

                string curFile = Path.GetFullPath("BangChamCong.xlsx");                
                File.Copy(curFile, filename);

                var res = MessageBox.Show("Export Thành Công, Bạn Có Muốn Mở File Không?", "Thông Báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (res == MessageBoxResult.Yes)
                    Process.Start(filename);
            }
        }
    }
}
