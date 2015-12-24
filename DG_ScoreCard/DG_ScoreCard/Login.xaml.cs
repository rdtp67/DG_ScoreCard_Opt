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
using MySql.Data.MySqlClient;

namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private SignUp mainWindow;
        //Variables
        const string myConnection = "datasource=localhost;port=3306;username=root;password=root; database=discgolf";
        MySqlConnection myConn = new MySqlConnection(myConnection);


        public Login()
        {
            InitializeComponent();
        }


        public Login(SignUp mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void exit1_btn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(); //Shutsdown Application
        }

        private void signup1_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SignUp SignUpWin = new SignUp(this);
            SignUpWin.Show();
            this.Close();
        }

        private void movebar1_r_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void submit1_btn_Click(object sender, RoutedEventArgs e)
        {
            List<string> salthashList = null;
            List<string> emailList = null;

            try
            {
                myConn.Open();
                string query = "Select user_name, user_email, user_slowhashsalt, user_active from user where user_name = @username";
                MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, myConn);
                cmd.Parameters.AddWithValue("@username", login1_tb.Text);
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.HasRows && reader.Read())
                {
                    if(salthashList == null)
                    {
                        salthashList = new List<string>();
                        emailList = new List<string>();
                    }

                    String saltHashes = reader.GetString(reader.GetOrdinal("user_slowhashsalt"));
                    salthashList.Add(saltHashes);

                    String email = reader.GetString(reader.GetOrdinal("user_email"));
                    emailList.Add(email);
                }
                reader.Close();

                if(salthashList != null)
                {
                    for(int i = 0; i < salthashList.Count; i++)
                    {
                        query = "";
                        bool validuser = PasswordHash.ValidatePassword(password1_pb.Password, salthashList[i]);
                        if(validuser == true)
                        {

                            //redirect to dashboard
                            MessageBox.Show("you did it");
                        }
                        else
                        {
                            password1_pb.BorderBrush = Brushes.Red;
                            MessageBox.Show("Password entered is incorrect. Please try again.");
                        }

                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myConn.Close();
            }
        }
    }
}
