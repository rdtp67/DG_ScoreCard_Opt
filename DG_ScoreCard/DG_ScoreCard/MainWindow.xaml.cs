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
using DG_ScoreCard.DGserviceReference;



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
        private string currentbutton = "none";
        private int user_id;
        DGserviceClient client = new DGserviceClient();


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
            user_id = client.getUserID(username);
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
        private Brush getindowButtonLightColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFD8D8D8");
            return brush;
        }
        //Get Round top panel solid color
        private Brush getRoundTopPanelSolidColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FF7A00B4");
            return brush;
        }
        //Get Course top panel solid color
        private Brush getCourseTopPanelSolidColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FF007836");
            return brush;
        }
        //Get Disc top panel solid color
        private Brush getDiscTopPanelSolidColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFCB0000");
            return brush;
        }
        //Get Stats top panel solid color
        private Brush getStatsTopPanelSolidColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FF005687");
            return brush;
        }

        //Desc: Sets side panel buttons to match top panel color
        //Pre: array of buttons
        private void setSidePaneltoTopPanelColor(Button[] btn)
        {
            for(int i = 0; i<btn.Count(); i++)
            {
                if(sidepanelclosedon == "round")
                {
                    btn[i].Background = getRoundTopPanelSolidColor();
                }
                else if(sidepanelclosedon == "course")
                {
                    btn[i].Background = getCourseTopPanelSolidColor();
                }
                else if(sidepanelclosedon == "disc")
                {
                    btn[i].Background = getDiscTopPanelSolidColor();
                }
                else if(sidepanelclosedon == "stats")
                {
                    btn[i].Background = getStatsTopPanelSolidColor();
                }
                else
                {
                    return;
                }
            }
        }

        //Desc: Gets Current Top Panel Button Solid Color
        private Brush getCurrentTopPanelButtonSolidColor()
        {
            if (sidepanelclosedon == "round")
            {
               return getRoundTopPanelSolidColor();
            }
            else if (sidepanelclosedon == "course")
            {
                return getCourseTopPanelSolidColor();
            }
            else if (sidepanelclosedon == "disc")
            {
                return getDiscTopPanelSolidColor();
            }
            else if (sidepanelclosedon == "stats")
            {
               return getStatsTopPanelSolidColor();
            }
            else
            {
                return Brushes.Black;
            }
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

        //Desc: sets non clicked side panel buttons dark, sets pushed panel button light, sets page color to light, sets foregrounds
        //Pre: array of buttons, button
        private void setSidePanelButtons(Button[] darkBtns, Button lightBtn)
        {
            //sets pages to light backgroud color
            pageload3_f.Background = getCurrentTopPanelButtonSolidColor();
            for (int i = 0; i < darkBtns.Count(); i++)
            {
                darkBtns[i].Background = getWindowButtonDarkColor();
                darkBtns[i].Foreground = Brushes.Black;
            }
            lightBtn.Background = getCurrentTopPanelButtonSolidColor();
            lightBtn.Foreground = Brushes.White;
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
        //Descs: sets forground black
        //Pre: array of buttons
        private void setSidePanelButtonForegroundBlack(Button[] black)
        {
            for(int i = 0; i<black.Count(); i++)
            {
                black[i].Foreground = Brushes.Black;
            }
        }

        //Desc: Sets all automatic objects after top panel button is pressed
        private void setTopPanelObjects()
        {
            Button[] round_btns = { round_addround3_btn, round_myround3_btn, round_editround3_btn };
            Button[] course_btns = { course_mycourse3_btn, course_addcourse3_btn, course_editcourse3_btn, course_searchcourse3_btn };
            Button[] disc_btns = { disc_mydiscs3_btn, disc_adddisc3_btn, disc_searchdisc3_btn };
            Button[] stats_btns = { stats_mystats3_btn, stats_searchstats3_btn };
            hidesidepanel_btn.Visibility = Visibility.Visible;
            hidensidepanel_r.Visibility = Visibility.Hidden;
            opensidepanel_btn.Visibility = Visibility.Hidden;
            pageload3_f.Background = null;
            if(sidepanelclosedon != "round")
            {
                setSidePanelButtonsNotVisible(round_btns);
                setSidePanelButtonsGenericColor(round_btns);
                setSidePanelButtonForegroundBlack(round_btns);
                
            }
            if (sidepanelclosedon != "course")
            {
                setSidePanelButtonsNotVisible(course_btns);
                setSidePanelButtonsGenericColor(course_btns);
                setSidePanelButtonForegroundBlack(course_btns);
            }
            if(sidepanelclosedon != "disc")
            {
                setSidePanelButtonsNotVisible(disc_btns);
                setSidePanelButtonsGenericColor(disc_btns);
                setSidePanelButtonForegroundBlack(disc_btns);
            }
            if(sidepanelclosedon != "stats")
            {
                setSidePanelButtonsNotVisible(stats_btns);
                setSidePanelButtonsGenericColor(stats_btns);
                setSidePanelButtonForegroundBlack(stats_btns);
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
        //Desc: Top Panel course button clicked
        private void course3_btn_Click(object sender, RoutedEventArgs e)
        {
            sidepanelclosedon = "course";
            Button[] course_btns = { course_mycourse3_btn, course_addcourse3_btn, course_editcourse3_btn, course_searchcourse3_btn };
            setSidePanelButtonsVisible(course_btns);
            setTopPanelObjects();
           
        }
        //Desc: Top Panel disc button clicked
        private void disc3_btn_Click(object sender, RoutedEventArgs e)
        {
            sidepanelclosedon = "disc";
            Button[] disc_btns = { disc_mydiscs3_btn, disc_adddisc3_btn, disc_searchdisc3_btn };
            setSidePanelButtonsVisible(disc_btns);
            setTopPanelObjects();
        }
        //Desc: Top Panel stats button clicked
        private void stats3_btn_Click(object sender, RoutedEventArgs e)
        {
            sidepanelclosedon = "stats";
            Button[] stats_btns = { stats_mystats3_btn, stats_searchstats3_btn };
            setSidePanelButtonsVisible(stats_btns);
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
            if(currentbutton != "mycourse")
            {
                Button[] dark = { course_addcourse3_btn, course_editcourse3_btn, course_searchcourse3_btn };
                setSidePanelButtons(dark, course_mycourse3_btn);
                ViewCourse view = new ViewCourse(user_id);//need overload for username
                pageload3_f.NavigationService.Navigate(view);
                currentbutton = "mycourse";
            }
            
        }
        //Desc: Course add course clicked
        private void course_addcourse3_btn_Click(object sender, RoutedEventArgs e)
        {
            if(currentbutton != "addcourse")
            {
                Button[] dark = { course_mycourse3_btn, course_editcourse3_btn, course_searchcourse3_btn };
                setSidePanelButtons(dark, course_addcourse3_btn);
                AddCourse addcourse = new AddCourse(username);
                pageload3_f.NavigationService.Navigate(addcourse);
                currentbutton = "addcourse";
            }
          
        }
        //Desc: Course edit course clicked
        private void course_editcourse3_btn_Click(object sender, RoutedEventArgs e)
        {
            if(currentbutton != "editcourse")
            {
                Button[] dark = { course_addcourse3_btn, course_mycourse3_btn, course_searchcourse3_btn };
                setSidePanelButtons(dark, course_editcourse3_btn);
                EditCourse editcourse = new EditCourse();
                pageload3_f.NavigationService.Navigate(editcourse);
                currentbutton = "editcourse";
            }
            
        }
        //Desc: Course search course clicked
        private void course_searchcourse3_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] dark = { course_addcourse3_btn, course_editcourse3_btn, course_mycourse3_btn };
            setSidePanelButtons(dark, course_searchcourse3_btn);
        }
        //Desc: Disc my disc clicked
        private void disc_mydiscs3_btn_Click(object sender, RoutedEventArgs e)
        {

            if (currentbutton != "disc_view")
            {
                Button[] dark = { disc_adddisc3_btn, disc_searchdisc3_btn };
                setSidePanelButtons(dark, disc_mydiscs3_btn);
                Disc_view da = new Disc_view(username);
                pageload3_f.NavigationService.Navigate(da);
                currentbutton = "disc_view";
            }
        }
        //Desc: Disc add disc clicked
        private void disc_adddisc3_btn_Click(object sender, RoutedEventArgs e)
        {
            if (currentbutton != "disc_add")
            {
                Button[] dark = { disc_mydiscs3_btn, disc_searchdisc3_btn };
                setSidePanelButtons(dark, disc_adddisc3_btn);
                Disc_add da = new Disc_add(username);
                pageload3_f.NavigationService.Navigate(da);
                currentbutton = "disc_add";
            }
        }
        //Desc: Disc search disc clicked
        private void disc_searchdisc3_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] dark = { disc_mydiscs3_btn, disc_adddisc3_btn };
            setSidePanelButtons(dark, disc_searchdisc3_btn);
        }
        //Desc: Stats my stats clicked
        private void stats_mystats3_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] dark = { stats_searchstats3_btn };
            setSidePanelButtons(dark, stats_mystats3_btn);
        }
        //Desc: Stats search stats clicked
        private void stats_searchstats3_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] dark = { stats_mystats3_btn };
            setSidePanelButtons(dark, stats_searchstats3_btn);
        }

        /***********************************************/



        /************* Hide/Open Button and Fuctions ********/

        //Desc: Hide Side Pandel Button Functions, Closes side panel, Makes Open Side panel visible, Makes hide side panel not visible
        private void hidesidepanel_btn_Click(object sender, RoutedEventArgs e)
        {
            //All side panel buttons
            Button[] btns = { round_myround3_btn, round_addround3_btn, round_editround3_btn, course_mycourse3_btn, course_addcourse3_btn, course_editcourse3_btn, course_searchcourse3_btn, disc_mydiscs3_btn, disc_adddisc3_btn, disc_searchdisc3_btn, stats_mystats3_btn, stats_searchstats3_btn };
            setSidePanelButtonsNotVisible(btns);
            hidensidepanel_r.Visibility = Visibility.Visible;
            hidesidepanel_btn.Visibility = Visibility.Hidden;
            opensidepanel_btn.Visibility = Visibility.Visible;
            Thickness m = pagegrid.Margin;
            m.Left = 40;
            pagegrid.Margin = m;

        }

        //Desc: Open Side Panel button Fuctions, Opens last open side panel buttons, Makes Open side panel not visible, makes hide side panel visible
        private void opensidepanel_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] round_btns = { round_myround3_btn, round_addround3_btn, round_editround3_btn }; //round buttons
            Button[] course_btns = { course_mycourse3_btn, course_addcourse3_btn, course_editcourse3_btn, course_searchcourse3_btn }; //course buttons
            Button[] disc_btns = { disc_mydiscs3_btn, disc_adddisc3_btn, disc_searchdisc3_btn }; //disc buttons
            Button[] stats_btns = { stats_mystats3_btn, stats_searchstats3_btn }; //stats buttons
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
            else if (sidepanelclosedon == "disc")
            {
                setSidePanelButtonsVisible(disc_btns);
            }
            else if (sidepanelclosedon == "stats")
            {
                setSidePanelButtonsVisible(stats_btns);
            }
            else
            {
                opensidepanel_btn.Visibility = Visibility.Hidden;
            }
            opensidepanel_btn.Visibility = Visibility.Hidden;
            hidesidepanel_btn.Visibility = Visibility.Visible;

            Thickness m = pagegrid.Margin;
            m.Left = 208.5;
            pagegrid.Margin = m;


        }


        /**********************************************/

        /************** Settings Buttons Fuctions *****/
        private void usersettings3_btn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void settings3_mi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void signout3_mi_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Login loginWindow = new Login(this);
            loginWindow.Show();
            this.Close();
        }
        /***************************************************/
    }
}
