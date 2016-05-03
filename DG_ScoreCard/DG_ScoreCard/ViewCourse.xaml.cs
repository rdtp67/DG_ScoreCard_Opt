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
        private int c_id;
        List<courselist> c_list = new List<courselist>();
        List<course_view_holes> cvh = new List<course_view_holes>();
        List<combobox_item_string> colors_list = new List<combobox_item_string>();
        private string cur_color = "None";
        private int cur_num = 1;
        private int cur_high;
        
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
                next18_btn.Visibility = Visibility.Hidden;
                prev18_btn.Visibility = Visibility.Hidden;
                next9_btn.Visibility = Visibility.Hidden;
                prev9_btn.Visibility = Visibility.Hidden;
                holecolor_cb.Visibility = Visibility.Hidden;
            }
        }

        private void holedetails_btn_Click(object sender, RoutedEventArgs e)
        {
            if(hole_g.Visibility == Visibility.Hidden)
            {
                hole_g.Visibility = Visibility.Visible;
                course_g.Visibility = Visibility.Hidden;
                next18_btn.Visibility = Visibility.Visible;
                prev18_btn.Visibility = Visibility.Visible;
                next9_btn.Visibility = Visibility.Visible;
                prev9_btn.Visibility = Visibility.Visible;
                holecolor_cb.Visibility = Visibility.Visible;
            }
        }

        private void mycourse_cb_DropDownClosed(object sender, EventArgs e)
        {
            c_id = getCourseID(mycourse_cb.Text);
            populateParkLocation(c_id, usr_id);
            populateCourseMain(c_id, usr_id);
            populateCourseViewHoleColorParCount(c_id, usr_id);
            populateCourseViewHolesList(c_id, usr_id);
            cur_color = checkHoleListforColor();
            cur_high = checkHoleNumHigh();
            setRadioButtonColor(cur_color);
            populateHoleNumbers(cur_num);
            populateCourseViewHoles(c_id, usr_id);
            hideNonUsedHoles();
            unhideUsedHoles();
            populateHoleColorDropDown(c_id, usr_id);
            
        }

        private void holecolor_cb_DropDownClosed(object sender, EventArgs e)
        {
            setRadioButtonColor(holecolor_cb.Text);
            cur_color = holecolor_cb.Text;
            cur_high = checkHoleNumHigh();
            populateHoleNumbers(cur_num);
            populateCourseViewHoles(c_id, usr_id);
            hideNonUsedHoles();
            unhideUsedHoles();
        }

        private void radio_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            int rb0 = int.Parse(rb.Content.ToString()[0].ToString());
            int cur = cur_num + rb0;
            int color_num = 1;
            while(color_num<3)
            {
                if (rb.Content.ToString()[color_num] == 'r')
                {
                    populateHoleOnRadioClick("Red", cur.ToString(), rb0);
                    return;
                }
                else if (rb.Content.ToString()[color_num] == 'g')
                {
                    populateHoleOnRadioClick("Gold", cur.ToString(), rb0);
                    return;
                }
                else if (rb.Content.ToString()[color_num] == 'w')
                {
                    populateHoleOnRadioClick("White", cur.ToString(), rb0);
                    return;
                }
                else if (rb.Content.ToString()[color_num] == 'b')
                {
                    populateHoleOnRadioClick("Blue", cur.ToString(), rb0);
                    return;
                }
                else if (rb.Content.ToString()[color_num] == 'l')
                {
                    populateHoleOnRadioClick("Black", cur.ToString(), rb0);
                    return;
                }
                else
                {
                    rb0 = int.Parse(rb.Content.ToString()[0].ToString() + rb.Content.ToString()[1].ToString());
                    color_num++;
                }
            }
            
        }

        /********* End On Clicks **********/

        /********* Button Clicks **********/
        private void prev18_btn_Click(object sender, RoutedEventArgs e)
        {
            if((cur_num - 18)<0)
            {
                return;
            }

            cur_num -= 18;
            populateHoleNumbers(cur_num);
            populateCourseViewHoles(c_id, usr_id);
            setRadioButtonColor(holecolor_cb.Text);
            hideNonUsedHoles();
            unhideUsedHoles();
        }

        private void prev9_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((cur_num - 9) < 0)
            {
                return;
            }

            cur_num -= 9;
            populateHoleNumbers(cur_num);
            populateCourseViewHoles(c_id, usr_id);
            setRadioButtonColor(holecolor_cb.Text);
            hideNonUsedHoles();
            unhideUsedHoles();
        }

        private void next9_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((cur_num + 9) > cur_high)
            {
                return;
            }

            cur_num += 9;
            populateHoleNumbers(cur_num);
            populateCourseViewHoles(c_id, usr_id);
            setRadioButtonColor(holecolor_cb.Text);
            hideNonUsedHoles();
            unhideUsedHoles();
        }

        private void next18_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((cur_num + 18) > cur_high)
            {
                return;
            }

            cur_num += 18;
            populateHoleNumbers(cur_num);
            populateCourseViewHoles(c_id, usr_id);
            setRadioButtonColor(holecolor_cb.Text);
            hideNonUsedHoles();
            unhideUsedHoles();
        }
        /****** End Button Clicks *********/

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

        private void populateCourseViewHoleColorParCount(int course_id, int user_id)
        {
            coursemain1_tbl.Text = "";

            List<course_view_course> c = client.getCourseViewCourse(course_id, user_id);
            for(int i = 0; i<c.Count(); i++)
            {
                coursemain1_tbl.Text = coursemain1_tbl.Text + "Hole Color: " + c[i].h_color + "\n" +
                                                              "Number of Holes: " + c[i].h_count + "\n" +
                                                              "Total Yardage: " + c[i].h_yardage + "\n" +
                                                              "Par: " + c[i].h_par + "\n\n";
            }

        }

        private void populateCourseViewHolesList(int course_id, int user_id)
        {
                cvh = new List<course_view_holes>();
                cvh = client.getCourseViewHoles(course_id, user_id);
        }

        private void populateCourseViewHoles(int course_id, int user_id)
        {
            TextBlock[] l = { holel1_tbl, holel2_tbl, holel3_tbl, holel4_tbl, holel5_tbl, holel6_tbl, holel7_tbl, holel8_tbl, holel9_tbl, holel10_tbl, holel11_tbl, holel12_tbl, holel13_tbl, holel14_tbl, holel15_tbl, holel16_tbl, holel17_tbl, holel18_tbl };
            TextBlock[] p = { holep1_tbl, holep2_tbl, holep3_tbl, holep4_tbl, holep5_tbl, holep6_tbl, holep7_tbl, holep8_tbl, holep9_tbl, holep10_tbl, holep11_tbl, holep12_tbl, holep13_tbl, holep14_tbl, holep15_tbl, holep16_tbl, holep17_tbl, holep18_tbl };
            TextBlock[] y = { holey1_tbl, holey2_tbl, holey3_tbl, holey4_tbl, holey5_tbl, holey6_tbl, holey7_tbl, holey8_tbl, holey9_tbl, holey10_tbl, holey11_tbl, holey12_tbl, holey13_tbl, holey14_tbl, holey15_tbl, holey16_tbl, holey17_tbl, holey18_tbl };
            TextBlock[] tbl = { num1_tbl, num2_tbl, num3_tbl, num4_tbl, num5_tbl, num6_tbl, num7_tbl, num8_tbl, num9_tbl, num10_tbl, num11_tbl, num12_tbl, num13_tbl, num14_tbl, num15_tbl, num16_tbl, num17_tbl, num18_tbl };
            clearHoleLetterParYardage();
            disableRadioButtons();
            enableRadioButtons();
            int stop = cur_num + 17;
            for (int i = 0; i < cvh.Count(); i++)
            {
                
                if (cvh[i].t_color == cur_color && int.Parse(cvh[i].h_num) <= stop && int.Parse(cvh[i].h_num) >= cur_num)
                {
                    l[int.Parse(cvh[i].h_num) - cur_num].Text += cvh[i].b_letter + "\n";
                    p[int.Parse(cvh[i].h_num) - cur_num].Text += cvh[i].h_par + "\n";
                    y[int.Parse(cvh[i].h_num) - cur_num].Text += cvh[i].h_yard + "\n";
                    
                }
                
            }


        }

        private void populateHoleNumbers(int start)
        {
            TextBlock[] tbl = { num1_tbl, num2_tbl, num3_tbl, num4_tbl, num5_tbl, num6_tbl, num7_tbl, num8_tbl, num9_tbl, num10_tbl, num11_tbl, num12_tbl, num13_tbl, num14_tbl, num15_tbl, num16_tbl, num17_tbl, num18_tbl };
            int count = 0;

            if((cvh.Count-start)<0)
            {
                return;
            }

            if((cvh.Count()- start)>17)
            {
                count = 17;
            }
            else
            {
                count = (cvh.Count() - start);
            }

            for (int i = 0; i <=count; i++)
            {
                tbl[i].Text = "Hole " + (i + start).ToString();
            }
        }

        private void populateHoleColorDropDown(int course_id, int user_id)
        {
            colors_list = client.getCourseDistinctHoleColors(course_id, user_id);
            holecolor_cb.ItemsSource = colors_list;
  
        }

        private void populateHoleOnRadioClick(string color, string hole_num, int cur)
        {
            TextBlock[] l = { holel1_tbl, holel2_tbl, holel3_tbl, holel4_tbl, holel5_tbl, holel6_tbl, holel7_tbl, holel8_tbl, holel9_tbl, holel10_tbl, holel11_tbl, holel12_tbl, holel13_tbl, holel14_tbl, holel15_tbl, holel16_tbl, holel17_tbl, holel18_tbl };
            TextBlock[] p = { holep1_tbl, holep2_tbl, holep3_tbl, holep4_tbl, holep5_tbl, holep6_tbl, holep7_tbl, holep8_tbl, holep9_tbl, holep10_tbl, holep11_tbl, holep12_tbl, holep13_tbl, holep14_tbl, holep15_tbl, holep16_tbl, holep17_tbl, holep18_tbl };
            TextBlock[] y = { holey1_tbl, holey2_tbl, holey3_tbl, holey4_tbl, holey5_tbl, holey6_tbl, holey7_tbl, holey8_tbl, holey9_tbl, holey10_tbl, holey11_tbl, holey12_tbl, holey13_tbl, holey14_tbl, holey15_tbl, holey16_tbl, holey17_tbl, holey18_tbl };
            l[cur].Text ="";
            p[cur].Text ="";
            y[cur].Text ="";
            for (int i = 0; i < cvh.Count(); i++)
            {

                if (cvh[i].t_color == color && cvh[i].h_num == hole_num)
                {
                    l[cur].Text += cvh[i].b_letter + "\n";
                    p[cur].Text += cvh[i].h_par + "\n";
                    y[cur].Text += cvh[i].h_yard + "\n";

                }

            }

        }

        /***** End Populate window functions *****/

        /***** Help functions                ****/

        private string checkHoleListforColor()
        {
            int color = 9999;
            for (int i = 0; i < cvh.Count(); i++)
            {
                if (cvh[i].t_color == "Red")
                {
                    return "Red";
                }

                if (cvh[i].t_color == "Gold")
                {
                    color = 2;
                }

                if (cvh[i].t_color == "White" && color > 2)
                {
                    color = 3;
                }

                if (cvh[i].t_color == "Blue" && color > 3)
                {
                    color = 4;
                }

                if (cvh[i].t_color == "Black" && color > 4)
                {
                    color = 5;
                }
            }

            if (color == 2)
                return "Gold";
            else if (color == 3)
                return "White";
            else if (color == 4)
                return "Blue";
            else if (color == 5)
                return "Black";
            else
                return "Error";


        }

        private void clearHoleLetterParYardage()
        {
            TextBlock[] l = { holel1_tbl, holel2_tbl, holel3_tbl, holel4_tbl, holel5_tbl, holel6_tbl, holel7_tbl, holel8_tbl, holel9_tbl, holel10_tbl, holel11_tbl, holel12_tbl, holel13_tbl, holel14_tbl, holel15_tbl, holel16_tbl, holel17_tbl, holel18_tbl };
            TextBlock[] p = { holep1_tbl, holep2_tbl, holep3_tbl, holep4_tbl, holep5_tbl, holep6_tbl, holep7_tbl, holep8_tbl, holep9_tbl, holep10_tbl, holep11_tbl, holep12_tbl, holep13_tbl, holep14_tbl, holep15_tbl, holep16_tbl, holep17_tbl, holep18_tbl };
            TextBlock[] y = { holey1_tbl, holey2_tbl, holey3_tbl, holey4_tbl, holey5_tbl, holey6_tbl, holey7_tbl, holey8_tbl, holey9_tbl, holey10_tbl, holey11_tbl, holey12_tbl, holey13_tbl, holey14_tbl, holey15_tbl, holey16_tbl, holey17_tbl, holey18_tbl };

            for (int i = 0; i<18; i++)
            {
                l[i].Text = "";
                p[i].Text = "";
                y[i].Text = "";
            }
        }

        private void setRadioButtonColor(string color)
        {
            RadioButton[] r = { cr1_rb, cr2_rb, cr3_rb, cr4_rb, cr5_rb, cr6_rb, cr7_rb, cr8_rb, cr9_rb, cr10_rb, cr11_rb, cr12_rb, cr13_rb, cr14_rb, cr15_rb, cr16_rb, cr17_rb, cr18_rb };
            RadioButton[] g = { cg1_rb, cg2_rb, cg3_rb, cg4_rb, cg5_rb, cg6_rb, cg7_rb, cg8_rb, cg9_rb, cg10_rb, cg11_rb, cg12_rb, cg13_rb, cg14_rb, cg15_rb, cg16_rb, cg17_rb, cg18_rb };
            RadioButton[] w = { cw1_rb, cw2_rb, cw3_rb, cw4_rb, cw5_rb, cw6_rb, cw7_rb, cw8_rb, cw9_rb, cw10_rb, cw11_rb, cw12_rb, cw13_rb, cw14_rb, cw15_rb, cw16_rb, cw17_rb, cw18_rb };
            RadioButton[] b = { cb1_rb, cb2_rb, cb3_rb, cb4_rb, cb5_rb, cb6_rb, cb7_rb, cb8_rb, cb9_rb, cb10_rb, cb11_rb, cb12_rb, cb13_rb, cb14_rb, cb15_rb, cb16_rb, cb17_rb, cb18_rb };
            RadioButton[] l = { cl1_rb, cl2_rb, cl3_rb, cl4_rb, cl5_rb, cl6_rb, cl7_rb, cl8_rb, cl9_rb, cl10_rb, cl11_rb, cl12_rb, cl13_rb, cl14_rb, cl15_rb, cl16_rb, cl17_rb, cl18_rb };

            if (color == "Red")
            {
                for(int i = 0; i<r.Count(); i++)
                {
                    r[i].IsChecked = true;
                }
            }
            else if (color == "Gold")
            {
                for (int i = 0; i < g.Count(); i++)
                {
                    g[i].IsChecked = true;
                }
            }
            else if (color == "White")
            {
                for (int i = 0; i < w.Count(); i++)
                {
                    w[i].IsChecked = true;
                }
            }
            else if (color == "Blue")
            {
                for (int i = 0; i < b.Count(); i++)
                {
                    b[i].IsChecked = true;
                }
            }
            else if (color == "Black")
            {
                for (int i = 0; i < l.Count(); i++)
                {
                    l[i].IsChecked = true;
                }
            }
            else
            {
                return;
            }


        }

        private int checkHoleNumHigh()
        {
            int count = 0;
            for (int i = 0; i < cvh.Count(); i++)
            {
                if(int.Parse(cvh[i].h_num) > count && cvh[i].t_color == cur_color)
                {
                    count = int.Parse(cvh[i].h_num);
                }
            }

            return count;
        }

        private void hideNonUsedHoles()
        {
            Grid[] g = { Hole1_g, Hole2_g, Hole3_g, Hole4_g, Hole5_g, Hole6_g, Hole7_g, Hole8_g, Hole9_g, Hole10_g, Hole11_g, Hole12_g, Hole13_g, Hole14_g, Hole15_g, Hole16_g, Hole17_g, Hole18_g };
            int count = cur_high - cur_num;
            for(int i = 17; i> count; i--)
            {
                if(g[i].Visibility == Visibility.Visible)
                    g[i].Visibility = Visibility.Hidden;
            }
        }

        private void unhideUsedHoles()
        {
            Grid[] g = { Hole1_g, Hole2_g, Hole3_g, Hole4_g, Hole5_g, Hole6_g, Hole7_g, Hole8_g, Hole9_g, Hole10_g, Hole11_g, Hole12_g, Hole13_g, Hole14_g, Hole15_g, Hole16_g, Hole17_g, Hole18_g };
            int count = cur_high - cur_num;
            if(count > 17)
            {
                count = 17;
            }
            for(int i = 0; i<=count; i++)
            {
                if (g[i].Visibility == Visibility.Hidden)
                    g[i].Visibility = Visibility.Visible;
            }
        }

        private void disableRadioButtons()
        {
            RadioButton[] r = { cr1_rb, cr2_rb, cr3_rb, cr4_rb, cr5_rb, cr6_rb, cr7_rb, cr8_rb, cr9_rb, cr10_rb, cr11_rb, cr12_rb, cr13_rb, cr14_rb, cr15_rb, cr16_rb, cr17_rb, cr18_rb };
            RadioButton[] g = { cg1_rb, cg2_rb, cg3_rb, cg4_rb, cg5_rb, cg6_rb, cg7_rb, cg8_rb, cg9_rb, cg10_rb, cg11_rb, cg12_rb, cg13_rb, cg14_rb, cg15_rb, cg16_rb, cg17_rb, cg18_rb };
            RadioButton[] w = { cw1_rb, cw2_rb, cw3_rb, cw4_rb, cw5_rb, cw6_rb, cw7_rb, cw8_rb, cw9_rb, cw10_rb, cw11_rb, cw12_rb, cw13_rb, cw14_rb, cw15_rb, cw16_rb, cw17_rb, cw18_rb };
            RadioButton[] b = { cb1_rb, cb2_rb, cb3_rb, cb4_rb, cb5_rb, cb6_rb, cb7_rb, cb8_rb, cb9_rb, cb10_rb, cb11_rb, cb12_rb, cb13_rb, cb14_rb, cb15_rb, cb16_rb, cb17_rb, cb18_rb };
            RadioButton[] l = { cl1_rb, cl2_rb, cl3_rb, cl4_rb, cl5_rb, cl6_rb, cl7_rb, cl8_rb, cl9_rb, cl10_rb, cl11_rb, cl12_rb, cl13_rb, cl14_rb, cl15_rb, cl16_rb, cl17_rb, cl18_rb };
            for (int i = 0; i <= 17; i++)
            {
                r[i].IsEnabled = false;
                g[i].IsEnabled = false;
                w[i].IsEnabled = false;
                b[i].IsEnabled = false;
                l[i].IsEnabled = false;
            }
        }

        private void enableRadioButtons()
        {
            RadioButton[] r = { cr1_rb, cr2_rb, cr3_rb, cr4_rb, cr5_rb, cr6_rb, cr7_rb, cr8_rb, cr9_rb, cr10_rb, cr11_rb, cr12_rb, cr13_rb, cr14_rb, cr15_rb, cr16_rb, cr17_rb, cr18_rb };
            RadioButton[] g = { cg1_rb, cg2_rb, cg3_rb, cg4_rb, cg5_rb, cg6_rb, cg7_rb, cg8_rb, cg9_rb, cg10_rb, cg11_rb, cg12_rb, cg13_rb, cg14_rb, cg15_rb, cg16_rb, cg17_rb, cg18_rb };
            RadioButton[] w = { cw1_rb, cw2_rb, cw3_rb, cw4_rb, cw5_rb, cw6_rb, cw7_rb, cw8_rb, cw9_rb, cw10_rb, cw11_rb, cw12_rb, cw13_rb, cw14_rb, cw15_rb, cw16_rb, cw17_rb, cw18_rb };
            RadioButton[] b = { cb1_rb, cb2_rb, cb3_rb, cb4_rb, cb5_rb, cb6_rb, cb7_rb, cb8_rb, cb9_rb, cb10_rb, cb11_rb, cb12_rb, cb13_rb, cb14_rb, cb15_rb, cb16_rb, cb17_rb, cb18_rb };
            RadioButton[] l = { cl1_rb, cl2_rb, cl3_rb, cl4_rb, cl5_rb, cl6_rb, cl7_rb, cl8_rb, cl9_rb, cl10_rb, cl11_rb, cl12_rb, cl13_rb, cl14_rb, cl15_rb, cl16_rb, cl17_rb, cl18_rb };
            int stop = cur_num + 17;
            for(int i = 0; i<cvh.Count(); i++)
            {
                if(int.Parse(cvh[i].h_num) >= cur_num && int.Parse(cvh[i].h_num) <= stop)
                {
                    if (cvh[i].t_color == "Red")
                    {
                        if (r[int.Parse(cvh[i].h_num) - cur_num].IsEnabled == false)
                            r[int.Parse(cvh[i].h_num) - cur_num].IsEnabled = true;
                    }
                    else if (cvh[i].t_color == "Gold")
                    {
                        if (g[int.Parse(cvh[i].h_num) - cur_num].IsEnabled == false)
                            g[int.Parse(cvh[i].h_num) - cur_num].IsEnabled = true;
                    }
                    else if (cvh[i].t_color == "White")
                    {
                        if (w[int.Parse(cvh[i].h_num) - cur_num].IsEnabled == false)
                            w[int.Parse(cvh[i].h_num) - cur_num].IsEnabled = true;
                    }
                    else if (cvh[i].t_color == "Blue")
                    {
                        if (b[int.Parse(cvh[i].h_num) - cur_num].IsEnabled == false)
                            b[int.Parse(cvh[i].h_num) - cur_num].IsEnabled = true;
                    }
                    else if (cvh[i].t_color == "Black")
                    {
                        if (l[int.Parse(cvh[i].h_num) - cur_num].IsEnabled == false)
                            l[int.Parse(cvh[i].h_num) - cur_num].IsEnabled = true;
                    }
                    else
                    {
                        return;
                    }
                }
                
            }
            
        }
        /***** End Help Funcitons            ****/
    }
}
