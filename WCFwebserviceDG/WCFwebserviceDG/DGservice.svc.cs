﻿using System;
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
        //const string myConnection = System.Configuration.ConfigurationManager.ConnectionStrings["cstring"].ConnectionString;
        //MySqlConnection myconn = new MySqlConnection(myConnection);

        const string myConnection = "server=198.71.227.92; username=rdtp67; password=sT7uq0^6; database=rdtp67_DG_Scorecard_PROD";
        MySqlConnection myConn = new MySqlConnection(myConnection);

        //Desc: Gets ID of one user
        //Pre: String
        //Post: String
        public string getUserID(string id)
        {
           
            string name = "name";
            
            try
            {
                myConn.Open();
                string query = "Select user_id from user where user_name = @username";
                MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, myConn);
                cmd.Parameters.AddWithValue("@username", id);
               
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

        //Desc: Inserts new location
        //Pre: Strings for location parameters
        public void insertLocation(string loc_address, string loc_state, string loc_city, string loc_country, string loc_zip)
        {
            try
            {
                myConn.Open();
                MySqlCommand com = new MySqlCommand();
                com.Connection = myConn;
                com.CommandText = "INSERT INTO location(loc_address, loc_state, loc_city, loc_country, loc_zip) VALUES(@loc_address, @loc_state, @loc_city, @loc_country, @loc_zip)";
                com.Prepare();
                com.Parameters.AddWithValue("@loc_address", loc_address);
                com.Parameters.AddWithValue("@loc_state", loc_state);
                com.Parameters.AddWithValue("@loc_city", loc_city);
                com.Parameters.AddWithValue("@loc_country", loc_country);
                com.Parameters.AddWithValue("@loc_zip", loc_zip);
                com.ExecuteNonQuery();

            }
            catch
            {
               
            }
            finally
            {
                myConn.Close();
            }  

        }

        //Desc: Inserts new user
        //Pre: Strings for user parameters, Strings for location parameters
        public void insertUser(string username, string fname, string lname, string email, string phone, string shash, string loc_address, string loc_state, string loc_city, string loc_country, string loc_zip)
        {
            //Loads new user
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "INSERT INTO user(user_name, loc_id, user_slowhashsalt, user_fname, user_lname, user_email, user_phone) VALUES(@user_name, (Select l.loc_id " +
                                                                                                                                                            "from location l " +
                                                                                                                                                           "where l.loc_address = '" + loc_address + "' and l.loc_state = '" + loc_state + "' and l.loc_city = '" + loc_city + "' and l.loc_country = '" + loc_country + "' and l.loc_zip = '" + loc_zip + "' " +
                                                                                                                                                            " ), @slowhash, @fname, @lname, @email, @phone)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@user_name", username);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@slowhash", shash);



                cmd.ExecuteNonQuery();

                
            }
            catch
            { 
            }
            finally
            {
                myConn.Close();
            }
           

        }
        
        //Desc: Checks if username is taken
        //Pre: string
        //Post: string
        public string checkUsername(string username)
        {
            String check = "1";

            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand("Select count(u.user_name) as 'Count' " +
                                                    "from user u " +
                                                    "where u.user_name = '" + username + "' ", myConn); //Does not check for active incase account gets re-activated
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    check = rdr["Count"].ToString();
                }
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return check;
        }

        //Desc: Checks if location is taken, returns true if it is taken
        //Pre: Strings for location parameters
        //Post: bool
        public bool checkLocation(string address, string state, string city, string country, string zip)
        {
            string loc = "";
            bool check = true;
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand("Select count(l.loc_id) as 'Count' from location l where l.loc_address = '" + address + "' and l.loc_state = '" + state + "' and l.loc_city = '" + city + "' and l.loc_country = '" + country + "' and l.loc_zip = '" + zip + "' ", myConn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    loc = rdr["Count"].ToString();
                }
                if (loc == "0")
                {
                    check = false;
                }
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return check;
        }

        //Desc: Returns lists for cstrings
        //Pre: string username
        //Post: cstring lists
        public List<login> returnCstringLists(string username)
        {


            List<login> cstring = new List<login>();

            try
            {
                myConn.Open();
                string query = "Select user_active, user_name, user_slowhashsalt from user where user_name = @username";
                MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, myConn);
                cmd.Parameters.AddWithValue("@username", username);
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.HasRows && reader.Read())
                {
                    login data = new login();
                    data.user_active = reader.GetChar(reader.GetOrdinal("user_active"));
                    data.user_cstring = reader.GetString(reader.GetOrdinal("user_slowhashsalt"));
                    data.user_username = reader.GetString(reader.GetOrdinal("user_name"));
                    cstring.Add(data);
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


            return cstring.ToList();
        }

        //Desc: Inserts Course
        public void insertCourse(string name, string website, string phone, string basket_type, string year_established, string tee_type, string course_type, string terrain, string basket_maker, char? course_private, char? p2p, char? guide, string course_designer, string user, string address, string state, string city, string country, string zip)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "INSERT into course(user_id, loc_id, course_name, course_website, course_phone_number, course_basket_type, course_year_established, course_tee_type, course_type, course_terrain, course_basket_manufacturer, course_private, course_pay, course_has_guides, course_designer) " +
                    "VALUES((Select user_id from user where user_name = @user ), " +
                    " (Select loc_id from location where loc_address = @address and loc_state = @state and loc_city = @city and loc_country = @country and loc_zip = @zip), " + 
                    " @name, @website, @phone, @basket_type, @year_established, @tee_type, @course_type, @terrain, @basket_maker, @course_private, @p2p, @guide, @course_designer)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@website", website);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@basket_type", basket_type);
                cmd.Parameters.AddWithValue("@year_established", year_established);
                cmd.Parameters.AddWithValue("@tee_type", tee_type);
                cmd.Parameters.AddWithValue("@course_type", course_type);
                cmd.Parameters.AddWithValue("@terrain", terrain);
                cmd.Parameters.AddWithValue("@basket_maker", basket_maker);
                cmd.Parameters.AddWithValue("@course_private", course_private);
                cmd.Parameters.AddWithValue("@p2p", p2p);
                cmd.Parameters.AddWithValue("@guide", guide);
                cmd.Parameters.AddWithValue("@course_designer", course_designer);

                cmd.ExecuteNonQuery();

            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }
        }

        //Desc: Inserts Park
        public void insertPark(string name, string hour_h, string hour_l, char? guide, char? pet, char? pri, string user_name, string course_name)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Insert into park(course_id, park_name, park_private, park_hours_high, park_hours_low, park_has_guides, park_pet_friendly) " +
                    "Values( (select c.course_id from course c join user u on u.user_id = c.user_id where u.user_name = @user_name and c.course_name = @course_name), @name, @pri, @hour_h, @hour_l, @guide, @pet) ";
                cmd.Parameters.AddWithValue("@user_name", user_name);
                cmd.Parameters.AddWithValue("@course_name", course_name);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@pri", pri);
                cmd.Parameters.AddWithValue("@hour_h", hour_h);
                cmd.Parameters.AddWithValue("@hour_l", hour_l);
                cmd.Parameters.AddWithValue("@guide", guide);
                cmd.Parameters.AddWithValue("@pet", pet);

                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }
        }


    }
}
