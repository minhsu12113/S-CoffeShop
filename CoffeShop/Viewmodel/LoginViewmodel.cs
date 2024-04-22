using CoffeShop.ExtentionCommon;
using CoffeShop.Internationalization;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.View.Dialog;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Xml.Schema;

namespace CoffeShop.Viewmodel
{
    public class LoginViewmodel : BindableBase, ICustomDialog
    {
        #region [Variable]
        private object _dialogContent;
        private bool _isOpendialog;
        private UserModel _currentUser;
        private WindowState _windowState;



        public WindowState StateWindow
        {
            get { return _windowState; }
            set { _windowState = value; OnPropertyChanged(); }
        }
        public UserModel CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(); }
        }
        public object DialogContent
        {
            get => _dialogContent;
            set { _dialogContent = value; OnPropertyChanged(); }
        }
        public bool IsOpenDialog 
        { 
            get => _isOpendialog;
            set { _isOpendialog = value; OnPropertyChanged(); }
        }

        private bool _isRemember;   
        public bool IsRemember
        {
            get { return _isRemember; }
            set { _isRemember = value; OnPropertyChanged(); }
        }

        #endregion

        #region [Command]
        public ICommand DragMoveWindowCMD { get { return new CommandHelper<Window>((w) => { return w != null; }, DragMoveWindow); } }
        public ICommand LoginCMD { get { return new CommandHelper(Login); } }
        public ICommand MiniMizedWindowCMD { get { return new CommandHelper(MiniMizedWindow); } }
        public ICommand CloseWindowCMD { get { return new CommandHelper(CloseWindow); } }
        #endregion


        public LoginViewmodel()
        {
            CurrentUser = new UserModel();
        }

        public void CloseDialog() => IsOpenDialog = false;

        public void OpenDialog(object uc = null)
        {
            if (uc != null)
                DialogContent = uc;
            IsOpenDialog = true;
        }

        public void OpenDialog(int timeSecondAutoClose, object uc = null)
        {
            if (uc != null)
            {
                if (timeSecondAutoClose > 0)
                    Task.Delay(TimeSpan.FromSeconds(timeSecondAutoClose)).ContinueWith((t, _) => CloseDialog(), null, TaskScheduler.FromCurrentSynchronizationContext());
                DialogContent = uc;
            }               
            IsOpenDialog = true;
        }

        public void DragMoveWindow(Window window) => window.DragMove();

        public void Login()
        {
            if (CurrentUser.UserName == "admin" && CurrentUser.Password == "admin")
            {
                CSGlobal.Instance.LoginWindow.Hide();
                CSGlobal.Instance.MainWindow = new MainWindow();
                CSGlobal.Instance.MainWindow.Show();
                CSGlobal.Instance.CurrentUser = CurrentUser;
                if (IsRemember) SaveRememberData();
                CSGlobal.Instance.MainViewmodel.CurrentUserNameLogin = CurrentUser.UserName;
            }
            else
            {
                OpenDialog(2, new WarningUC(StringResources.Find("ERROR_LOGIN")));
            }
        }

        public void MiniMizedWindow() => StateWindow = WindowState.Minimized;

        public void CloseWindow() => OpenDialog(new ConfirmUC("Bạn có muốn đóng ứng dụng không?", () => { App.Current.Shutdown(); }, CloseDialog));

        public void LoadRememberData()
        {
            try
            {
                var listData = new List<string>();
                using (StreamReader readtext = new StreamReader("config"))
                {                    
                    while (!readtext.EndOfStream)
                    {
                        listData.Add(MyExtention.Base64Decode(readtext.ReadLine()));
                    }
                }

                CurrentUser.UserName = listData[0];
                CurrentUser.Password = listData[1];
                Login();
            }
            catch { }            
        }

        public void SaveRememberData()
        {
            string userName = MyExtention.Base64Encode(CurrentUser.UserName);
            string password = MyExtention.Base64Encode(CurrentUser.Password);
            string finalText = userName + Environment.NewLine + password;
            File.WriteAllText("config", finalText);
        }
    }
}
