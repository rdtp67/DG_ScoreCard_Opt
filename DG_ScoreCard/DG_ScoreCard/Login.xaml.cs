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
using MySql.Data.MySqlClient;
using DG_ScoreCard.DGserviceReference;


namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {


        //Variables
        private SignUp mainWindow;
        DGserviceReference.DGserviceClient client = new DGserviceReference.DGserviceClient();


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

        //Desc: Logs in user to dashboard if username is found, is active, and password is correct
        //Post: Redirects to dashboard, sends username
        private void submit1_btn_Click(object sender, RoutedEventArgs e)
        {

            string checkUsername = client.checkUsername(login1_tb.Text);
            if(checkUsername == "0")
            {
                login1_tb.BorderBrush = Brushes.Red;
                MessageBox.Show("Username does not exist! Please enter different username.");
            }

            List<login> loginList = new List<login>();
            loginList = client.returnCstringLists(login1_tb.Text);

            if (loginList != null)
            {
                for (int i = 0; i < loginList.Count; i++)
                {
                   
                    bool validuser = PasswordHash.ValidatePassword(password1_pb.Password, loginList[i].user_cstring);
                    if (validuser == true)
                    {
                        if (GenLib.isUserActive(loginList[i].user_active) == false)
                        {
                            MessageBox.Show("Username is Inactive. Contact ... to reactivate account.");
                            return;
                        }
                        //redirect to dashboard
                        this.Hide();
                        MainWindow mainWindow = new MainWindow(this, login1_tb.Text);
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        password1_pb.BorderBrush = Brushes.Red;
                        MessageBox.Show("Password entered is incorrect. Please try again.");
                    }

                }
            }
            else
            {
                password1_pb.BorderBrush = Brushes.Red;
                MessageBox.Show("Password entered is incorrect. Please try again.");
            }


        }
    }
}
