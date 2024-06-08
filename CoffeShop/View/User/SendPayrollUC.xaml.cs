using CoffeShop.DAO;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CoffeShop.View.User
{
    /// <summary>
    /// Interaction logic for SendPayrollUC.xaml
    /// </summary>
    public partial class SendPayrollUC : UserControl
    {
        public SendPayrollUCViewModel ViewModel { get; set; }
        public Action CallbackCloseDialog { get; set; }
        public SendPayrollUC(Action callbackCloseDialog)
        {
            InitializeComponent();
            DataContext = ViewModel = new SendPayrollUCViewModel();
            CallbackCloseDialog = callbackCloseDialog;
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            CallbackCloseDialog?.Invoke();
        }

        private void SendMail_Click(object sender, RoutedEventArgs e) => SendMail();

        private void SendMail()
        {
            if (ViewModel.UserModelList != null)
            {
                var objSelectFirst = ViewModel.UserModelMasterList.FirstOrDefault(u => u.IsSelected);
                if(objSelectFirst == null)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một nhân viên!");
                    return;
                }

                string passMail = "vczr dsdh gzpf jfzg";
                string mailMaster = "hoangnguyenba1112@gmail.com";

                foreach (var user in ViewModel.UserModelList)
                {
                    if (user.IsSelected)
                    {
                        Task.Run(() => {
                            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                            {
                                var pathCheckinFile = "BangChamCong.xlsx";
                                client.EnableSsl = true;
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                client.UseDefaultCredentials = false;
                                client.Credentials = new NetworkCredential(mailMaster, passMail);
                                var mailObj = new MailMessage();
                                mailObj.From = new MailAddress(mailMaster);
                                mailObj.To.Add(user.Email);
                                mailObj.Subject = $"Payroll Coffee Shop_{user.FullName}";
                                mailObj.Body = $"Hi {user.FullName} bên dưới là file bảng lương của bạn, mọi thắc mắc xin liên hệ {mailMaster}";
                                var isFileExits = File.Exists(pathCheckinFile);
                                if (isFileExits)
                                {
                                    var file = Path.GetFullPath(pathCheckinFile);
                                    mailObj.Attachments.Add(new Attachment(file));
                                }    
                                client.Send(mailObj);
                            }
                        });
                    }
                } 
            }

            CSGlobal.Instance.MainWindow.Notify("Đã Gửi Yêu Cầu Gửi Mail!");
            CallbackCloseDialog?.Invoke();
        }
    }

    public class SendPayrollUCViewModel : BindableBase
    {

        private string _nameSearch;
        public string NameSearch
        {
            get { return _nameSearch; }
            set { _nameSearch = value; OnPropertyChanged(); SearchUser(); }
        }

        public List<UserModel> UserModelMasterList { get; set; } = new List<UserModel>();
        private ObservableCollection<UserModel> _userModelList = new ObservableCollection<UserModel>();

        public ObservableCollection<UserModel> UserModelList
        {
            get { return _userModelList; }
            set { _userModelList = value; OnPropertyChanged(); }
        }

        public SendPayrollUCViewModel()
        {
            LoadUser();
        }

        private void LoadUser()
        {
            try
            {
                UserModelMasterList.Clear();
                var dt = AccountDAO.Instance.Get_All();
                var user = this.IsAdmin ? UserModel.ParseUsers(dt)
                    : UserModel.ParseUsers(dt)?.Where(u => u.UserName == CSGlobal.Instance.CurrentUser.UserName);
                UserModelMasterList.AddRange(user);
                UserModelList = new ObservableCollection<UserModel>(UserModelMasterList);
            }
            catch
            { }
        }
        
        private void SearchUser()
        {
            var searchData = UserModelMasterList.Where(c => c.UserName.ToLower().Contains(NameSearch.ToLower())).ToList();
            UserModelList = new ObservableCollection<UserModel>(searchData);
        }
    }
}
