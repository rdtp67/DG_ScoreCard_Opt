﻿using System;
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

namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(); //Shutsdown Application
        }

        private void signupButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SignUp SignUpWin = new SignUp(this);
            SignUpWin.Show();
            this.Close();
        }
    }
}
