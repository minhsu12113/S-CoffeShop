using CoffeShop.ExtentionCommon;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.User
{
    public class AddOrUpdateUserViewModel : BindableBase
    {
        #region [Variable]
        private UserModel _currentUser;
        private int _maxLengthName; 
        private bool _isCanEditUserName = true;

        public bool IsCanEditUserName
        {
            get { return _isCanEditUserName; }
            set { _isCanEditUserName = value; OnPropertyChanged(); }
        }

        public int MaxLengthName
        {
            get { return _maxLengthName; }
            set { _maxLengthName = value; OnPropertyChanged(); }
        }

        public UserModel CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(); }
        }

        private string _reEnterPassord;
        public string ReEnterPassord
        {
            get { return _reEnterPassord; }
            set { _reEnterPassord = value; OnPropertyChanged(); }
        }

        private string _permison;

        public string Permision
        {
            get { return _permison; }
            set { _permison = value; OnPropertyChanged(); }
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set { _header = value; OnPropertyChanged(); }
        }

        private bool _isShowPasswordSection;
        public bool IsShowPasswordSection
        {
            get { return _isShowPasswordSection; }
            set { _isShowPasswordSection = value; OnPropertyChanged(); }
        }

        #endregion

        #region [Action]
        public Action<UserModel, bool> AddCategoryOrEditCategoryCallback { get; set; }
        public Action CloseDialogParent { get; set; }
        public bool IsEdit { get; set; }
        #endregion

        #region [Command]
        public ICommand CloseDialogCMD { get { return new CommandHelper(CloseDialogParent); } }
        public ICommand SaveCMD { get { return new CommandHelper(Save); } }
        #endregion

        #region [Collection]
        private List<string> _permisionList;
        public List<string> PermisionList
        {
            get { return _permisionList; }
            set { _permisionList = value; }
        }
        #endregion

        public AddOrUpdateUserViewModel(Action<UserModel, bool> addUserOrEditCategoryCallback, Action closeDialog, UserModel user = null)
        {
            PermisionList = new List<string>() { "Nhân Viên", "Quản Lý" };
            IsCanEditUserName = true;
            if (user == null) // Add new
            {
                CurrentUser = new UserModel(); 
                Permision = PermisionList[0];
                Header = "Thêm Mới Tài Khoản";
                IsShowPasswordSection = true;
            }
            else  // Edit
            {
                CurrentUser = user.Clone();
                ReEnterPassord = CurrentUser.Password;
                var temper = PermisionList.Where(p => p == CurrentUser.Permission).FirstOrDefault();
                Permision = String.IsNullOrEmpty(temper) ? PermisionList[0] : temper;
                IsCanEditUserName = false;
                Header = "Chỉnh Sửa Tài Khoản";
                IsShowPasswordSection = false;
                IsEdit = true;
            }
             
            AddCategoryOrEditCategoryCallback = addUserOrEditCategoryCallback;
            CloseDialogParent = closeDialog;
        }

        public void Save()
        {
            if (!IsEdit)
            {
                if (String.IsNullOrEmpty(CurrentUser.Password) || String.IsNullOrEmpty(CurrentUser.UserName))
                {
                    MessageBox.Show("Vui Lòng Nhập Đầy Đủ Thông Tin!");
                    return;
                }
            }
            

            if (CurrentUser.UserName.ToLower().Contains("admin") || CurrentUser.UserName.Contains(" ") || CurrentUser.UserName.Any(char.IsUpper))
            {
                MessageBox.Show("Tên Tài Khoản Không Hợp Lệ!");
                return;
            }

            if (CurrentUser.Password.Contains(" "))
            {
                MessageBox.Show("Mật Khẩu Không Hợp Lệ!");
                return;
            }

            if (CurrentUser.Password != ReEnterPassord)
            {
                MessageBox.Show("Mật khẩu không khớp!");
                return;
            }
            bool isChangRole = CurrentUser.Permission != Permision && IsEdit && CSGlobal.Instance.CurrentUser.UserName == CurrentUser.UserName;
            CurrentUser.Permission = Permision;
            AddCategoryOrEditCategoryCallback(CurrentUser, isChangRole);
            CloseDialogParent();
        }
    }
}
