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
using MySql.Data.MySqlClient;
using System.Windows.Shapes;

namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        //Variables
        private Login mainWindow;
        DGserviceReference.DGserviceClient client = new DGserviceReference.DGserviceClient();

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

            string address = address2_tb.Text;
            string state = state2_tb.Text;
            string city = city2_tb.Text;
            string country = country2_tb.Text;
            string zip = zip2_tb.Text;
            string username = username2_tb.Text;
            string fname = fname2_tb.Text;
            string lname = lname2_tb.Text;
            string email = email2_tb.Text;
            string phone = phone2_tb.Text;

            //Check for blanks
            if (submit2_btn_CheckBlanks() == true)
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
            if (isLengthCorrect() == false)
            {
                return;
            }
            bool loccheck = client.checkLocation(address, state, city, country, zip);
            if (loccheck== false)
            {

               client.insertLocation(address, state, city, country, zip);


            }

            //salt : hash
            String saltHashReturned = PasswordHash.CreateHash(password2_pb.Password);
                int commaIndex = saltHashReturned.IndexOf(":");
                String extractedString = saltHashReturned.Substring(0, commaIndex);
                commaIndex = saltHashReturned.IndexOf(":");
                extractedString = saltHashReturned.Substring(commaIndex + 1);
                commaIndex = extractedString.IndexOf(":");
                String salt = extractedString.Substring(0, commaIndex);

            commaIndex = extractedString.IndexOf(":");
                extractedString = extractedString.Substring(commaIndex + 1);
                String hash = extractedString;

            client.insertUser(username, fname, lname, email, phone, saltHashReturned, address, state, city, country, zip);
                MessageBox.Show("User has been created!");





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
                blank = true;
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

                string username = client.checkUsername(username2_tb.Text);
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


        //Check Length of Signup TextBoxes & PasswordBoxes
        private bool isLengthCorrect()
        {
            bool lengthCheck = true;
            string toLongMessage;
            toLongMessage = "The Following fields have too many characters: ";

            if (username2_tb.Text.Length > 50)
            {
                lengthCheck = false;
                username2_tb.BorderBrush = Brushes.Red;
                toLongMessage += "\n Username is " + username2_tb.Text.Length + " characters. (Max 50)";
            }
            else
            {
                setBorderBrushTBDefault(username2_tb);
            }
            if(password2_pb.Password.Length > 50)
            {
                lengthCheck = false;
                password2_pb.BorderBrush = Brushes.Red;
                toLongMessage += "\n Password is " + password2_pb.Password.Length + " characters. (Max 50)";
            }
            else
            {
                setBorderBrushPBDefault(password2_pb);
            }
            if(fname2_tb.Text.Length > 100)
            {
                lengthCheck = false;
                fname2_tb.BorderBrush = Brushes.Red;
                toLongMessage += "\n First Name is " + fname2_tb.Text.Length + " characters. (Max 100)";
            }
            else
            {
                setBorderBrushTBDefault(fname2_tb);
            }
            if(lname2_tb.Text.Length > 100)
            {
                lengthCheck = false;
                lname2_tb.BorderBrush = Brushes.Red;
                toLongMessage += "\n Last Name is " + lname2_tb.Text.Length + " characters. (Max 100)";
            }
            else
            {
                setBorderBrushTBDefault(lname2_tb);
            }
            if(email2_tb.Text.Length > 100)
            {
                lengthCheck = false;
                email2_tb.BorderBrush = Brushes.Red;
                toLongMessage += "\n Email is " + email2_tb.Text.Length + " characters. (Max 100)";
            }
            else
            {
                setBorderBrushTBDefault(email2_tb);
            }
            if (phone2_tb.Text.Length > 100)
            {
                lengthCheck = false;
                phone2_tb.BorderBrush = Brushes.Red;
                toLongMessage += "\n Phone Number is " + phone2_tb.Text.Length + " characters. (Max 15)";
            }
            else
            {
                setBorderBrushTBDefault(phone2_tb);
            }
            if (address2_tb.Text.Length > 100)
            {
                lengthCheck = false;
                address2_tb.BorderBrush = Brushes.Red;
                toLongMessage += "\n Address is " + address2_tb.Text.Length + " characters. (Max 100)";
            }
            else
            {
                setBorderBrushTBDefault(address2_tb);
            }
            if (state2_tb.Text.Length > 100)
            {
                lengthCheck = false;
                state2_tb.BorderBrush = Brushes.Red;
                toLongMessage += "\n State is " + state2_tb.Text.Length + " characters. (Max 100)";
            }
            else
            {
                setBorderBrushTBDefault(state2_tb);
            }
            if (city2_tb.Text.Length > 100)
            {
                lengthCheck = false;
                city2_tb.BorderBrush = Brushes.Red;
                toLongMessage += "\n City is " + city2_tb.Text.Length + " characters. (Max 100)";
            }
            else
            {
                setBorderBrushTBDefault(city2_tb);
            }
            if (country2_tb.Text.Length > 100)
            {
                lengthCheck = false;
                country2_tb.BorderBrush = Brushes.Red;
                toLongMessage += "\n Country is " + country2_tb.Text.Length + " characters. (Max 50)";
            }
            else
            {
                setBorderBrushTBDefault(country2_tb);
            }
            if (zip2_tb.Text.Length > 100)
            {
                lengthCheck = false;
                zip2_tb.BorderBrush = Brushes.Red;
                toLongMessage += "\n Zip is " + zip2_tb.Text.Length + " characters. (Max 15)";
            }
            else
            {
                setBorderBrushTBDefault(zip2_tb);
            }

            if(lengthCheck == false)
            {
                MessageBox.Show(toLongMessage);
            }


            return lengthCheck;
        }
            
        ////Return byte array as hex string
        //private static string ByteArrayToHexString(byte[] ba)
        //{
        //    StringBuilder hex = new StringBuilder(ba.Length * 2);
        //    foreach(byte b in ba)
        //    {
        //        hex.AppendFormat("{0:x2}", b);
        //    }

        //    return hex.ToString();
        //}

        ////Hash
        //private String GenerateSHA256Hash(string input, String salt)
        //{
        //    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
        //    System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
        //    byte[] hash = sha256hashstring.ComputeHash(bytes);
        //    return ByteArrayToHexString(hash);
        //}

        ////Salt
        //private string CreateSalt(int size)
        //{
        //    var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        //    var buff = new byte[size];
        //    rng.GetBytes(buff);
        //    return Convert.ToBase64String(buff);
        //}
    }
}
