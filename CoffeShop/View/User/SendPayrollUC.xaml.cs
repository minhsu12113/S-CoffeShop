using CoffeShop.DAO;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void SendMail_Click(object sender, RoutedEventArgs e)
        {
            CallbackCloseDialog?.Invoke();
        }

        private void SendMail()
        {
            if (ViewModel.UserModelList != null)
            {
                string passMail = "";
                string mailMaster = "";

                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(mailMaster, passMail);

                    var mailObj = new MailMessage();
                    mailObj.From = new MailAddress(mailMaster);
                    foreach (var user in ViewModel.UserModelList)
                    {
                        mailObj.To.Add("su7minh@gmail.com");
                        mailObj.Subject = "Payroll Coffee Shop_su7minh";
                        mailObj.Body = "This is your Pay roll link: https://docs.google.com/spreadsheets/d/1HL66QBL7YQbVtmCp-pvbX4uzqhi5magG/edit?usp=sharing&ouid=104706678397712899729&rtpof=true&sd=true";
                    } 
                    client.Send(mailObj);
                }
            }
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
