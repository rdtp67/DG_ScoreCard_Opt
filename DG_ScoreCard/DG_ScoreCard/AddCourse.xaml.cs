﻿using System;
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
        private int g_user_id = 0;

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
            g_user_id = client.getUserID(username);
        }

        /********* Move to Different Page *******/

        //Desc: Changes to Holes Grid
        private void addholes_btn_Click(object sender, RoutedEventArgs e)
        {
            Main_g.Visibility = Visibility.Hidden;
            submit_btn.Visibility = Visibility.Visible;
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

            if(holeList.Count() == 0)
            {
                intializeHoleListRun();
            }

        }
        //Desc: Changes to Main Grid
        private void main_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Simple_g.Visibility == Visibility.Visible)
                setHoleListSimple();
            if (Custom_g.Visibility == Visibility.Visible)
                setHoleListcustom();
            if (Complex_g.Visibility == Visibility.Visible)
                updateComplexHoles();
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

            if(Complex_g.Visibility == Visibility.Visible)
            {
                updateComplexHoles();
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
                setComplexHoles();
            }

        }

        private void custom_rb_Click(object sender, RoutedEventArgs e)
        {
            if (Simple_g.Visibility == Visibility.Visible)
            {
                setHoleListSimple();
            }      
            if(Complex_g.Visibility == Visibility.Visible)
            {
                updateComplexHoles();
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
            /* Checks if user has already created a course of the name and directs them to edit the course instead 
               The function will return here if call is true                                                        */
            int u = client.getUserID(username);
            bool cnuexist = client.checkCourseUserExists(u, coursename_tb1.Text);
            if(cnuexist == true)
            {
                MessageBox.Show("You have already created a course of this name.\nPlease Edit Course to make changes!");
                coursename_tb1.BorderBrush = Brushes.Red;
                //send to edit page in the future?
                return;
            }
            else
            {
                coursename_tb1.BorderBrush = Brushes.Black;
            }
            /*******************************************************************************************************/


            /* Verify field lengths, will display an error message if field length(s) are too long, highlight in 
                red the field, then return                                                                          */
            string length_error_message = checkSubmitButtonLengths();
            if (length_error_message != "")
            {
                MessageBox.Show(length_error_message);
                return;
            }

            /******************************************************************************************************/

            /* Checks specified erros, returns if they are true */
            string errors_found = checkSubmitButtonErrors();

            if (errors_found != "")
            {
                MessageBox.Show(errors_found);
                return;
            }

            /******************************************************************************************************/

            if (Simple_g.Visibility == Visibility.Visible)
                setHoleListSimple();
            if (Custom_g.Visibility == Visibility.Visible)
                setHoleListcustom();
            if (Complex_g.Visibility == Visibility.Visible)
                updateComplexHoles();

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
            string load_messages = "Load Notes: \n";
            load_messages += client.Load_Course_Store_Prod(parkname_tb.Text, hightime_cb.Text, lowtime_cb.Text, p_guide, p_pet, p_private, address_tb1.Text, state_tb1.Text, city_tb1.Text,
            country_tb1.Text, zip_tb1.Text, g_user_id.ToString(), coursename_tb1.Text, website_tb1.Text, phonenumber_tb1.Text, email_tbl.Text, basket_tb.Text, year_established_tb.Text, tee_type_cb.Text, course_type_cb.Text, terrain_cb.Text, basket_maker_tb.Text, c_private, c_p2p, c_guide, course_designer_tb.Text) + "\n";
            for (int i = holeList.Count()-1; i >= 0; i--)
            {
                if (holeList[i].h_num > hole_count)
                {
                    holeList.RemoveAt(i);
                }

            }

            load_messages += client.Load_Holes_Stored_Proc(holeList, client.getCourseID2(g_user_id, coursename_tb1.Text).ToString());
 
            MessageBox.Show(load_messages);

            
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

        /*** Submit button funcitons ***/

        //Desc: Verifies field lenghs of Submit button fields
        //Post: Returns string errors
        private string checkSubmitButtonLengths()
        {
            string errormessage = "";

            if(GenLib.isField50Chars(coursename_tb1.Text) == false)
            {
                errormessage += "Error ~ Course Name field must be 50 characters or less!\n";
                coursename_tb1.BorderBrush = Brushes.Red;
            }
            else
            {
                coursename_tb1.BorderBrush = Brushes.Black;
            }

            if (GenLib.isField50Chars(website_tb1.Text) == false)
            {
                errormessage += "Error ~ Website field must be 50 characters or less!\n";
                website_tb1.BorderBrush = Brushes.Red;
            }
            else
            {
                website_tb1.BorderBrush = Brushes.Black;
            }

            if(GenLib.isField15Chars(phonenumber_tb1.Text) == false)
            {
                errormessage += "Error ~ Phone number field must be 15 characters or less!\n";
                phonenumber_tb1.BorderBrush = Brushes.Red;
            }
            else
            {
                phonenumber_tb1.BorderBrush = Brushes.Black;
            }

            if (GenLib.isField50Chars(basket_tb.Text) == false)
            {
                errormessage += "Error ~ Basket Type field must be 50 characters or less!\n";
                basket_tb.BorderBrush = Brushes.Red;
            }
            else
            {
                basket_tb.BorderBrush = Brushes.Black;
            }

            if (GenLib.isField50Chars(course_designer_tb.Text) == false)
            {
                errormessage += "Error ~ Course Designer field must be 50 characters or less!\n";
                course_designer_tb.BorderBrush = Brushes.Red;
            }
            else
            {
                course_designer_tb.BorderBrush = Brushes.Black;
            }

            if (GenLib.isField50Chars(basket_maker_tb.Text) == false)
            {
                errormessage += "Error ~ Basket Maker field must be 50 characters or less!\n";
                basket_maker_tb.BorderBrush = Brushes.Red;
            }
            else
            {
                basket_maker_tb.BorderBrush = Brushes.Black;
            }

            if (GenLib.isField100Chars(address_tb1.Text) == false)
            {
                errormessage += "Error ~ Location Address field must be 100 characters or less!\n";
                address_tb1.BorderBrush = Brushes.Red;
            }
            else
            {
                address_tb1.BorderBrush = Brushes.Black;
            }

            if (GenLib.isField100Chars(city_tb1.Text) == false)
            {
                errormessage += "Error ~ Location City field must be 100 characters or less!\n";
                city_tb1.BorderBrush = Brushes.Red;
            }
            else
            {
                city_tb1.BorderBrush = Brushes.Black;
            }

            if (GenLib.isField15Chars(zip_tb1.Text) == false)
            {
                errormessage += "Error ~ Location Zip field must be 15 characters or less!\n";
                zip_tb1.BorderBrush = Brushes.Red;
            }
            else
            {
                zip_tb1.BorderBrush = Brushes.Black;
            }

            if (GenLib.isField30Chars(state_tb1.Text) == false)
            {
                errormessage += "Error ~ Location State field must be 30 characters or less!\n";
                state_tb1.BorderBrush = Brushes.Red;
            }
            else
            {
                state_tb1.BorderBrush = Brushes.Black;
            }

            if (GenLib.isField50Chars(country_tb1.Text) == false)
            {
                errormessage += "Error ~ Location Country field must be 50 characters or less!\n";
                country_tb1.BorderBrush = Brushes.Red;
            }
            else
            {
                country_tb1.BorderBrush = Brushes.Black;
            }

            if (GenLib.isField50Chars(parkname_tb.Text) == false)
            {
                errormessage += "Error ~ Park Name field must be 50 characters or less!\n";
                parkname_tb.BorderBrush = Brushes.Red;
            }
            else
            {
                parkname_tb.BorderBrush = Brushes.Black;
            }

            return errormessage;
        }

        //Desc: Checks mandatory button fields for errors
        //Post: Return string errors
        private string checkSubmitButtonErrors()
        {
            if (checkSubmitButtonMandatoryFieldsIsBlank() == true)
            {
                return ("Error ~ Fill in red fields before submiting!");
            }

            if(checkSubmitButtonFieldsNumberic() == false)
            {
                return ("Error ~ Fill in red fields with numeric values before submiting!");
            }

            return "";
        }

        //Desc: Checks submit buttons mandatory fields for blank values
        //Post: returns true if there are blank fields
        private bool checkSubmitButtonMandatoryFieldsIsBlank()
        {
            bool blank = false;
            if(GenLib.isBlank(coursename_tb1.Text) == true)
            {
                coursename_tb1.BorderBrush = Brushes.Red;
                blank = true;
            }
            else
            {
                coursename_tb1.BorderBrush = Brushes.Black;
            }

            return blank;
        }

        //Desc: Checks Submit button fields for numberic values for those that apply
        //Post: returns false if the fields are not numberic
        private bool checkSubmitButtonFieldsNumberic()
        {
            bool isnum = true;
            if(GenLib.isFieldNumberic(zip_tb1.Text) == false && GenLib.isBlank(zip_tb1.Text) == false)
            {
                isnum = false;
                zip_tb1.BorderBrush = Brushes.Red;
            }
            else
            {
                zip_tb1.BorderBrush = Brushes.Black;
            }
            if (GenLib.isFieldNumberic(phonenumber_tb1.Text) == false && GenLib.isBlank(phonenumber_tb1.Text) == false)
            {
                isnum = false;
                phonenumber_tb1.BorderBrush = Brushes.Red;
            }
            else
            {
                phonenumber_tb1.BorderBrush = Brushes.Black;
            }

                return isnum;
        }


        /**End submit btn*****************************/

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
            if(yardagetype_cb.Text == "Ft.")
            {
                h.h_unit = "Feet";
            }
            else if(yardagetype_cb.Text == "Yrd.")
            {
                h.h_unit = "Yard";
            }
            else
            {
                h.h_unit = "Meter";
            }
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
                holeList[getHoleListIterator(holeList, start+1, simpletcolor_cb.Text, 'A')].h_par = int.Parse(cb[i].Text);
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

            holeList[getHoleListIterator(holeList, start+1, simpletcolor_cb.Text, 'A')].h_yardage = int.Parse(tb.Text);
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
                holeList[holeList.Count() - 1].h_name = "";
                holeList[holeList.Count() - 1].h_mando = 'I';
                holeList[holeList.Count() - 1].h_hazzards = 'I';
                holeList[holeList.Count() - 1].t_color = color_intital;
                holeList[holeList.Count() - 1].b_letter = letter_intital;
                holeList[holeList.Count() - 1].b_deduction = deducation_initial;
                holeList[holeList.Count() - 1].b_note = "";
                holeList[holeList.Count() - 1].t_pad_type = "";
                holeList[holeList.Count() - 1].t_notes = "";
                holeList[holeList.Count() - 1].m_guide = "";
                holeList[holeList.Count() - 1].m_trash = 'I';
                holeList[holeList.Count() - 1].m_trail = 'I';
                holeList[holeList.Count() - 1].m_road = 'I';
                holeList[holeList.Count() - 1].m_general_comments = "";
                holeList[holeList.Count() - 1].r_disc = "";
                holeList[holeList.Count() - 1].r_shots = "";

            }
        }

        ////Desc: Initializes first 36 holes based on color
        private void initializeHoleList(string color)
        {
            for (int i = 0; i < 36; i++)
            {
                holeList.Add(new holeLib());
                holeList[holeList.Count() - 1].h_num = i+1;
                holeList[holeList.Count() - 1].h_par = par_initial;
                holeList[holeList.Count() - 1].h_yardage = yardage_intitial;
                holeList[holeList.Count() - 1].h_unit = unit_intitial;
                holeList[holeList.Count() - 1].h_name = "";
                holeList[holeList.Count() - 1].h_mando = 'I';
                holeList[holeList.Count() - 1].h_hazzards = 'I';
                holeList[holeList.Count() - 1].t_color = color;
                holeList[holeList.Count() - 1].b_letter = letter_intital;
                holeList[holeList.Count() - 1].b_deduction = deducation_initial;
                holeList[holeList.Count() - 1].b_note = "";
                holeList[holeList.Count() - 1].t_pad_type = "";
                holeList[holeList.Count() - 1].t_notes = "";
                holeList[holeList.Count() - 1].m_guide = "";
                holeList[holeList.Count() - 1].m_trash = 'I';
                holeList[holeList.Count() - 1].m_trail = 'I';
                holeList[holeList.Count() - 1].m_road = 'I';
                holeList[holeList.Count() - 1].m_general_comments = "";
                holeList[holeList.Count() - 1].r_disc = "";
                holeList[holeList.Count() - 1].r_shots = "";
            }
        }

        //Desc: Initializes holes based on main page
        private void intializeHoleListRun()
        {
            bool ischeck = false;
            if(maintred_chb.IsChecked == false && maintgold_chb.IsChecked == false && maintwhite_chb.IsChecked == false && maintblue_chb.IsChecked == false && maintblack_chb.IsChecked == false)
            {
                initializeHoleList();
                simpletcolor_cb.Visibility = Visibility.Hidden;
            }
            else
            {
                if(maintred_chb.IsChecked == true)
                {
                    initializeHoleList("Red");
                    ischeck = true;
                    last1_color = "Red";
                    last2_color = "Red";
                    last3_color = "Red";
                }
                else
                {
                    tred1.Visibility = Visibility.Hidden;
                    tred1.IsSelected = false;
                    tee_red.IsChecked = false;
                    tee_red1.IsChecked = false;
                    tee_red2.IsChecked = false;
                    tred.IsSelected = false;
                }
                if(maintgold_chb.IsChecked == true)
                {
                    initializeHoleList("Gold");
                    if(ischeck == false)
                    {
                        tgold1.IsSelected = true;
                        tee_gold.IsChecked = true;
                        tee_gold1.IsChecked = true;
                        tee_gold2.IsChecked = true;
                        tgold.IsSelected = true;
                        ischeck = true;
                        last1_color = "Gold";
                        last2_color = "Gold";
                        last3_color = "Gold";
                    }

                }
                else
                {
                    tgold1.Visibility = Visibility.Hidden;
                }
                if (maintwhite_chb.IsChecked == true)
                {
                    initializeHoleList("White");
                    if(ischeck == false)
                    {
                        twhite1.IsSelected = true;
                        tee_white.IsChecked = true;
                        tee_white1.IsChecked = true;
                        tee_white2.IsChecked = true;
                        twhite.IsSelected = true;
                        last1_color = "White";
                        last2_color = "White";
                        last3_color = "White";
                        ischeck = true;
                    }
                }
                else
                {
                    twhite1.Visibility = Visibility.Hidden;
                }
                if (maintblue_chb.IsChecked == true)
                {
                    initializeHoleList("Blue");
                    if (ischeck == false)
                    {
                        tblue1.IsSelected = true;
                        tee_blue.IsChecked = true;
                        tee_blue1.IsChecked = true;
                        tee_blue2.IsChecked = true;
                        tblue.IsSelected = true;
                        last1_color = "Blue";
                        last2_color = "Blue";
                        last3_color = "Blue";
                        ischeck = true;
                    }
                }
                else
                {
                    tblue1.Visibility = Visibility.Hidden;
                }
                if (maintblack_chb.IsChecked == true)
                {
                    initializeHoleList("Black");
                    if (ischeck == false)
                    {
                        tblack1.IsSelected = true;
                        tee_black.IsChecked = true;
                        tee_black1.IsChecked = true;
                        tee_black2.IsChecked = true;
                        tblack.IsSelected = true;
                        last1_color = "Black";
                        last2_color = "Black";
                        last3_color = "Black";
                        ischeck = true;
                    }
                }
                else
                {
                    tblack1.Visibility = Visibility.Hidden;
                }
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

        //Updates simple unit for full holeList
        private void updateUnit(string unit)
        {
            for (int i = 0; i < holeList.Count(); i++)
            {
                holeList[i].h_unit = unit;
            }
        }
        /*************************************************************/

        //Complex***********************************************************************************************************
        string last1_color;
        string last2_color;
        string last3_color;
        int complex_current_hole = 3;
        //Complex Buttons
        private void complex9hole_btn_Click(object sender, RoutedEventArgs e)
        {
            hole_count = 9;
            updateHoleCount();
        }

        private void complex18hole_btn_Click(object sender, RoutedEventArgs e)
        {
            hole_count = 18;
            updateHoleCount();
        }

        private void complex27hole_btn_Click(object sender, RoutedEventArgs e)
        {
            hole_count = 27;
            updateHoleCount();
        }

        private void complexx36hole_btn_Click(object sender, RoutedEventArgs e)
        {
            hole_count = 36;
            updateHoleCount();
        }

        private void complexaddhole_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(1) == false)
            {
                hole_count++;
            }

            updateHoleCount();
        }

        private void complexxremovehole_btn_Click(object sender, RoutedEventArgs e)
        {
            if (checkHoleCount(-1) == false)
            {
                hole_count--;
            }
            updateHoleCount();
        }

        private void complexprv_btn_Click(object sender, RoutedEventArgs e)
        {
            updateComplexHoles();
            if (complex_current_hole > 3)
            {
                complex_current_hole -= 3;
            }
            //Set new Holes
            setComplexHoles();
        }

        private void complexnxt_btn_Click(object sender, RoutedEventArgs e)
        {
            //Save Current Holes
            updateComplexHoles();
            if (complex_current_hole < 36)
            {
                complex_current_hole += 3;
            }

            //Set new Holes
            setComplexHoles();

        }

        private void setComplexColorRadio(int p)
        {
            RadioButton[] t1 = { tee_red, tee_gold, tee_white, tee_blue, tee_black };
            RadioButton[] t2 = { tee_red1, tee_gold1, tee_white1, tee_blue1, tee_black1 };
            RadioButton[] t3 = { tee_red2, tee_gold2, tee_white2, tee_blue2, tee_black2 };
            List<RadioButton[]> t = new List<RadioButton[]>();
            t.Add(t1); t.Add(t2); t.Add(t3);

            if (holeList[0].t_color == "Red")
            {
                t[p][0].IsChecked = true;
            }
            else if (holeList[0].t_color == "Gold")
            {
                t[p][1].IsChecked = true;
            }
            else if (holeList[0].t_color == "White")
            {
                t[p][2].IsChecked = true;
            }
            else if (holeList[0].t_color == "Blue")
            {
                t[p][3].IsChecked = true;
            }
            else
            {
                t[p][4].IsChecked = true;
            }
        }

        private void setComplexHoles()
        {
            if (checkHoleExists(holeList, complex_current_hole - 2, getComplexTeeColor(0), 'A') == false)
            {
                setComplexColorRadio(0);
            }
            if (checkHoleExists(holeList, complex_current_hole - 1, getComplexTeeColor(1), 'A') == false)
            {
                setComplexColorRadio(1);
            }
            if (checkHoleExists(holeList, complex_current_hole, getComplexTeeColor(2), 'A') == false)
            {
                setComplexColorRadio(2);
            }
            setComplexFields();

        }

        private void setComplexFields()
        {
            complexhole1_tbl.Text = "Hole " + (complex_current_hole - 2).ToString();
            complexhole2_tbl.Text = "Hole " + (complex_current_hole - 1).ToString();
            complexhole3_tbl.Text = "Hole " + (complex_current_hole).ToString();
            complex1a_rb.IsChecked = true;
            complex2a_rb.IsChecked = true;
            complex3a_rb.IsChecked = true;
            complexpar1_cb.Text = holeList[getHoleListIterator(holeList, (complex_current_hole - 2), getComplexTeeColor(0), 'A')].h_par.ToString();
            complexpar2_cb.Text = holeList[getHoleListIterator(holeList, (complex_current_hole - 1), getComplexTeeColor(1), 'A')].h_par.ToString();
            complexpar3_cb.Text = holeList[getHoleListIterator(holeList, (complex_current_hole), getComplexTeeColor(2), 'A')].h_par.ToString();
            complexyard1_tb.Text = holeList[getHoleListIterator(holeList, (complex_current_hole - 2), getComplexTeeColor(0), 'A')].h_yardage.ToString();
            complexyard2_tb.Text = holeList[getHoleListIterator(holeList, (complex_current_hole - 1), getComplexTeeColor(1), 'A')].h_yardage.ToString();
            complexyard3_tb.Text = holeList[getHoleListIterator(holeList, (complex_current_hole), getComplexTeeColor(2), 'A')].h_yardage.ToString();
        }

        //Desc: Gets Coresponding Tee Color
        private string getComplexTeeColorArrayColor(int i)
        {
            if(i == 0)
            {
                return "Red";
            }
            else if(i == 1)
            {
                return "Gold";
            }
            else if(i == 2)
            {
                return "White";
            }
            else if(i == 3)
            {
                return "Blue";
            }
            else
            {
                return "Black";
            }
        }

        //Desc: Complex hole tee color
        private string getComplexTeeColor(int i)
        {
            RadioButton[] t1 = {tee_red, tee_gold, tee_white, tee_blue, tee_black};
            RadioButton[] t2 = { tee_red1, tee_gold1, tee_white1, tee_blue1, tee_black1 };
            RadioButton[] t3 = { tee_red2, tee_gold2, tee_white2, tee_blue2, tee_black2 };
            List<RadioButton[]> t = new List<RadioButton[]>();
            t.Add(t1); t.Add(t2);t.Add(t3);

            for(int k=0; k<t[i].Count();k++)
            {
                if(t[i][k].IsChecked == true)
                {
                    return getComplexTeeColorArrayColor(k);
                }
            }

           return "Panic";
        }

        //Desc: Gets Corresponding Basket Letter
        private char getComplexLetterArrayColor(int i)
        {
            if (i == 0)
            {
                return 'A';
            }
            else if (i == 1)
            {
                return 'B';
            }
            else if (i == 2)
            {
                return 'C';
            }
            else if (i == 3)
            {
                return 'D';
            }
            else if (i == 4)
            {
                return 'E';
            }
            else if (i == 5)
            {
                return 'F';
            }
            else if (i == 6)
            {
                return 'G';
            }
            else
            {
                return 'Z';
            }
        }

        //Desc: Get Complex Basket Letter
        private char getComplexLetter(int i)
        {
            RadioButton[] l1 = { complex1a_rb, complex1b_rb, complex1c_rb, complex1d_rb, complex1e_rb };
            RadioButton[] l2 = { complex2a_rb, complex2b_rb, complex2c_rb, complex2d_rb, complex2e_rb };
            RadioButton[] l3 = { complex3a_rb, complex3b_rb, complex3c_rb, complex3d_rb, complex3e_rb };
            List<RadioButton[]> l = new List<RadioButton[]>();
            l.Add(l1); l.Add(l2); l.Add(l3);

            for(int k=0; k<l[i].Count(); k++)
            {
                if(l[i][k].IsChecked == true)
                {
                    return getComplexLetterArrayColor(k);
                }
            }

            return 'Z';
        }

        private int getComplexPar(int i)
        {
            if(i == 0)
            {
                return int.Parse(complexpar1_cb.Text);
            }
            else if(i == 1)
            {
                return int.Parse(complexpar2_cb.Text);
            }
            else
            {
                return int.Parse(complexpar3_cb.Text);
            }
        }

        private int getComplexYard(int i)
        {
            if(i == 0)
            {
                return int.Parse(complexyard1_tb.Text);
            }
            else if(i == 1)
            {
                return int.Parse(complexyard2_tb.Text);
            }
            else
            {
                return int.Parse(complexyard3_tb.Text);
            }
        }

        private void setComplexHoles(int p, int l)
        {
            if (checkHoleExists(holeList, complex_current_hole - p, getComplexTeeColor(l), getComplexLetter(l)) == false)
            {
                setComplexColorRadio(p);
            }
            setComplexFields(p, l);

        }

        private void setComplexFields(int p, int l)
        {
            TextBlock[] tbl = {complexhole1_tbl, complexhole2_tbl, complexhole3_tbl};
            tbl[l].Text = "Hole " + (complex_current_hole - p).ToString();
            RadioButton[] rb = { complex1a_rb, complex2a_rb, complex3a_rb };
            rb[l].IsChecked = true;
            ComboBox[] cb = { complexpar1_cb, complexpar2_cb, complexpar3_cb };
            cb[l].Text = holeList[getHoleListIterator(holeList, (complex_current_hole - p), getComplexTeeColor(l), getComplexLetter(l))].h_par.ToString();
            TextBox[] tb = {complexyard1_tb, complexyard2_tb, complexyard3_tb };
            tb[l].Text = holeList[getHoleListIterator(holeList, (complex_current_hole - p), getComplexTeeColor(l), getComplexLetter(l))].h_yardage.ToString();
           
        }

        //Desc: saves current complex hole to array
        private void saveComplexHoles(int c_current_hole, int p)
        {
            holeList[c_current_hole].t_color = getComplexTeeColor(p);
            holeList[c_current_hole].b_letter = getComplexLetter(p);
            holeList[c_current_hole].h_par = getComplexPar(p);
            holeList[c_current_hole].h_yardage = getComplexYard(p);
        }
        //Desc: Saves holes with past hole color
        private void saveComplexHoles(int c_current_hole, int p, string last_color)
        {
            holeList[c_current_hole].t_color = last_color;
            holeList[c_current_hole].b_letter = getComplexLetter(p);
            holeList[c_current_hole].h_par = getComplexPar(p);
            holeList[c_current_hole].h_yardage = getComplexYard(p);
        }

        private void ComplexTee1_Click(object sender, RoutedEventArgs e)
        {

            updateComplexHoles(2, last1_color, 0);
            setComplexHoles(2, 0);
            last1_color = getComplexTeeColor(0);
        }

        private void ComplexTee2_Click(object sender, RoutedEventArgs e)
        {

            updateComplexHoles(1, last2_color, 1);
            setComplexHoles(1, 1);
            last2_color = getComplexTeeColor(1);
        }

        private void ComplexTee3_Click(object sender, RoutedEventArgs e)
        {

            updateComplexHoles(0, last3_color, 2);
            setComplexHoles(0, 2);
            last3_color = getComplexTeeColor(2);
        }



        //Desc: adds hole of complex hole number to end of array
        private void addComplexHole(int c_current_hole, int p)
        {

            holeLib h = new holeLib();
            h.h_num = c_current_hole;
            h.t_color = getComplexTeeColor(p);
            h.b_letter = getComplexLetter(p);
            h.h_par = getComplexPar(p);
            h.h_yardage = getComplexYard(p);
            holeList.Add(h);
        }

        //Desc: Saves Complex Holes
        private void updateComplexHoles()
        {
            //if hole exists update for 3 current holes
            if (checkHoleExists(holeList, complex_current_hole - 2, getComplexTeeColor(0), getComplexLetter(0)) == false)
            {
                addComplexHole(complex_current_hole - 2, 0);
            }
            if (checkHoleExists(holeList, complex_current_hole - 1, getComplexTeeColor(1), getComplexLetter(1)) == false)
            {
                addComplexHole(complex_current_hole - 1, 1);
            }
            if (checkHoleExists(holeList, complex_current_hole, getComplexTeeColor(2), getComplexLetter(2)) == false)
            {
                addComplexHole(complex_current_hole, 2);
            }
            saveComplexHoles(getHoleListIterator(holeList, (complex_current_hole - 2), getComplexTeeColor(0), getComplexLetter(0)), 0);
            saveComplexHoles(getHoleListIterator(holeList, (complex_current_hole - 1), getComplexTeeColor(1), getComplexLetter(1)), 1);
            saveComplexHoles(getHoleListIterator(holeList, (complex_current_hole), getComplexTeeColor(2), getComplexLetter(2)), 2);
        }

        private void updateComplexHoles(int p, string c, int l)
        {
            //if hole exists update for 3 current holes
            if (checkHoleExists(holeList, complex_current_hole - p, c, getComplexLetter(l)) == false)
            {
                addComplexHole(complex_current_hole - p, l);
            }
            saveComplexHoles(getHoleListIterator(holeList, (complex_current_hole - p), c, getComplexLetter(l)), l, c);
        }

        //RB Buttons
        private void complexfeet_rb_Checked(object sender, RoutedEventArgs e)
        {
            updateUnit("Feet");
            complex1uf_rb.IsChecked = true;
            complex1uy_rb.IsChecked = false;
            complex1um_rm.IsChecked = false;

            complex2uf_rb.IsChecked = true;
            complex2uy_rb.IsChecked = false;
            complex2um_rb.IsChecked = false;

            complex3uf_rb.IsChecked = true;
            complex3uy_rb.IsChecked = false;
            complex3um_rb.IsChecked = false;
            
        }
        private void complexyards_rb_Checked(object sender, RoutedEventArgs e)
        {
            updateUnit("Yards");
            complex1um_rm.IsChecked = false;
            complex1uf_rb.IsChecked = false;
            complex1uy_rb.IsChecked = true;

            complex2uf_rb.IsChecked = false;
            complex2um_rb.IsChecked = false;
            complex2uy_rb.IsChecked = true;

            complex3um_rb.IsChecked = false;
            complex3uf_rb.IsChecked = false;
            complex3uy_rb.IsChecked = true;
        }
        private void complexmeters_rb_Checked(object sender, RoutedEventArgs e)
        {
            updateUnit("Meters");
            complex1uy_rb.IsChecked = false;
            complex1uf_rb.IsChecked = false;
            complex1um_rm.IsChecked = true;

            complex2uf_rb.IsChecked = false;
            complex2uy_rb.IsChecked = false;
            complex2um_rb.IsChecked = true;

            complex3uy_rb.IsChecked = false;
            complex3uf_rb.IsChecked = false;
            complex3um_rb.IsChecked = true;
        }
        //End Complex Buttons

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
            //teecolor_cb.Text = color_intital;
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
           // yardage_tb.Text = yardage_intitial.ToString();
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
                unit = "Yards";
            }
            else if(simple_measurement_cb.Text == "m.")
            {
                unit = "Meters";
            }
            else
            {
                unit = "Error";
            }

            updateUnit(unit);

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
                //cb[i].Text = holeList[start + i].h_par.ToString();
                //MessageBox.Show(start + (start + 1) + simpletcolor_cb.Text);
                cb[i].Text = holeList[getHoleListIterator(holeList, (start + i + 1), simpletcolor_cb.Text, 'A')].h_par.ToString();
               // tb[i].Text = holeList[start + i].h_yardage.ToString();
                tb[i].Text = holeList[getHoleListIterator(holeList, (start + i + 1), simpletcolor_cb.Text, 'A')].h_yardage.ToString();
            }
        }

        private void simpletcolor_cb_DropDownClosed(object sender, EventArgs e)
        {
            updateSimpleHoleVisibility();
            updateSimpleGidValues();
        }

        private void simpletcolor_cb_DropDownOpened(object sender, EventArgs e)
        {
            setHoleListSimple();
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
