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

namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        //Variables
        private Login mainWindow;
        const string myConnection = "datasource=localhost;port=3306;username=root;password=root; database=discgolf";
        MySqlConnection myConn = new MySqlConnection(myConnection);

        //Desc: Creates Window
        public SignUp(Login mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        //Desc: Allows to drag window when pressed down
        private void movebar_r_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //Desc: Redirects back to login
        private void backarrow2_BA_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Login LoginWin = new Login(this);
            LoginWin.Show();
            this.Close();
        }

        //Desc: Signup process
        private void submit2_btn_Click(object sender, RoutedEventArgs e)
        {
            bool blank = submit2_btn_CheckBlanks();

            if(blank == true)
            {
                MessageBox.Show("Please fill in required fields for Sign Up.");
            }
            else
            {
                if (isUsernameTaken(username2_tb.Text) == true)
                {
                    MessageBox.Show("Username is already in use.");
                }
                else
                {
                    MessageBox.Show("yall good");
                }
            }
      
        }

        //Desc: Checks for blanks in username2_tb, password2_pb, and email2_tb
        //Post: returns true for blank
        private bool submit2_btn_CheckBlanks()
        {
            bool blank = false;
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFABADB3");

            if (GenLib.isBlank(username2_tb.Text) == true)
            {
                username2_tb.BorderBrush = Brushes.Red;
                blank = true;
            }
            else
            {
                username2_tb.BorderBrush = brush;
            }
            if (GenLib.isBlank(password2_pb.Password) == true)
            {
                password2_pb.BorderBrush = Brushes.Red;
                blank = true;
            }
            else
            {
                password2_pb.BorderBrush = brush;
            }
            if (GenLib.isBlank(email2_tb.Text) == true)
            {
                email2_tb.BorderBrush = Brushes.Red;
            }
            else
            {
                email2_tb.BorderBrush = brush;
            }

            return blank;
        }

        //Desc: Checks if username is already taken
        //Post: returns true if is taken, turn username2_tb border brush red if true
        private bool isUsernameTaken(string t)
        {
            bool blank = false;
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFABADB3");
            bool isTaken;
            myConn.Open();
            String username = "1";
            MySqlCommand cmd = new MySqlCommand("Select count(u.user_name) as 'Count' " +
                                                    "from user u " +
                                                    "where u.user_name = '" + t + "' ", myConn); //Does not check for active incase account gets re-activated
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                username = rdr["Count"].ToString();
            }
            if(username == "0")
            {
                isTaken = false;
                username2_tb.BorderBrush = brush;
                
            }
            else
            {
                isTaken = true;
                username2_tb.BorderBrush = Brushes.Red;
            }

            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myConn.Close();
            }

            return isTaken;
        }


    }
}
