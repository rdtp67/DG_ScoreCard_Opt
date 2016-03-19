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
    /// Interaction logic for ViewCourse.xaml
    /// </summary>
    public partial class ViewCourse : Page
    {
        DGserviceClient client = new DGserviceClient();
        private int user_id;
        List<courselist> c_list = new List<courselist>();

        public ViewCourse()
        {
            InitializeComponent();
        }

        public ViewCourse(int u_id)
        {
            InitializeComponent();
            user_id = u_id;
            c_list = client.getMyCourseList(user_id);
        }

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
    }
}
