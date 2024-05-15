﻿using CoffeShop.DAO;
using CoffeShop.Internationalization;
using CoffeShop.Model;
using CoffeShop.Utility;
using CoffeShop.View.Categorys;
using CoffeShop.View.Dialog;
using CoffeShop.View.User;
using CoffeShop.Viewmodel.Base;
using CoffeShop.Viewmodel.Categorys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.User
{
    public class UserViewModel : BindableBase, ICustomDialog
    {

        #region [Command]
        public ICommand AddNewCMD { get { return new CommandHelper(OpenDialogAddNew); } }
        public ICommand OpenUCSendPayrollCMD { get { return new CommandHelper(OpenUCSendPayroll); } }
        public ICommand EditCMD { get { return new CommandHelper<UserModel>((c) => { return c != null; }, OpenDialogEdit); } }

        public ICommand DeleteCMD { get { return new CommandHelper<UserModel>((c) => { return c != null; }, DeleteUser); } }

        public ICommand SearchCMD { get { return new CommandHelper(LoadUser); } }
        #endregion

        #region [Props]
        private object _dialogContent;
        private bool _isOpendialog;

        public object DialogContent
        {
            get { return _dialogContent; }
            set { _dialogContent = value; OnPropertyChanged(); }
        }
        public bool IsOpenDialog
        {
            get { return _isOpendialog; }
            set { _isOpendialog = value; OnPropertyChanged(); }
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

        private string _nameSearch;
        public string NameSearch
        {
            get { return _nameSearch; }
            set { _nameSearch = value; OnPropertyChanged(); SearchCategory(); }
        }

        #endregion

        #region [Collection]
        public List<UserModel> UserModelMasterList { get; set; } = new List<UserModel>();
        private ObservableCollection<UserModel> _userModelList = new ObservableCollection<UserModel>();

        public ObservableCollection<UserModel> UserModelList
        {
            get { return _userModelList; }
            set { _userModelList = value; OnPropertyChanged(); }
        }
        #endregion

        public UserViewModel()
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
            {  }
            
        }

        public void SearchCategory()
        {
            if (String.IsNullOrEmpty(NameSearch))
            {
                UserModelList = new ObservableCollection<UserModel>(UserModelMasterList);
                return;
            }

            var searchData = UserModelMasterList.Where(c => c.UserName.ToLower().Contains(NameSearch.ToLower())).ToList();
            UserModelList = new ObservableCollection<UserModel>(searchData);
        }

        private void OpenDialogAddNew()
        {
            OpenDialog(new AddOrUpdateUserUC(new AddOrUpdateUserViewModel(AddUserOrEdit, CloseDialog)));

        }

        private void AddUserOrEdit(UserModel model)
        {
            
            var addOrUpdateObj = UserModelMasterList.Where(u => u.Id == model.Id)?.FirstOrDefault();
            if (addOrUpdateObj != null)
            { 
                AccountDAO.Instance.Update(model.Data);
            }
            else
            {
                AccountDAO.Instance.Insert(model.Data);
            }    

            LoadUser();
        }

        private void OpenDialogEdit(UserModel model)
        {
            OpenDialog(new AddOrUpdateUserUC(new AddOrUpdateUserViewModel(AddUserOrEdit, CloseDialog, model))); 
        }

        private void DeleteUser(UserModel model)
        {
            string question = String.Format(StringResources.Find("CATEGORY_CONFIRM_DELETE"), model.UserName);
            OpenDialog(new ConfirmUC(question,
                () =>
                {
                    AccountDAO.Instance.Delete(model.Data);
                    LoadUser();
                    CloseDialog();

                }, CloseDialog));
        }

        public void OpenUCSendPayroll()
        {
            OpenDialog(new SendPayrollUC(CloseDialog));
        }
    }
}
