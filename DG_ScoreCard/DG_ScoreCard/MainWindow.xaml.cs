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
using MySql.Data.MySqlClient;



namespace DG_ScoreCard
{

    public partial class MainWindow : Window
    {
        /*****************************************************************************************************************************************************/
        /**************************                                          Variables                                              **************************/
        /*****************************************************************************************************************************************************/

        private Login mainWindow;
        private string username;
        private string sidepanelclosedon = "none";


        public MainWindow()
        {
            username_l.Content = username;
            InitializeComponent();

        }

        public MainWindow(Login mainWindow, string user)
        {
            InitializeComponent();
            username = user;
            username_l.Content = ("Welcome " + username + "!");
            this.mainWindow = mainWindow;
        }

        private void btn_close_loginClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown(); //Shutsdown Application
        }

        private void move3_r_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /************ Button Functions ***********/

        //Sets button light
        private Brush getWindowButtonLightColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFD8D8D8");
            return brush;
        }

        //Sets button generic 
        private Brush getWindowButtonGenericColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFC0C0C0");
            return brush;
        }

        //Sets array of buttons generic
        //Pre: array of buttons
        private void setSidePanelButtonsGenericColor(Button[] btns)
        {
            for(int i = 0; i<btns.Count(); i++)
            {
                btns[i].Background = getWindowButtonGenericColor();
            }
        }

        //Sets button dark
        private Brush getWindowButtonDarkColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FF6C6C6C");
            return brush;
        }

        //Desc: sets non clicked side panel buttons dark, sets pushed panel button light, sets page color to light
        //Pre: array of buttons, button
        private void setSidePanelButtons(Button[] darkBtns, Button lightBtn)
        {
            //sets pages to light backgroud color
            pageload3_f.Background = getWindowButtonLightColor();
            for (int i = 0; i < darkBtns.Count(); i++)
            {
                darkBtns[i].Background = getWindowButtonDarkColor();
            }
            lightBtn.Background = getWindowButtonLightColor();
        }

        //Desc: sets side panel buttons to visible coresponding to top panel button pressed
        //Pre: array of buttons
        private void setSidePanelButtonsVisible(Button[] btns)
        {
            for(int i = 0; i<btns.Count(); i++)
            {
                btns[i].Visibility = Visibility.Visible;
            }
        }

        //Desc: sets side panel buttons to not visible
        //Pre: array of buttons
        private void setSidePanelButtonsNotVisible(Button[] btns)
        {
            for(int i = 0; i<btns.Count(); i++)
            {
                btns[i].Visibility = Visibility.Hidden;
            }
        }

        //Desc: Sets all automatic objects after top panel button is pressed
        private void setTopPanelObjects()
        {
            Button[] round_btns = { round_addround3_btn, round_myround3_btn, round_editround3_btn };
            Button[] course_btns = { course_mycourse3_btn, course_addcourse3_btn, course_editcourse3_btn, course_searchcourse3_btn };
            hidesidepanel_btn.Visibility = Visibility.Visible;
            hidensidepanel_r.Visibility = Visibility.Hidden;
            opensidepanel_btn.Visibility = Visibility.Hidden;
            if(sidepanelclosedon != "round")
            {
                setSidePanelButtonsNotVisible(round_btns);
                setSidePanelButtonsGenericColor(round_btns);
                
            }
            if (sidepanelclosedon != "course")
            {
                setSidePanelButtonsNotVisible(course_btns);
                setSidePanelButtonsGenericColor(course_btns);
            }
        }

        /**************************************/

        /********** TOP Panel Clicked Buttons **********/

        //Desc: Top Panel round button clicked
        private void round3_btn_Click(object sender, RoutedEventArgs e)
        {
            sidepanelclosedon = "round";
            Button[] round_btns = { round_addround3_btn, round_myround3_btn, round_editround3_btn };
            setSidePanelButtonsVisible(round_btns);
            setTopPanelObjects();
           
        }

        private void course3_btn_Click(object sender, RoutedEventArgs e)
        {
            sidepanelclosedon = "course";
            Button[] course_btns = { course_mycourse3_btn, course_addcourse3_btn, course_editcourse3_btn, course_searchcourse3_btn };
            setSidePanelButtonsVisible(course_btns);
            setTopPanelObjects();
           
        }

        /***********************************************/

        /********** Side Panel Clicked Buttons ********/

        //Desc: Round my round button clicked
        private void round_myround3_Click(object sender, RoutedEventArgs e)
        {
              Button[] dark = { round_addround3_btn, round_editround3_btn };
             setSidePanelButtons(dark, round_myround3_btn);
            
        }

        //Desc: Round add round button clicked
        private void round_addround3_Click(object sender, RoutedEventArgs e)
        {

            Button[] dark = { round_myround3_btn, round_editround3_btn };
            setSidePanelButtons(dark, round_addround3_btn);
          
        }

        //Desc: Round edit round button clicked
        private void round_editround3_Click(object sender, RoutedEventArgs e)
        {
            Button[] dark = { round_myround3_btn, round_addround3_btn };
            setSidePanelButtons(dark, round_editround3_btn);
        }

        //Desc: Course my course clicked
        private void course_mycourse3_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] dark = { course_addcourse3_btn, course_editcourse3_btn, course_searchcourse3_btn };
            setSidePanelButtons(dark, course_mycourse3_btn);
        }
        //Desc: Course add course clicked
        private void course_addcourse3_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] dark = { course_mycourse3_btn, course_editcourse3_btn, course_searchcourse3_btn };
            setSidePanelButtons(dark, course_addcourse3_btn);
        }
        //Desc: Course edit course clicked
        private void course_editcourse3_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] dark = { course_addcourse3_btn, course_mycourse3_btn, course_searchcourse3_btn };
            setSidePanelButtons(dark, course_editcourse3_btn);
        }
        //Desc: Course search course clicked
        private void course_searchcourse3_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] dark = { course_addcourse3_btn, course_editcourse3_btn, course_mycourse3_btn };
            setSidePanelButtons(dark, course_searchcourse3_btn);
        }



        /***********************************************/

        /************* Hide/Open Button and Fuctions ********/

        //Desc: Hide Side Pandel Button Functions, Closes side panel, Makes Open Side panel visible, Makes hide side panel not visible
        private void hidesidepanel_btn_Click(object sender, RoutedEventArgs e)
        {
            //All side panel buttons
            Button[] btns = { round_myround3_btn, round_addround3_btn, round_editround3_btn, course_mycourse3_btn, course_addcourse3_btn, course_editcourse3_btn, course_searchcourse3_btn };
            setSidePanelButtonsNotVisible(btns);
            hidensidepanel_r.Visibility = Visibility.Visible;
            hidesidepanel_btn.Visibility = Visibility.Hidden;
            opensidepanel_btn.Visibility = Visibility.Visible;
            //Thickness m = pageload3_f.Margin;
            //m.Left = 10;
            //pageload3_f.Margin = m;

        }

        //Desc: Open Side Panel button Fuctions, Opens last open side panel buttons, Makes Open side panel not visible, makes hide side panel visible
        private void opensidepanel_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] round_btns = { round_myround3_btn, round_addround3_btn, round_editround3_btn }; //round buttons
            Button[] course_btns = { course_mycourse3_btn, course_addcourse3_btn, course_editcourse3_btn, course_searchcourse3_btn }; //course buttons
            hidensidepanel_r.Visibility = Visibility.Hidden;
            //Thickness m = pageload3_f.Margin;
            //m.Left = 10;
            //pageload3_f.Margin = m;
            if (sidepanelclosedon == "round")
            {
                setSidePanelButtonsVisible(round_btns);
            }
            else if (sidepanelclosedon == "course")
            {
                setSidePanelButtonsVisible(course_btns);
            }
            else
            {
                opensidepanel_btn.Visibility = Visibility.Hidden;
            }
            opensidepanel_btn.Visibility = Visibility.Hidden;
            hidesidepanel_btn.Visibility = Visibility.Visible;

        }

     

        /**********************************************/

    }
}
