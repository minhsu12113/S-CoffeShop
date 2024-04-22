using CoffeShop.Enums;
using CoffeShop.Internationalization;
using CoffeShop.Utility;
using CoffeShop.View;
using CoffeShop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CoffeShop.Viewmodel
{
    public class InitViewmodel : BindableBase
    {
        public InitViewmodel()
        {
            StringResources.ApplyLanguage(ALL_ENUM.LANGUAGE.VN);
            Loaded();
        }
        public async void Loaded()
        {
             
        }
        public async void CreateAccountAdmin()
        {
            await Task.Run(() =>
            {
                //using (var unitOfWork = new UnitOfWork(new CoffeeShopContext()))
                //{
                //    unitOfWork.User.CheckAndCreateAccountAdmin();
                //}
            });
        }        
    }
}
