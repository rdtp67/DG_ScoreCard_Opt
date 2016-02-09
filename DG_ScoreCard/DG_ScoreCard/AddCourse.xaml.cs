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
        const int min_holes = 1;
        const int max_holes = 36;
        const int par_initial = 3;
        const int yardage_intitial = 0;
        const string unit_intitial = "Feet";
        const string color_intital = "Red";
        const char letter_intital = 'A';
        const int deducation_initial = 0;
        private string username = "NULL";
        public AddCourse()
        {
            InitializeComponent();
            updateHoleCount();
            initializeHoleList();         

        }
        public AddCourse(string user)
        {
            InitializeComponent();
            username = user;
            updateHoleCount();
            initializeHoleList();
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
            if (Simple_g.Visibility == Visibility.Visible)
                setHoleListSimple();
            if (Custom_g.Visibility == Visibility.Visible)
                setHoleListcustom();
            Custom_g.Visibility = Visibility.Hidden;
            Simple_g.Visibility = Visibility.Hidden;
            Complex_g.Visibility = Visibility.Hidden;
            Main_g.Visibility = Visibility.Visible;
        }

        private void simple_rb_Click(object sender, RoutedEventArgs e)
        {
            if (Custom_g.Visibility == Visibility.Visible)
            {
                setHoleListcustom();
            }
                
            if (Main_g.Visibility == Visibility.Hidden && Simple_g.Visibility == Visibility.Hidden)
            {
                Custom_g.Visibility = Visibility.Hidden;
                Complex_g.Visibility = Visibility.Hidden;
                Simple_g.Visibility = Visibility.Visible;
                simple_end = 18;
                updateSimpleHoleVisibility();
                updateSimpleGidValues();
            }
     
        }

        private void complex_rb_Click(object sender, RoutedEventArgs e)
        {
            if (Simple_g.Visibility == Visibility.Visible)
            {
                setHoleListSimple();
            }
            if (Custom_g.Visibility == Visibility.Visible)
            {
                setHoleListcustom();
            }
            if (Main_g.Visibility == Visibility.Hidden && Complex_g.Visibility == Visibility.Hidden)
            {
                Custom_g.Visibility = Visibility.Hidden;
                Simple_g.Visibility = Visibility.Hidden;
                Complex_g.Visibility = Visibility.Visible;
            }

        }

        private void custom_rb_Click(object sender, RoutedEventArgs e)
        {
            if (Simple_g.Visibility == Visibility.Visible)
            {
                setHoleListSimple();
            }      
            if (Main_g.Visibility == Visibility.Hidden && Custom_g.Visibility == Visibility.Hidden)
            {
                Simple_g.Visibility = Visibility.Hidden;
                Complex_g.Visibility = Visibility.Hidden;
                Custom_g.Visibility = Visibility.Visible;
                setCustomFieldsUpdate(getCustumHoleNumberforTeeandColor());
                simpleend_tb.Text = custom_current_hole.ToString();
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

            return 'I';
        }

        /****************************************************************/

        /*** holeLib/List<HoleLib> functions*****/

        //Displays Holes
        private void displayholes_btn_Click(object sender, RoutedEventArgs e)
        {
            string holes = "";
            for(int i = 0; i<holeList.Count(); i++)
            {
                holes += "Hole: " + holeList[i].h_num + " Par: " + holeList[i].h_par + " Unit: " + holeList[i].h_unit + " Yardage: " + holeList[i].h_yardage +  " Hole_name: " + holeList[i].h_name + " Mando: " + holeList[i].h_mando + " Hazzard: " + holeList[i].h_hazzards + " Letter: " + holeList[i].b_letter + " Deduction: " + holeList[i].b_deduction + " Note: " + holeList[i].b_note + "Tee Color: " + holeList[i].t_color + " Pad Type: " + holeList[i].t_pad_type + " Tee Notes: " + holeList[i].t_notes + " Guide: " + holeList[i].m_guide + " Trash: " + holeList[i].m_trash + " Trail: " + holeList[i].m_trail + " Road: " + holeList[i].m_road + " Gen Com: " + holeList[i].m_general_comments + " Shot: " + holeList[i].r_shots + " Disc: " + holeList[i].r_disc + "\n";

            }

            MessageBox.Show(holes);
        }

        //Desc: Creates new hole using custom hole values
        //Post: Returns holeLib
        private holeLib getHoleLibCustom()
        {
            holeLib h = new holeLib();
            h.h_num = custom_current_hole;
            h.h_yardage = int.Parse(yardage_tb.Text);
            h.h_par = int.Parse(par_cb.Text);
            h.h_unit = yardagetype_cb.Text;
            h.h_name = holename_tb.Text;
            h.h_mando = getRadioButton(mandoyes_rb, mandono_rb);
            h.h_hazzards = getRadioButton(hazordyes_rb, hazordno_rb);
            h.b_letter = char.Parse(basketletter_cb.Text);
            h.b_deduction = int.Parse(basketyardage_tb.Text);
            h.b_note = basketnote_tb.Text;
            h.t_color = teecolor_cb.Text;
            h.t_pad_type = teepadtype_cb.Text;
            h.t_notes = teepadtype_cb.Text;
            h.m_guide = miscguidetohole_tb.Text;
            h.m_trash = getRadioButton(trashy_rb, trashno_rb);
            h.m_trail = getRadioButton(traily_rb, trailn_rb);
            h.m_road = getRadioButton(roady_rb, roadn_rb);
            h.m_general_comments = gencomments_tb.Text;
            h.r_disc = recshot_tb.Text;
            h.r_shots = recdisc_tb.Text;
            return h;
        }

        //Desc: Set Hole for simple values
        private void setHoleListSimple()
        {
            ComboBox[] cb = new ComboBox[18] {spar1_cb, spar2_cb, spar3_cb, spar4_cb, spar5_cb, spar6_cb, spar7_cb, spar8_cb, spar9_cb, spar10_cb, spar11_cb, spar12_cb, spar13_cb, spar14_cb, spar15_cb, spar16_cb, spar17_cb, spar18_cb };
            TextBox[] tb = new TextBox[18] {sunit1_tb, sunit2_tb, sunit3_tb, sunit4_tb, sunit5_tb, sunit6_tb, sunit7_tb, sunit8_tb, sunit9_tb, sunit10_tb, sunit11_tb, sunit12_tb, sunit13_tb, sunit14_tb, sunit15_tb, sunit16_tb, sunit17_tb, sunit18_tb };
            int start = simple_end - 18;
            int end;
            if(simple_end > hole_count)
            {
                end = hole_count;
            }
            else
            {
                end = simple_end;
            }
            int i = 0;
            while(start != end)
            {
                holeList[start].h_par = int.Parse(cb[i].Text);
                updateHoleListYardage(start, tb[i]);
                i++;
                start++;
            }
        }

        //Desc: Checks for blank, < 0, and > 9999
        //Pre: start used for holeList hole
        private void updateHoleListYardage(int start, TextBox tb)
        {
            if (GenLib.isBlank(tb.Text) == true)
            {
                tb.Text = "0";
            }
            else if (int.Parse(tb.Text) < 0)
            {
                MessageBox.Show("Yardage cannot be negative!");
                tb.Text = "0";
            }
            else if (int.Parse(tb.Text) > 9999)
            {
                MessageBox.Show("Yardage cannot be greater than 9999!");
                tb.Text = "9999";
            }
            else
            {

            }

            holeList[start].h_yardage = int.Parse(tb.Text);
        }

        //Desc: Add hole to hole list if new holes are added
        private void addHole()
        {
            int count = hole_count - holeList.Count;
            if(count < 1)
            {
                return;
            }
            for(int i = 0; i<count; i++)
            {
                holeList.Add(new holeLib());
            }

            hole_count_tb.Text = holeList.Count().ToString(); //remove
        }

        //Desc: Initializes first 36 holes
        private void initializeHoleList()
        {
            for (int i = 0; i < 36; i++)
            {
                holeList.Add(new holeLib());
                holeList[holeList.Count() - 1].h_num = holeList.Count();
                holeList[holeList.Count() - 1].h_par = par_initial;
                holeList[holeList.Count() - 1].h_yardage = yardage_intitial;
                holeList[holeList.Count() - 1].h_unit = unit_intitial;
                holeList[holeList.Count() - 1].t_color = color_intital;
                holeList[holeList.Count() - 1].b_letter = letter_intital;
                holeList[holeList.Count() - 1].b_deduction = deducation_initial;
            }
        }


        //Desc: Checks if tee color and placement exist in hole list
        //Post: return true if exists
        private bool checkHoleExists(List<holeLib> h, int num, string color, char letter)
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

        //Desc: Gets holelist iterator based on hole num, tee color, and basket letter
        private int getHoleListIterator(List<holeLib> h, int num, string color, char letter)
        {
            for (int i = 0; i < h.Count(); i++)
            {
                if (h[i].h_num == num && h[i].t_color == color && h[i].b_letter == letter)
                {
                    return i;
                }
            }

            return 0;
        }

        //Updates hole count lables
        private void updateHoleCount()
        {
            hole_count_complex_tbl.Text = "Hole Count: " + hole_count;
            hole_count_custom_tbl.Text = "Hole Count: " + hole_count;
            hole_count_simple_tbl.Text = "Hole Count: " + hole_count;
        }

        //Desc: Checks if hole count would go below 0 or above 36
        //Post: Return true if above max_holes or below min
        private bool checkHoleCount(int delta)
        {
            int count = hole_count;
            count += delta;
            if(count < min_holes || count > max_holes)
            {
                return true;
            }
            return false;
        }
        /*************************************************************/

        //Complex***********************************************************************************************************

        
        //Complex End*******************************************************************************************************

        //Custum****************************************************************************************************************
        int custom_current_hole = 1;

        private void teecolor_cb_DropDownOpened(object sender, EventArgs e)
        {
            if (checkHoleExists(holeList, custom_current_hole, teecolor_cb.Text, char.Parse(basketletter_cb.Text)) == true)
            {
                //update hole
                holeList[getHoleListIterator(holeList, custom_current_hole, teecolor_cb.Text, char.Parse(basketletter_cb.Text))] = getHoleLibCustom();
            }
            else
            {
                //create hole and update
                holeList.Add(getHoleLibCustom());
            }
        }
        private void teecolor_cb_DropDownClosed(object sender, EventArgs e)
        {
            int i = getCustumHoleNumberforTeeandColor();

            if (i == -1)
            {
                setCustomFieldsNew(custom_current_hole, teecolor_cb.Text, char.Parse(basketletter_cb.Text));
            }
            else
            {
                setCustomFieldsUpdate(i);
            }
        }

        //Desc: saves custom hole list
        private void setHoleListcustom()
        {
            if(checkHoleExists(holeList, custom_current_hole, teecolor_cb.Text, char.Parse(basketletter_cb.Text)) == true)
            {
                //update hole
                holeList[getHoleListIterator(holeList, custom_current_hole, teecolor_cb.Text, char.Parse(basketletter_cb.Text))] = getHoleLibCustom();
            }
            else
            {
                //create hole and update
                holeList.Add(getHoleLibCustom());
            }
        }
        

        //Desc: Setss custom hole number heading to current hole
        private void setCustomHoleNumberHeading()
        {
            customholeheading_tb.Text = ("Hole " + custom_current_hole.ToString());
        }

        /********** Custom Buttoms **********/
        private void custumback_btn_Click(object sender, RoutedEventArgs e)
        {
            if(checkCustomBack() == true)
            {
                if (checkHoleExists(holeList, custom_current_hole, teecolor_cb.Text, char.Parse(basketletter_cb.Text)) == true)
                {
                    //update hole
                    holeList[getHoleListIterator(holeList, custom_current_hole, teecolor_cb.Text, char.Parse(basketletter_cb.Text))] = getHoleLibCustom();
                }
                else
                {
                    //create hole and update
                    holeList.Add(getHoleLibCustom());
                }
                custom_current_hole--;
                simpleend_tb.Text = custom_current_hole.ToString();
                updateHoleCountCustom();
                customHoleLayoutUpdateOnMove();
            }

        }

        //Desc: Returns true if current hole number is not 1
        private bool checkCustomBack()
        {
            if(custom_current_hole > 1)
            {
                return true;
            }

            return false;

        }

        //Desc: Increments hole count by one if custom current hole > hole count
        private void updateHoleCountCustom()
        {
            if(custom_current_hole>hole_count)
            {
                hole_count++;
            }
        }

        private void custumforward_btn_Click(object sender, RoutedEventArgs e)
        {
            if(checkCustomForward() == true)
            {
               if(checkHoleExists(holeList, custom_current_hole, teecolor_cb.Text, char.Parse(basketletter_cb.Text)) == true)
                {
                    //update hole
                    holeList[getHoleListIterator(holeList, custom_current_hole, teecolor_cb.Text, char.Parse(basketletter_cb.Text))] = getHoleLibCustom();
                }
               else
                {
                    //create hole and update
                    holeList.Add(getHoleLibCustom());      
                }
                custom_current_hole++;
                simpleend_tb.Text = custom_current_hole.ToString();
                updateHoleCountCustom();
                customHoleLayoutUpdateOnMove();
            }
            else
            {
                MessageBox.Show("Hole amount exceded!");
            }
        }

        //Desc: updates custom page with current hole values
        private void customHoleLayoutUpdateOnMove()
        {
            updateHoleCount();
            setCustomHoleNumberHeading();
            setCustomFieldsInitial();
        }

        //Desc: Sets custum fields to current hole and intial settings
        private void setCustomFieldsInitial()
        {
            int i = custom_current_hole - 1;
            yardage_tb.Text = holeList[i].h_yardage.ToString();
            //will not add unit, will pull unit on submit to keep all holes them same
            par_cb.Text = holeList[i].h_par.ToString();
            teecolor_cb.Text = color_intital;
            teepadtype_cb.Text = holeList[i].t_pad_type;
            teepadnotes_tb.Text = holeList[i].t_notes;
            basketletter_cb.Text = letter_intital.ToString();
            basketyardage_tb.Text = holeList[i].b_deduction.ToString();
            basketnote_tb.Text = holeList[i].b_note;
            recshot_tb.Text = holeList[i].r_shots;
            recdisc_tb.Text = holeList[i].r_disc;
            holename_tb.Text = holeList[i].h_name;
            if(holeList[i].h_mando == 'T') { mandoyes_rb.IsChecked = true; } if(holeList[i].h_mando == 'F') { mandono_rb.IsChecked = true; }
            if(holeList[i].h_hazzards == 'T') { hazordyes_rb.IsChecked = true; } if(holeList[i].h_hazzards == 'F') { hazordno_rb.IsChecked = true; }
            miscguidetohole_tb.Text = holeList[i].m_guide;
            if(holeList[i].m_trash == 'T') { trashy_rb.IsChecked = true; } if(holeList[i].m_trash == 'F') { trashno_rb.IsChecked = true; }
            if(holeList[i].m_trail == 'T') { traily_rb.IsChecked = true; } if(holeList[i].m_trail == 'F') { trailn_rb.IsChecked = true; }
            if(holeList[i].m_road == 'T') { roady_rb.IsChecked = true; } if(holeList[i].m_road == 'F') { roadn_rb.IsChecked = true; }
            gencomments_tb.Text = holeList[i].m_general_comments;

        }

        private void setCustomFieldsNew(int hole_num, string color, char letter)
        {
            yardage_tb.Text = yardage_intitial.ToString();
            //will not add unit, will pull unit on submit to keep all holes them same
            par_cb.Text = par_initial.ToString();
           // teecolor_cb.Text = color_intital; current
           //  teepadtype_cb.Text = holeList[i].t_pad_type; current
            teepadnotes_tb.Text = null;
           // basketletter_cb.Text = letter_intital.ToString(); current
            basketyardage_tb.Text = deducation_initial.ToString();
            basketnote_tb.Text = null;
            recshot_tb.Text = null;
            recdisc_tb.Text = null;
          //  holename_tb.Text = null;
            mandoyes_rb.IsChecked = false;
            mandono_rb.IsChecked = false;
            hazordyes_rb.IsChecked = false; 
            hazordno_rb.IsChecked = false; 
            miscguidetohole_tb.Text = null;
            trashy_rb.IsChecked = false; 
            trashno_rb.IsChecked = false; 
            traily_rb.IsChecked = false; 
            trailn_rb.IsChecked = false; 
            roady_rb.IsChecked = false; 
            roadn_rb.IsChecked = false; 
            gencomments_tb.Text = null;

        }
        //Desc: Updates custom fields when tee color is changed or basket letter is changed
        private void setCustomFieldsUpdate(int i)
        {
           
            //find current or use new...
            yardage_tb.Text = holeList[i].h_yardage.ToString();
            //will not add unit, will pull unit on submit to keep all holes them same
            par_cb.Text = holeList[i].h_par.ToString();
           // teecolor_cb.Text = color_intital;
            teepadtype_cb.Text = holeList[i].t_pad_type;
            teepadnotes_tb.Text = holeList[i].t_notes;
           // basketletter_cb.Text = letter_intital.ToString();
            basketyardage_tb.Text = holeList[i].b_deduction.ToString();
            basketnote_tb.Text = holeList[i].b_note;
            recshot_tb.Text = holeList[i].r_shots;
            recdisc_tb.Text = holeList[i].r_disc;
           // holename_tb.Text = holeList[i].h_name;
            if (holeList[i].h_mando == 'T') { mandoyes_rb.IsChecked = true; }
            if (holeList[i].h_mando == 'F') { mandono_rb.IsChecked = true; }
            if (holeList[i].h_hazzards == 'T') { hazordyes_rb.IsChecked = true; }
            if (holeList[i].h_hazzards == 'F') { hazordno_rb.IsChecked = true; }
            miscguidetohole_tb.Text = holeList[i].m_guide;
            if (holeList[i].m_trash == 'T') { trashy_rb.IsChecked = true; }
            if (holeList[i].m_trash == 'F') { trashno_rb.IsChecked = true; }
            if (holeList[i].m_trail == 'T') { traily_rb.IsChecked = true; }
            if (holeList[i].m_trail == 'F') { trailn_rb.IsChecked = true; }
            if (holeList[i].m_road == 'T') { roady_rb.IsChecked = true; }
            if (holeList[i].m_road == 'F') { roadn_rb.IsChecked = true; }
            gencomments_tb.Text = holeList[i].m_general_comments; 
        } 

        //Desc: Returns hole list iterator for custom holes when matches hole number, color, and letter
        private int getCustumHoleNumberforTeeandColor()
        {
            for(int i = 0; i<holeList.Count; i++)
            {
                if(holeList[i].h_num == custom_current_hole && holeList[i].t_color == teecolor_cb.Text && holeList[i].b_letter == char.Parse(basketletter_cb.Text))
                {
                    return i;
                }
            }
            return -1;
        }
        //Desc: if current hole heading < max_holes return true
        private bool checkCustomForward()
        {
            if(custom_current_hole < max_holes)
            {
                return true;
            }

            return false;

        }
        /***********************************/

        //end Custum**********************************************************************************************************************
        //Simple*****************************************************************************************************************        

        /*** simple add/sub hole btns ****/
        private void simple_add1_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(1) == false)
            {
                hole_count++;
            }
            
            updateHoleCount();
            updateSimpleHoleVisibility();
        }

        private void simple_sub1_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(-1) == false)
            {
                hole_count--;
            }
            updateHoleCount();
            updateSimpleHoleVisibility();
            checkSimpleHoleGridVisibility();
        }

        private void simple_add9_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(9) == true)
            {
                hole_count = max_holes;
            }
            else
            {
                hole_count += 9;
            }
            updateHoleCount();
            updateSimpleHoleVisibility();
        }

        private void simple_sub9_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(-9) == true)
            {
                hole_count = min_holes;
            }
            else
            {
                hole_count -= 9;
            }
            
            updateHoleCount();
            updateSimpleHoleVisibility();
            checkSimpleHoleGridVisibility();
        }

        private void simple_add18_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(18) == true)
            {
                hole_count = max_holes;
            }
            else
            {
                hole_count += 18;
            }
            updateHoleCount();
            updateSimpleHoleVisibility();
        }

        private void simple_sub18_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(-18) == true)
            {
                hole_count = min_holes;
            }
            else
            {
                hole_count -= 18;
            }
            updateHoleCount();
            updateSimpleHoleVisibility();
            checkSimpleHoleGridVisibility();
        }

        /***************************************************************/

        /*** Simple Measurement *****/
        private void simple_measurement_cb_DropDownClosed(object sender, EventArgs e)
        {
            string unit;
            if(simple_measurement_cb.Text == "Ft.")
            {
                unit = "Feet";
            }
            else if(simple_measurement_cb.Text == "Yrd.")
            {
                unit = "Yard";
            }
            else if(simple_measurement_cb.Text == "m.")
            {
                unit = "Meter";
            }
            else
            {
                unit = "Error";
            }

            updateSimpleUnit(unit);

            su1_tbl.Text = unit;
            su2_tbl.Text = unit;
            su3_tbl.Text = unit;
            su4_tbl.Text = unit;
            su5_tbl.Text = unit;
            su6_tbl.Text = unit;
            su7_tbl.Text = unit;
            su8_tbl.Text = unit;
            su9_tbl.Text = unit;
            su10_tbl.Text = unit;
            su11_tbl.Text = unit;
            su12_tbl.Text = unit;
            su13_tbl.Text = unit;
            su14_tbl.Text = unit;
            su15_tbl.Text = unit;
            su16_tbl.Text = unit;
            su17_tbl.Text = unit;
            su18_tbl.Text = unit;
        }

        //Updates simple unit for full holeList
        private void updateSimpleUnit(string unit)
        {
            for(int i=0; i<holeList.Count(); i++)
            {
                holeList[i].h_unit = unit;
            }
        }

        /***************************************************************/

        /****** Simple Hole View *****/
        int simple_end = 18;
        private void previous9_btn_Click(object sender, RoutedEventArgs e)
        {
            if (simple_end == 18)
            {
                MessageBox.Show("No more holes to view!");
                return;
            }
            setHoleListSimple();
            simple_end -= 9;
            updateSimpleHoleVisibility();
            updateSimpleGidValues();
            simpleend_tb.Text = simple_end.ToString();
        }

        private void next9_btn_Click(object sender, RoutedEventArgs e)
        {
            if(hole_count <= simple_end)
            {
                MessageBox.Show("No more holes to view!");
                return;
            }
             setHoleListSimple();
            simple_end += 9;
            updateSimpleHoleVisibility();
            updateSimpleGidValues();
            simpleend_tb.Text = simple_end.ToString();
           
        }
        //Consolidate with updatesimplegridvalues()
        private void updateSimpleHoleVisibility()
        {
            
            int start = simple_end - 18;
            for (int i=0; i<18; i++)
            {
                int temp = start;
                temp += i+1;
                if(temp > hole_count)
                {
                    updateSimpleHoleGridVisibility(i, 'H');
                }
                else
                {
                    updateSimpleHoleGridVisibility(i, 'V');
                }
            }
        }

        private void updateSimpleHoleField(int i, string output)
        {
            TextBlock[] simple_hole_tbls = new TextBlock[18] { sh1_tbl, sh2_tbl, sh3_tbl, sh4_tbl, sh5_tbl, sh6_tbl, sh7_tbl, sh8_tbl, sh9_tbl, sh10_tbl, sh11_tbl, sh12_tbl, sh13_tbl, sh14_tbl, sh15_tbl, sh16_tbl, sh17_tbl, sh18_tbl };
            simple_hole_tbls[i].Text = output;
        }

        private void updateSimpleHoleGridVisibility(int i, char c)
        {
            Grid[] simple_hole_grids = new Grid[18]{shole1_g, shole2_g, shole3_g, shole4_g, shole5_g, shole6_g, shole7_g, shole8_g, shole9_g, shole10_g, shole11_g, shole12_g, shole13_g, shole14_g, shole15_g, shole16_g, shole17_g, shole18_g};
            if (c == 'V')
            {
                simple_hole_grids[i].Visibility = Visibility.Visible;
            }
            else if (c == 'H')
            {
                simple_hole_grids[i].Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("We got a problem with updateSimpleHoleGridVisibility Yo ~ ! ~");
            }
            return;
           
        }

        private void checkSimpleHoleGridVisibility()
        {
            if(shole1_g.Visibility == Visibility.Hidden)
            {
                simple_end = 18;
                updateSimpleHoleVisibility();
                updateSimpleGidValues();
            }
        }

        private void updateSimpleGidValues()
        {
            ComboBox[] cb = new ComboBox[18] { spar1_cb, spar2_cb, spar3_cb, spar4_cb, spar5_cb, spar6_cb, spar7_cb, spar8_cb, spar9_cb, spar10_cb, spar11_cb, spar12_cb, spar13_cb, spar14_cb, spar15_cb, spar16_cb, spar17_cb, spar18_cb };
            TextBox[] tb = new TextBox[18] { sunit1_tb, sunit2_tb, sunit3_tb, sunit4_tb, sunit5_tb, sunit6_tb, sunit7_tb, sunit8_tb, sunit9_tb, sunit10_tb, sunit11_tb, sunit12_tb, sunit13_tb, sunit14_tb, sunit15_tb, sunit16_tb, sunit17_tb, sunit18_tb };
            int start = simple_end - 18;
            for(int i=0; i<18; i++)
            {
                updateSimpleHoleField(i, ("Hole " + (start + i + 1)).ToString());
                cb[i].Text = holeList[start + i].h_par.ToString();
                tb[i].Text = holeList[start + i].h_yardage.ToString();
            }
        }


        /******************************************************************/

        //end Simple************************************************************************************************************************************

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
            if (teeinfo_g.Visibility == Visibility.Hidden)
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
            if (basic_g.Visibility == Visibility.Hidden)
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
            if (basketloc_g.Visibility == Visibility.Hidden)
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
            if (holepic_g.Visibility == Visibility.Hidden)
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
            if (holeinfo_g.Visibility == Visibility.Hidden)
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
            if (holelines_g.Visibility == Visibility.Hidden)
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
            if (misc_g.Visibility == Visibility.Hidden)
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
    }
}
