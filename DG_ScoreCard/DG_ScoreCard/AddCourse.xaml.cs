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
        public AddCourse()
        {
            InitializeComponent();
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
            //Check for main fields entered

            //Verify field lengths


        }
    }
}
