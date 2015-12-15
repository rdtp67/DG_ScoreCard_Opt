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
using System.Windows.Shapes;

namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private Login mainWindow;

        public SignUp(Login mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void movebar_Rec_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void backarrow_BA_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Login LoginWin = new Login(this);
            LoginWin.Show();
            this.Close();
        }
    }
}
