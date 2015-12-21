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
            
            //Check for blanks
            if(submit2_btn_CheckBlanks() == true)
            {
                MessageBox.Show("Please fill in required fields for Sign Up.");
                return;
            }
           
            //Check for usernae taken already
            if (isUsernameTaken(username2_tb.Text) == true)
            {
              MessageBox.Show("Username is already in use.");
                return;
            }
               
            //Password check
            if(isPasswordMatch(password2_pb.Password, confirm2_pb.Password) == false)
            {
                 MessageBox.Show("Passwords do not match.");
                return;
            }
            //Check length of fields done in DB

            else
            {
                //Loads new location
                try
                {
                    myConn.Open();
                    MySqlCommand com = new MySqlCommand();
                    com.Connection = myConn;
                    com.CommandText = "INSERT INTO location(loc_address, loc_state, loc_city, loc_country, loc_zip) VALUES(@loc_address, @loc_state, @loc_city, @loc_country, @loc_zip)";
                    com.Prepare();
                    com.Parameters.AddWithValue("@loc_address", address2_tb.Text);
                    com.Parameters.AddWithValue("@loc_state", state2_tb.Text);
                    com.Parameters.AddWithValue("@loc_city", city2_tb.Text);
                    com.Parameters.AddWithValue("@loc_country", country2_tb.Text);
                    com.Parameters.AddWithValue("@loc_zip", zip2_tb.Text);
                    com.ExecuteNonQuery();
                           
                }
                catch(Exception ext)
                {
                    MessageBox.Show(ext.Message);
                }
                finally
                {
                    myConn.Close();
                }

                //Loads new user
                try
                {
                    myConn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = myConn;
                    cmd.CommandText = "INSERT INTO user(user_name, loc_id, user_password, user_fname, user_lname, user_email, user_phone) VALUES(@user_name, (Select l.loc_id " +
                                                                                                                                                                "from location l " +
                                                                                                                                                               "where l.loc_address = '" + address2_tb.Text + "' and l.loc_state = '" + state2_tb.Text + "' and l.loc_city = '" + city2_tb.Text + "' and l.loc_country = '" + country2_tb.Text + "' and l.loc_zip = " + zip2_tb.Text + " " +
                                                                                                                                                                " ), @password, @fname, @lname, @email, @phone)";
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@user_name", username2_tb.Text);
                    cmd.Parameters.AddWithValue("@password", password2_pb.Password);
                    cmd.Parameters.AddWithValue("@fname", fname2_tb.Text);
                    cmd.Parameters.AddWithValue("@lname", lname2_tb.Text);
                    cmd.Parameters.AddWithValue("@email", email2_tb.Text);
                    cmd.Parameters.AddWithValue("@phone", phone2_tb.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User has been created!");
                 }
                 catch(Exception ex)
                 {
                    MessageBox.Show("User creation has failed. " + ex.Message);
                 }
                 finally
                 {
                    myConn.Close();
                 }
             }
                
            
      
        }

        //Desc: Sets textbox borderbrush to #FFABADB3
        private void setBorderBrushTBDefault(TextBox o)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFABADB3");
            o.BorderBrush = brush;

            return;
        }

        //Desc: Sets textbox borderbrush to #FFABADB3
        private void setBorderBrushPBDefault(PasswordBox o)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFABADB3");
            o.BorderBrush = brush;

            return;
        }

        //Desc: Checks for blanks in username2_tb, password2_pb, and email2_tb
        //Post: returns true for blank
        private bool submit2_btn_CheckBlanks()
        {
            bool blank = false;

            if (GenLib.isBlank(username2_tb.Text) == true)
            {
                username2_tb.BorderBrush = Brushes.Red;
                blank = true;
            }
            else
            {
                setBorderBrushTBDefault(username2_tb);
            }
            if (GenLib.isBlank(password2_pb.Password) == true)
            {
                password2_pb.BorderBrush = Brushes.Red;
                blank = true;
            }
            else
            {
                setBorderBrushPBDefault(password2_pb);
            }
            if (GenLib.isBlank(confirm2_pb.Password) == true)
            {
                confirm2_pb.BorderBrush = Brushes.Red;
                blank = true;
            }
            else
            {
                setBorderBrushPBDefault(confirm2_pb);
            }
            if (GenLib.isBlank(email2_tb.Text) == true)
            {
                email2_tb.BorderBrush = Brushes.Red;
            }
            else
            {
                setBorderBrushTBDefault(email2_tb);
            }

            return blank;
        }

        //Desc: Checks if username is already taken
        //Post: returns true if is taken, turn username2_tb border brush red if true
        private bool isUsernameTaken(string t)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFABADB3");
            bool isTaken = false;

            try
            { 
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

        //Desc: Checks if password is the same
        //Post: returns true if password is the same
        private bool isPasswordMatch(string s1, string s2)
        {
            bool match = false;
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFABADB3");

            if (s1 == s2)
            {
                match = true;
                password2_pb.BorderBrush = brush;
                confirm2_pb.BorderBrush = brush;
            }
            else
            {
                password2_pb.BorderBrush = Brushes.Red;
                confirm2_pb.BorderBrush = Brushes.Red;
            }
            return match;
        }

        //Desc: Exits app from signup page
        private void exit2_btn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(); //Shutsdown Application
        }

        //Desc: Check location
        //Post: Return true if present already
        private bool isLocationTaken()
        {
            bool taken = true;
            try
            {
                myConn.Open();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }
            
            return taken;

        }
    }
}
