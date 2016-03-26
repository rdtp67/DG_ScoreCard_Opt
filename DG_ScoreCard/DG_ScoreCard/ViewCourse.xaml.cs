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
using System.ComponentModel;
using DG_ScoreCard.DGserviceReference;

namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for ViewCourse.xaml
    /// </summary>
    public partial class ViewCourse : Page
    {
        DGserviceClient client = new DGserviceClient();
        private int usr_id;
        List<courselist> c_list = new List<courselist>();
        
        public ViewCourse()
        {
            InitializeComponent();
        }

        public ViewCourse(int u_id)
        {
            InitializeComponent();
            usr_id = u_id;
                      
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            c_list = client.getMyCourseList(usr_id);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mycourse_cb.ItemsSource = c_list;
        }


        /********* On Clicks **********/

        private void coursedetails_btn_Click(object sender, RoutedEventArgs e)
        {
            if(course_g.Visibility == Visibility.Hidden)
            {
                course_g.Visibility = Visibility.Visible;
                hole_g.Visibility = Visibility.Hidden;
                next_btn.Visibility = Visibility.Hidden;
                prev_btn.Visibility = Visibility.Hidden;
                holecolor_cb.Visibility = Visibility.Hidden;
            }
        }

        private void holedetails_btn_Click(object sender, RoutedEventArgs e)
        {
            if(hole_g.Visibility == Visibility.Hidden)
            {
                hole_g.Visibility = Visibility.Visible;
                course_g.Visibility = Visibility.Hidden;
                next_btn.Visibility = Visibility.Visible;
                prev_btn.Visibility = Visibility.Visible;
                holecolor_cb.Visibility = Visibility.Visible;
            }
        }

        private void mycourse_cb_DropDownClosed(object sender, EventArgs e)
        {
            int c_id = getCourseID(mycourse_cb.Text);
            populateParkLocation(c_id, usr_id);
            populateCourseMain(c_id, usr_id);
        }


        /********* End On Clicks **********/

        /***** Populate window functions *****/

        private int getCourseID(string course_name)
        {
            for(int i=0; i<c_list.Count(); i++)
            {
                if(course_name == c_list[i].c_name)
                {
                    return c_list[i].c_id;
                }
            }

            return -666;
        }

        private void populateParkLocation(int course_id, int user_id)
        {
            location loc = client.getParkLoc(course_id);
            park p = client.getPark(course_id, user_id);

            parkname_tbl.Text = p.p_name;

            parkdeatils_tbl.Text = "Address: " + loc.l_address + "\n" +
                     "City: " + loc.l_city + "\n" +
                     "State: " + loc.l_state + "\n" +
                     "Zip: " + loc.l_zip + "\n" + 
                     "Country: " + loc.l_country + "\n\n" + 
                     "Park Hours: " + p.p_hours_high + "-" + p.p_hours_low + "\n" +
                     "Park Private: " + p.p_private + "\n" +
                     "Pet Friendly: " + p.p_pet_friendly + "\n" + 
                     "Park has Guides: " + p.p_has_guides;
        }

        private void populateCourseMain(int course_id, int user_id)
        {
            course c = client.getCourse(course_id, user_id);
            coursename_tbl.Text = c.c_name;
            phone_tbl.Text = "Phone: " + c.c_phone;
            email_tbl.Text = "Email: " + c.c_email;
            website_tbl.Text = "Website: " + c.c_website;

            coursemain4_tbl.Text = "?\n" +
                                    c.c_type + "\n" + 
                                    c.c_tee_type + "\n" + 
                                    c.c_basket_type + "\n" +
                                    c.c_basket_manu + "\n" +
                                    c.c_year_est + "\n" +
                                    c.c_design + "\n" +
                                    c.c_terrain + "\n" +
                                    c.c_pri + "\n" +
                                    c.c_pay + "\n" +
                                    c.c_has_guide;
        }

        /***** End Populate window functions *****/
    }
}
