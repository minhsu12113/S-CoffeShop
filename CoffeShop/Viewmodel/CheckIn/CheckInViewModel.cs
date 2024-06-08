using CoffeShop.DAO;
using CoffeShop.Model;
using CoffeShop.Viewmodel.Base;
using Infragistics.Documents.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Viewmodel.CheckIn
{
    public class CheckInViewModel : BindableBase, ICustomDialog
    {
        private object _dialogContent;
        public object DialogContent
        {
            get { return _dialogContent; }
            set { _dialogContent = value; OnPropertyChanged(); }
        }
        private bool _isOpendialog;
        public bool IsOpenDialog
        {
            get { return _isOpendialog; }
            set { _isOpendialog = value; OnPropertyChanged(); }
        }

        private bool _isShowError;
        public bool IsShowError
        {
            get { return _isShowError; }
            set 
            {
                _isShowError = value; OnPropertyChanged();
                IsShowExcel = !value;
            }
        }

        private bool _isShowExcel;
        public bool IsShowExcel
        {
            get { return _isShowExcel; }
            set { _isShowExcel = value; OnPropertyChanged(); }
        }

        private bool _isShowFunc = true;
        public bool IsShowFunc
        {
            get { return _isShowFunc; }
            set { _isShowFunc = value; OnPropertyChanged(); }
        }


        public string FileNameExcel { get; set; } = "BangChamCong.xlsx";

        public Task<Workbook> LoadWorkBook()
        {
            return Task.Run(async () =>
            {
                Workbook res;
                try
                {
                    string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, FileNameExcel); //Excel/
                    res = Workbook.Load(filePath);

                    var dt = AccountDAO.Instance.Get_All();
                    
                   await App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        var users = UserModel.ParseUsers(dt)?.OrderBy(x => DateTime.ParseExact(x.DateCreate, "MM/dd/yyyy HH:mm:ss", DateTimeFormatInfo.InvariantInfo)).ToList();
                        int defaultRow = 5; 
                        int defaultValueSTT = 1;
                        foreach (var user in users)
                        {
                            var cellSTT = res.Worksheets[0].GetCell($"A{defaultRow}");
                            cellSTT.Value = defaultValueSTT;

                            var cellName = res.Worksheets[0].GetCell($"B{defaultRow}");
                            cellName.Value = user.FullName;

                            var cellPosition = res.Worksheets[0].GetCell($"C{defaultRow}");
                            cellPosition.Value = user.Permission;

                            defaultRow += 1; 
                            defaultValueSTT += 1;
                        }
                    })); 
                }
                catch (Exception ex)
                {
                    res = null;
                }
                return res;
            });
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
    }
}
