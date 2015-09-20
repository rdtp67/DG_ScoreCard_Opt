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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void dclick_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            test_data.Text = "help";
        }

        private void btn_closeClick(object sender, RoutedEventArgs e)
        {
           // this.Close(); //closes current window
            App.Current.Shutdown(); //Shutsdown Application
        }

   
    }
}
