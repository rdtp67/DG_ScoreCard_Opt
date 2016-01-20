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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for AddCourse.xaml
    /// </summary>
    public partial class AddCourse : Page
    {
        DGserviceReference.DGserviceClient client = new DGserviceReference.DGserviceClient();
        private string username = "NULL";
        public AddCourse()
        {
            InitializeComponent();
        }
        public AddCourse(string user)
        {
            InitializeComponent();
            username = user;
        }
        //Desc: Changes to Holes Grid
        private void addholes_btn_Click(object sender, RoutedEventArgs e)
        {
            Main_g.Visibility = Visibility.Hidden;
            Custom_g.Visibility = Visibility.Visible;
        }
        //Desc: Changes to Main Grid
        private void main_btn_Click(object sender, RoutedEventArgs e)
        {
            Custom_g.Visibility = Visibility.Hidden;
            Main_g.Visibility = Visibility.Visible;
        }
        //Submits Course
        private void submit_btn_Click(object sender, RoutedEventArgs e)
        {
            char? c_private = null;
            char? c_p2p = null;
            char? c_guide = null;
            char? p_private = null;
            char? p_pet = null;
            char? p_guide = null;

            c_private = getRadioButton(private_yes_r, private_no_r);
            c_p2p = getRadioButton(p2p_yes_r, p2p_no_r);
            c_guide = getRadioButton(course_guide_yes_r, course_guide_no_r);
            p_private = getRadioButton(park_private_y_r, park_private_n_r);
            p_pet = getRadioButton(park_pet_y_r, park_pet_n_r);
            p_guide = getRadioButton(park_guide_y_r, park_guide_n_r);

            //Check for main fields entered

            //Verify field lengths
            client.insertPark(parkname_tb.Text, hightime_cb.Text, lowtime_cb.Text, p_guide, p_pet, p_private);
            client.insertLocation(address_tb1.Text, state_tb1.Text, city_tb1.Text, country_tb1.Text, zip_tb1.Text);
            client.insertCourse(coursename_tb1.Text, website_tb1.Text, phonenumber_tb1.Text, basket_tb.Text, year_established_tb.Text, tee_type_cb.Text, course_type_cb.Text, terrain_cb.Text, basket_maker_tb.Text, c_private, c_p2p, c_guide, course_designer_tb.Text, username, address_tb1.Text, state_tb1.Text, city_tb1.Text, country_tb1.Text, zip_tb1.Text, client.getParkId(parkname_tb.Text, p_private, hightime_cb.Text, lowtime_cb.Text, p_guide, p_pet));
            
        }

        //Desc: Gets Radio button current state
        //Pre: radio buttons
        //Post: ischecked returns T or F, nothing returns NULL
        private char? getRadioButton(RadioButton yes, RadioButton no)
        {
            if(yes.IsChecked == true)
            {
                return 'T';
            }
            if(no.IsChecked == true)
            {
                return 'F';
            }

            return 'N';
        }
        Point m_start;
        Vector m_startOffset;
        private void move_g_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(course_designer_g);
            m_startOffset = new Vector(tt.X, tt.Y);
            move_g.CaptureMouse();

        }

        private void move_g_MouseMove(object sender, MouseEventArgs e)
        {
            if (move_g.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(course_designer_g), m_start);
                tt.X = m_startOffset.X + offset.X;
                tt.Y = m_startOffset.Y + offset.Y;
                textBlock23.Text = "X: " + tt.X.ToString() + " Y: " + tt.Y.ToString() + " Mstart: " + m_start.X.ToString() + ", " + m_start.Y.ToString();
            }
        }

        private void move_g_MouseUp(object sender, MouseButtonEventArgs e)
        {
            move_g.ReleaseMouseCapture();
        }

        private void move_maindetails_g_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(course_designer_g);
            m_startOffset = new Vector(tt1.X, tt1.Y);
           // m_startOffset = new Vector(tt1.X, tt1.Y);
            // maindetails_g.CaptureMouse();
            move_maindetails_g.CaptureMouse();
            maindetails_g.BringIntoView();
        }

        private void move_maindetails_g_MouseMove(object sender, MouseEventArgs e)
        {
            if (move_maindetails_g.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(course_designer_g), m_start);
               // tt1.X = m_startOffset.X + offset.X;
               // tt1.Y = m_startOffset.Y + offset.Y;
                tt1.X = m_startOffset.X + offset.X;
                tt1.Y = m_startOffset.Y + offset.Y;
                textBlock23.Text = "X: " + tt2.X.ToString() + " Y: " + tt2.Y.ToString() + " Mstart: " + m_start.X.ToString() + ", " + m_start.Y.ToString();
            }
        }

        private void move_maindetails_g_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // maindetails_g.ReleaseMouseCapture();
           move_maindetails_g.ReleaseMouseCapture();
        }
    }
}
