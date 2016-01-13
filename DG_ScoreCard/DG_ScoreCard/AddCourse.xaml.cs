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
            Simple_g.Visibility = Visibility.Visible;
        }
        //Desc: Changes to Main Grid
        private void main_btn_Click(object sender, RoutedEventArgs e)
        {
            Simple_g.Visibility = Visibility.Hidden;
            Main_g.Visibility = Visibility.Visible;
        }
        //Submits Course
        private void submit_btn_Click(object sender, RoutedEventArgs e)
        {
            char? c_private = null;
            char? c_p2p = null;
            char? c_guide = null;
            if(private_yes_r.IsChecked == true)
            {
                c_private = 'T';
            }
            if(private_no_r.IsChecked == true)
            {
                c_private = 'F';
            }
            if(p2p_yes_r.IsChecked == true)
            {
                c_p2p = 'T';
            }
            if(p2p_no_r.IsChecked == true)
            {
                c_p2p = 'F';
            }
            if(course_guide_yes_r.IsChecked == true)
            {
                c_guide = 'T';
            }
            if(course_guide_no_r.IsChecked == true)
            {
                c_guide = 'F';
            }
            char? p_private = null;
            char? p_pet = null;
            char? p_guide = null;
            if (park_pet_y_r.IsChecked == true)
            {
                p_private = 'T';
            }
            if (park_pet_n_r.IsChecked == true)
            {
                p_private = 'F';
            }
            if (park_pet_y_r.IsChecked == true)
            {
                p_pet = 'T';
            }
            if (park_pet_n_r.IsChecked == true)
            {
                p_pet = 'F';
            }
            if (park_guide_y_r.IsChecked == true)
            {
                p_guide = 'T';
            }
            if (park_guide_n_r.IsChecked == true)
            {
                p_guide = 'F';
            }

            //Check for main fields entered

            //Verify field lengths
            MessageBox.Show(park_name_tb.Text);
            client.insertCourse(coursename_tb1.Text, website_tb1.Text, phonenumber_tb1.Text, basket_tb.Text, year_established_tb.Text, tee_type_cb.Text, course_type_cb.Text, terrain_cb.Text, basket_maker_tb.Text, c_private, c_p2p, c_guide, course_designer_tb.Text, username, address_tb1.Text, state_tb1.Text, city_tb1.Text, country_tb1.Text, zip_tb1.Text);
            client.insertPark(park_name_tb.Text, hightime_cb.Text, lowtime_cb.Text, p_guide, p_pet, p_private, username, coursename_tb1.Text);
        }
    }
}
