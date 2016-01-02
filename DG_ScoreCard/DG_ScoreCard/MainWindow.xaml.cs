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

        /**************************************/

        /********** TOP Panel Clicked Buttons **********/

        //Desc: Top Panel round button clicked
        private void round3_btn_Click(object sender, RoutedEventArgs e)
        {
            Button[] panel_btns = { round_addround3_btn, round_myround3_btn, round_editround3_btn };
            setSidePanelButtonsVisible(panel_btns);
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

        private void round_editround3_Click(object sender, RoutedEventArgs e)
        {
            Button[] dark = { round_myround3_btn, round_addround3_btn };
            setSidePanelButtons(dark, round_editround3_btn);
        }

        /***********************************************/

    }
}
