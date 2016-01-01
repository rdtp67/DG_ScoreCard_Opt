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

        private Brush getWindowButtonColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFD8D8D8");
            return brush;
        }
        private Brush getWindowButtonGenericColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFC0C0C0");
            return brush;
        }

        private void button3_btn_Click(object sender, RoutedEventArgs e)
        {
            
            pageload3_f.Background = getWindowButtonColor();
            button3_btn.Background = getWindowButtonColor();
            button23_btn.Background = getWindowButtonGenericColor();
        }

        private void button23_btn_Click(object sender, RoutedEventArgs e)
        {
            
            pageload3_f.Background = getWindowButtonColor();
            button23_btn.Background = getWindowButtonColor();
            button3_btn.Background = getWindowButtonGenericColor();
        }
    }
}
