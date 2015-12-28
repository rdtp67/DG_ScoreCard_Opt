using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;


namespace WCFwebserviceDG
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DGservice" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DGservice.svc or DGservice.svc.cs at the Solution Explorer and start debugging.
    public class DGservice : IDGservice
    {
        const string myConnection = "server=198.71.227.92;username=rdtp67;password=sT7uq0^6;database=rdtp67_DG_Scorecard_PROD";
        MySqlConnection myConn = new MySqlConnection(myConnection);

        public string Message()
        {
           
            string name = "name";
            
            try
            {
                myConn.Open();
                string query = "Select user_id from user where user_name = 'God'";
                MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, myConn);
               
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    name = reader.GetString(reader.GetOrdinal("user_id"));
                }
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }
            return name;
        }
    }
}
