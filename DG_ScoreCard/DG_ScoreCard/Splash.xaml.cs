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
using MySql.Data.MySqlClient;

namespace DG_ScoreCard
{
 
    public partial class Window1 : Window
    {
        /*****************************************************************************************************************************************************/
        /**************************                                          Variables                                              **************************/
        /*****************************************************************************************************************************************************/

        const string myConnection = "datasource=localhost;port=3306;username=root;password=root; database=discgolf";
        MySqlConnection myConn = new MySqlConnection(myConnection);

        public Window1()
        {
            InitializeComponent();
          
        }
    }
}
