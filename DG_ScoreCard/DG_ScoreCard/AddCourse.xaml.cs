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
using DG_ScoreCard.DGserviceReference;

namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for AddCourse.xaml
    /// </summary>
    public partial class AddCourse : Page
    {

        DGserviceClient client = new DGserviceClient();
        List<holeLib> holeList = new List<holeLib>();
        int hole_count = 18;
        private string username = "NULL";
        public AddCourse()
        {
            InitializeComponent();
            updateHoleCount();          

        }
        public AddCourse(string user)
        {
            InitializeComponent();
            username = user;
            updateHoleCount();
        }

        /********* Move to Different Page *******/

        //Desc: Changes to Holes Grid
        private void addholes_btn_Click(object sender, RoutedEventArgs e)
        {
            Main_g.Visibility = Visibility.Hidden;
            if (complex_rb.IsChecked == true)
            {
                Complex_g.Visibility = Visibility.Visible;
                Custom_g.Visibility = Visibility.Hidden;
                Simple_g.Visibility = Visibility.Hidden;

            }
            else if (custom_rb.IsChecked == true)
            {
                Custom_g.Visibility = Visibility.Visible;
                Complex_g.Visibility = Visibility.Hidden;
                Simple_g.Visibility = Visibility.Hidden;
            }
            else
            {
                Simple_g.Visibility = Visibility.Visible;
                Complex_g.Visibility = Visibility.Hidden;
                Custom_g.Visibility = Visibility.Hidden;
            }

        }
        //Desc: Changes to Main Grid
        private void main_btn_Click(object sender, RoutedEventArgs e)
        {
            Custom_g.Visibility = Visibility.Hidden;
            Simple_g.Visibility = Visibility.Hidden;
            Complex_g.Visibility = Visibility.Hidden;
            Main_g.Visibility = Visibility.Visible;
        }

        private void simple_rb_Click(object sender, RoutedEventArgs e)
        {
            if (Main_g.Visibility == Visibility.Hidden && Simple_g.Visibility == Visibility.Hidden)
            {
                Custom_g.Visibility = Visibility.Hidden;
                Complex_g.Visibility = Visibility.Hidden;
                Simple_g.Visibility = Visibility.Visible;
            }
        }

        private void complex_rb_Click(object sender, RoutedEventArgs e)
        {
            if (Main_g.Visibility == Visibility.Hidden && Complex_g.Visibility == Visibility.Hidden)
            {
                Custom_g.Visibility = Visibility.Hidden;
                Simple_g.Visibility = Visibility.Hidden;
                Complex_g.Visibility = Visibility.Visible;
            }
        }

        private void custom_rb_Click(object sender, RoutedEventArgs e)
        {
            if (Main_g.Visibility == Visibility.Hidden)
            {
                Simple_g.Visibility = Visibility.Hidden;
                Complex_g.Visibility = Visibility.Hidden;
                Custom_g.Visibility = Visibility.Visible;
            }
        }



        /****************************************/

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
            if (yes.IsChecked == true)
            {
                return 'T';
            }
            if (no.IsChecked == true)
            {
                return 'F';
            }

            return 'N';
        }

        /***** Custom Page Moving *****/

        Point m_start;
        Vector m_startOffset;

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(course_designer_g);
            m_startOffset = new Vector(basictt.X, basictt.Y);
            basicmove_r.CaptureMouse();

        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (basicmove_r.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(course_designer_g), m_start);
                basictt.X = m_startOffset.X + offset.X;
                basictt.Y = m_startOffset.Y + offset.Y;
            }

        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            basicmove_r.ReleaseMouseCapture();
        }

        private void teeinfo_r_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(course_designer_g);
            m_startOffset = new Vector(teeinfott.X, teeinfott.Y);
            teeinfo_r.CaptureMouse();
        }

        private void teeinfo_r_MouseMove(object sender, MouseEventArgs e)
        {
            if (teeinfo_r.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(course_designer_g), m_start);
                teeinfott.X = m_startOffset.X + offset.X;
                teeinfott.Y = m_startOffset.Y + offset.Y;
            }
        }

        private void teeinfo_r_MouseUp(object sender, MouseButtonEventArgs e)
        {
            teeinfo_r.ReleaseMouseCapture();
        }

        private void basketloc_r_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(course_designer_g);
            m_startOffset = new Vector(basketloctt.X, basketloctt.Y);
            basketloc_r.CaptureMouse();
        }

        private void basketloc_r_MouseMove(object sender, MouseEventArgs e)
        {
            if (basketloc_r.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(course_designer_g), m_start);
                basketloctt.X = m_startOffset.X + offset.X;
                basketloctt.Y = m_startOffset.Y + offset.Y;
            }
        }

        private void basketloc_r_MouseUp(object sender, MouseButtonEventArgs e)
        {
            basketloc_r.ReleaseMouseCapture();
        }

        private void holepic_r_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(course_designer_g);
            m_startOffset = new Vector(holepictt.X, holepictt.Y);
            holepic_r.CaptureMouse();
        }

        private void holepic_r_MouseMove(object sender, MouseEventArgs e)
        {
            if (holepic_r.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(course_designer_g), m_start);
                holepictt.X = m_startOffset.X + offset.X;
                holepictt.Y = m_startOffset.Y + offset.Y;
            }
        }

        private void holepic_r_MouseUp(object sender, MouseButtonEventArgs e)
        {
            holepic_r.ReleaseMouseCapture();
        }

        private void holeinfomove_r_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(course_designer_g);
            m_startOffset = new Vector(holeinfott.X, holeinfott.Y);
            holeinfomove_r.CaptureMouse();
        }

        private void holeinfomove_r_MouseMove(object sender, MouseEventArgs e)
        {
            if (holeinfomove_r.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(course_designer_g), m_start);
                holeinfott.X = m_startOffset.X + offset.X;
                holeinfott.Y = m_startOffset.Y + offset.Y;
            }
        }

        private void holeinfomove_r_MouseUp(object sender, MouseButtonEventArgs e)
        {
            holeinfomove_r.ReleaseMouseCapture();
        }

        private void holelines_r_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(course_designer_g);
            m_startOffset = new Vector(holelinestt.X, holelinestt.Y);
            holelines_r.CaptureMouse();
        }

        private void holelines_r_MouseMove(object sender, MouseEventArgs e)
        {
            if (holelines_r.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(course_designer_g), m_start);
                holelinestt.X = m_startOffset.X + offset.X;
                holelinestt.Y = m_startOffset.Y + offset.Y;
            }
        }

        private void holelines_r_MouseUp(object sender, MouseButtonEventArgs e)
        {
            holelines_r.ReleaseMouseCapture();
        }

        private void misc_r_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(course_designer_g);
            m_startOffset = new Vector(misctt.X, misctt.Y);
            misc_r.CaptureMouse();
        }

        private void misc_r_MouseMove(object sender, MouseEventArgs e)
        {
            if (misc_r.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(course_designer_g), m_start);
                misctt.X = m_startOffset.X + offset.X;
                misctt.Y = m_startOffset.Y + offset.Y;
            }
        }

        private void misc_r_MouseUp(object sender, MouseButtonEventArgs e)
        {
            misc_r.ReleaseMouseCapture();
        }


        /*****************************************************************************/


        /***** Custom Side Panel Buttons *****/

        private void teeinfo_btn_Click(object sender, RoutedEventArgs e)
        {
            if(teeinfo_g.Visibility == Visibility.Hidden)
            {
                teeinfo_g.Visibility = Visibility.Visible;
            }
            else
            {
                teeinfo_g.Visibility = Visibility.Hidden;
            }
        }

        private void basics_btn_Click(object sender, RoutedEventArgs e)
        {
            if(basic_g.Visibility == Visibility.Hidden)
            {
                basic_g.Visibility = Visibility.Visible;
            }
            else
            {
                basic_g.Visibility = Visibility.Hidden;
            }
        }

        private void basketloc_btn_Click(object sender, RoutedEventArgs e)
        {
           if(basketloc_g.Visibility == Visibility.Hidden)
            {
                basketloc_g.Visibility = Visibility.Visible;
            }
           else
            {
                basketloc_g.Visibility = Visibility.Hidden;
            }
        }

        private void holepicture_btn_Click(object sender, RoutedEventArgs e)
        {
            if(holepic_g.Visibility == Visibility.Hidden)
            {
                holepic_g.Visibility = Visibility.Visible;
            }
            else
            {
                holepic_g.Visibility = Visibility.Hidden;
            }
        }

        private void holeinfo_btn_Click(object sender, RoutedEventArgs e)
        {
            if(holeinfo_g.Visibility == Visibility.Hidden)
            {
                holeinfo_g.Visibility = Visibility.Visible;
            }
            else
            {
                holeinfo_g.Visibility = Visibility.Hidden;
            }
        }

        private void holelines_btn_Click(object sender, RoutedEventArgs e)
        {
            if(holelines_g.Visibility == Visibility.Hidden)
            {
                holelines_g.Visibility = Visibility.Visible;
            }
            else
            {
                holelines_g.Visibility = Visibility.Hidden;
            }
        }

        private void misc_btn_Click(object sender, RoutedEventArgs e)
        {
            if(misc_g.Visibility == Visibility.Hidden)
            {
                misc_g.Visibility = Visibility.Visible;
            }
            else
            {
                misc_g.Visibility = Visibility.Hidden;
            }
        }

        private void deselectall_btn_Click(object sender, RoutedEventArgs e)
        {
            basic_g.Visibility = Visibility.Hidden;
            holeinfo_g.Visibility = Visibility.Hidden;
            teeinfo_g.Visibility = Visibility.Hidden;
            basketloc_g.Visibility = Visibility.Hidden;
            holepic_g.Visibility = Visibility.Hidden;
            holelines_g.Visibility = Visibility.Hidden;
            misc_g.Visibility = Visibility.Hidden;
        }

        private void selectall_btn_Click(object sender, RoutedEventArgs e)
        {
            basic_g.Visibility = Visibility.Visible;
            holeinfo_g.Visibility = Visibility.Visible;
            teeinfo_g.Visibility = Visibility.Visible;
            basketloc_g.Visibility = Visibility.Visible;
            holepic_g.Visibility = Visibility.Visible;
            holelines_g.Visibility = Visibility.Visible;
            misc_g.Visibility = Visibility.Visible;
        }

        /***************************************************************/

        /***** Custom *******/

        private void teecolor_cb_DropDownClosed(object sender, EventArgs e)
        {
            bool hole_exists = false;
            //Steralize Data


            if(yardage_tb.Text == "" && par_cb.Text == "")
            {
                MessageBox.Show("Basic Criteria not filled out, progress will be lost.");
                //Create Go/No Go
            }
            else
            {
                //Check if Update vs Insert
                //hole_exists = checkHoleExists(holeList, )

                
                          
               
            }
           

        }

        /****************************************************************/

        /*** holeLib/List<HoleLib> functions*****/

        //Desc: Creates new hole using custom hole values
        //Post: Returns holeLib
       private holeLib setHole(int num, int yard, int par, string name, char? mando, char? hazard, char? letter, int deduction, string b_note, string color, string pad_type, string t_note, string guide, char? trash, char? trail, char? road, string comments, string shot, string disc)
        {
            holeLib h = new holeLib();
            h.h_num = num;
            h.h_yardage = yard;
            h.h_par = (par);
            h.h_name = name;
            h.h_mando = mando;
            h.h_hazzards = hazard;
            h.b_letter = letter;
            h.b_deduction = deduction;
            h.b_note = b_note;
            h.t_color = color;
            h.t_pad_type = pad_type;
            h.t_notes = t_note;
            h.m_guide = guide;
            h.m_trash = trash;
            h.m_trail = trail;
            h.m_road = road;
            h.m_general_comments = comments;
            h.r_disc = disc;
            h.r_shots = shot;
            return h;
        }

        //Desc: Checks if tee color and placement exist in hole list
        //Post: return true if exists
        private bool checkHoleExists(List<holeLib> h, int num, string color, char? letter)
        {
            for(int i = 0; i<h.Count(); i++)
            {
                if(h[i].h_num == num && h[i].t_color == color && h[i].b_letter == letter)
                {
                    return true;
                }
            }
            return false;
        }

        //Updates hole count lables
        private void updateHoleCount()
        {
            hole_count_complex_tbl.Text = "Hole Count: " + hole_count;
            hole_count_custom_tbl.Text = "Hole Count: " + hole_count;
            hole_count_simple_tbl.Text = "Hole Count: " + hole_count;
        }

        //Desc: Checks if hole count would go below 0 or above 36
        //Post: Return true if above 36 or below 0
        private bool checkHoleCount(int delta)
        {
            int count = hole_count;
            count += delta;
            if(count < 0 || count > 36)
            {
                return true;
            }
            return false;
        }
        /*************************************************************/

        /*** simple add/sub hole btns ****/
        private void simple_add1_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(1) == true) return;
            hole_count++;
            updateHoleCount();
        }

        private void simple_sub1_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(-1) == true) return;
            hole_count--;
            updateHoleCount();
        }

        private void simple_add9_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(9) == true) return;
            hole_count += 9;
            updateHoleCount();
        }

        private void simple_sub9_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(-9) == true) return;
            hole_count -= 9;
            updateHoleCount();
        }

        private void simple_add18_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(18) == true) return;
            hole_count += 18;
            updateHoleCount();
        }

        private void simple_sub18_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(-18) == true) return;
            hole_count -= 18;
            updateHoleCount();
        }
        /***************************************************************/



    }
}
