﻿using CoffeShop.Enums;
using CoffeShop.Internationalization;
using CoffeShop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoffeShop.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            StringResources.ApplyLanguage(ALL_ENUM.LANGUAGE.VN);
            CSGlobal.Instance.LoginWindow = this;
        }
    }
}
