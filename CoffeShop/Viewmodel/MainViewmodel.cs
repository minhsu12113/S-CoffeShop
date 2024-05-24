using CoffeShop.Viewmodel.Base;
using System.Windows;
using System.Windows.Input;
using CoffeShop.Internationalization;
using static CoffeShop.Enums.ALL_ENUM;
using CoffeShop.Model.UI;
using System.Windows.Media;
using CoffeShop.View.Category;
using CoffeShop.View.Foods;
using CoffeShop.View.Statistics;
using CoffeShop.View.User;
using CoffeShop.View.Setting;
using CoffeShop.View.Dialog;
using CoffeShop.View.FoodsTable;
using CoffeShop.View.Area;
using CoffeShop.Viewmodel.Food;
using CoffeShop.View.CheckIn;
using MaterialDesignThemes.Wpf;
using System;

namespace CoffeShop.Viewmodel
{
    public class MainViewmodel : BindableBase, ICustomDialog
    {
        #region [Vareable]
        private object _currentView;
        private WindowState _stateWindow;
        private object _dialogContent;
        private bool _isOpenDialog;
        private bool _isLoadSomeThing;



        public bool IsLoadSomeThing
        {
            get => _isLoadSomeThing;
            set { _isLoadSomeThing = value; OnPropertyChanged(); }
        }
        public object DialogContent
        {
            get => _dialogContent;
            set { _dialogContent = value; OnPropertyChanged(); }
        }
        public bool IsOpenDialog
        {
            get => _isOpenDialog;
            set { _isOpenDialog = value; OnPropertyChanged(); }
        }
        public WindowState StateWindow
        {
            get { return _stateWindow; }
            set { _stateWindow = value; OnPropertyChanged(); }
        }
        public object CurrenView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        private string _currentUserNameLogin;
        public string CurrentUserNameLogin
        {
            get { return _currentUserNameLogin; }
            set { _currentUserNameLogin = value; OnPropertyChanged(); }
        }

        #endregion

        #region [Command]        
        public ICommand NavigateToViewCMD { get { return new CommandHelper<ItemNavigate>((v) => { return v != null; }, NavigateToView); } }
        public ICommand DragmoveWindowCMD { get { return new CommandHelper<Window>((w) => { return w != null; }, DragmoveWindow); } }
        public ICommand MinimizedWindowCMD { get { return new CommandHelper(MinimizedWindow); } }
        public ICommand MaximizeWindowCMD { get { return new CommandHelper(MaximizeWindow); } }
        public ICommand ShutdownAppCMD { get { return new CommandHelper(ShutdownApp); } }
        #endregion

        public MainViewmodel()
        {
            StringResources.ApplyLanguage(Enums.ALL_ENUM.LANGUAGE.VN);
            CurrenView = new FoodsTableUC();
            StateWindow = WindowState.Normal; 
        } 
        public FoodsUC FoodsUC { get; set; }
        public AreaUC AreaUC { get; set; }
        public CategoryUC CategoryUC { get; set; }
        public StatisticsUC StatisticsUC { get; set; }
        public UserUC UserUC { get; set; } 

        public void NavigateToView(ItemNavigate itemNavigate)
        {
            HandleItemNavigateWhenChangeView(itemNavigate);

            switch (itemNavigate.TypeView)
            {
                case TYPE_VIEW.DASHBOARD:
                    CurrenView = new FoodsTableUC();
                    break;
                case TYPE_VIEW.FOODS:
                    CurrenView = new FoodsUC(); 
                    break;
                case TYPE_VIEW.AREA:
                    CurrenView = AreaUC == null ? AreaUC = new AreaUC() : AreaUC;
                    break;
                case TYPE_VIEW.CATEGORY:
                    CurrenView = CategoryUC == null ? CategoryUC = new CategoryUC() : CategoryUC;
                    break;
                case TYPE_VIEW.STATISTICS:
                    CurrenView = new StatisticsUC();
                    break;
                case TYPE_VIEW.CHECK_IN:
                    CurrenView = new CheckInUC();
                    break;
                case TYPE_VIEW.USER:
                    CurrenView = new UserUC();
                    break;
                default:
                    break;
            }
        }

        public void HandleItemNavigateWhenChangeView(ItemNavigate itemNavigate)
        {
            if (itemNavigate != null)
            {
                ItemNavigate.ListItemNavigate.ForEach((i) => { i.BackgoundItem = new SolidColorBrush(Colors.Transparent); i.ForegroundItem = new SolidColorBrush(Colors.White); i.StatePointer = Visibility.Collapsed; });
                itemNavigate.ForegroundItem = new SolidColorBrush(Color.FromRgb(51, 38, 174));
                itemNavigate.BackgoundItem = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                itemNavigate.StatePointer = Visibility.Visible;
            }
        }

        public void DragmoveWindow(Window window) => window.DragMove();

        public void MinimizedWindow() => StateWindow = WindowState.Minimized;

        public void MaximizeWindow() => StateWindow = (StateWindow == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized);

        public void ShutdownApp() => OpenDialog(new ConfirmUC("Bạn có muốn đóng ứng dụng không?", () => { App.Current.Shutdown(); }, CloseDialog));

        public void OpenDialog(object uc = null)
        {
            if (uc != null)
                DialogContent = uc;
            IsOpenDialog = true;
        }

        public void CloseDialog() => IsOpenDialog = false;
    }
}
