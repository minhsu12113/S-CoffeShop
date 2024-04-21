using CoffeShop.Internationalization;
using CoffeShop.Model;
using CoffeShop.View.Categorys;
using CoffeShop.View.Dialog;
using CoffeShop.View.User;
using CoffeShop.Viewmodel.Base;
using CoffeShop.Viewmodel.Categorys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoffeShop.Viewmodel.User
{
    public class UserViewModel : BindableBase, ICustomDialog
    {

        #region [Command]
        public ICommand AddNewCMD { get { return new CommandHelper(OpenDialogAddNew); } }
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

        private void LoadUser()
        {
            UserModelList = new ObservableCollection<UserModel>(UserModelMasterList);
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
                UserModelMasterList.Remove(addOrUpdateObj);
                UserModelMasterList.Add(addOrUpdateObj);
            }
            else
                UserModelMasterList.Add(model);

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
                    UserModelMasterList.Remove(model);
                    LoadUser();
                    CloseDialog();

                }, CloseDialog));
        }
    }
}
