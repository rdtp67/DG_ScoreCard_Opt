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
        //const string myConnection = System.Configuration.ConfigurationManager.ConnectionStrings["cstring"].ConnectionString;
        //MySqlConnection myconn = new MySqlConnection(myConnection);

        const string myConnection = "server=198.71.227.92; username=rdtp67; password=sT7uq0^6; database=rdtp67_DG_Scorecard_PROD";
        MySqlConnection myConn = new MySqlConnection(myConnection);

        //Desc: Gets ID of one user
        //Pre: String
        //Post: String
        public int getUserID(string id)
        {
           
            int name = 0;
            
            try
            {
                myConn.Open();
                string query = "Select user_id from user where user_name = @username";
                MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, myConn);
                cmd.Parameters.AddWithValue("@username", id);
               
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    name = reader.GetInt32(reader.GetOrdinal("user_id"));
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
                if (checkLocation(loc_address, loc_state, loc_city, loc_country, loc_zip) == false)
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

            }
            catch
            {
               
            }
            finally
            {
                myConn.Close();
            }  

        }

        //Desc: Gets Location ID
        public int getLocID(string loc_address, string loc_state, string loc_city, string loc_country, string loc_zip)
        {
            int loc_id = 666;

            try
            {
                myConn.Open();
                MySqlCommand com = new MySqlCommand();
                com.Connection = myConn;
                com.CommandText = "Select loc_id From location Where loc_address = @loc_address and loc_state = @loc_state and loc_city = @loc_city and loc_country = @loc_country and loc_zip = @loc_zip";
                com.Parameters.AddWithValue("@loc_address", loc_address);
                com.Parameters.AddWithValue("@loc_state", loc_state);
                com.Parameters.AddWithValue("@loc_city", loc_city);
                com.Parameters.AddWithValue("@loc_country", loc_country);
                com.Parameters.AddWithValue("@loc_zip", loc_zip);

                MySqlDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    loc_id = Convert.ToInt32(read["loc_id"]);
                }
                read.Close();

            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return loc_id;
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
            String check = "0";

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

                rdr.Close();
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

                rdr.Close();
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
        public void insertCourse(string name, string website, string phone, string email, string basket_type, string year_established, string tee_type, string course_type, string terrain, string basket_maker, char? course_private, char? p2p, char? guide, string course_designer, int user_id, int loc_id, int park_id)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "INSERT into course(user_id, loc_id, park_id, course_name, course_website, course_phone_number, course_email, course_basket_type, course_year_established, course_tee_type, course_type, course_terrain, course_basket_manufacturer, course_private, course_pay, course_has_guides, course_designer, upsrt_user_id) " +
                    "VALUES(@user, " +
                    " @loc_id, " + 
                    " @park_id, @name, @website, @phone, @email, @basket_type, @year_established, @tee_type, @course_type, @terrain, @basket_maker, @course_private, @p2p, @guide, @course_designer, @user)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@user", user_id);
                cmd.Parameters.AddWithValue("@park_id", park_id);
                cmd.Parameters.AddWithValue("@loc_id", loc_id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@website", website);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
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

        //Desc: Gets Course ID using Course Design Fields
        public int getCourseID(int user_id, int park_id, int loc_id, string name, string website, string phone, string basket_type, string year_established, string tee_type, string course_type, string terrain, string basket_maker, char? course_private, char? p2p, char? guide, string course_designer)
        {
            int c_id = 666;

            try
            {
                myConn.Open();
                MySqlCommand com = new MySqlCommand();
                com.Connection = myConn;
                com.CommandText = "Select course_id From course Where user_id = @user_id and loc_id = @loc_id and park_id = @park_id and course_name = @cname " +
                    " and course_website = @c_web and course_phone_number = @phone and course_basket_type = @btype and course_year_established = @year_est and " +
                    " course_tee_type = @ttype and course_type = @ctype and course_terrain = @terr and course_basket_manufacturer = @manu and course_private = @pri " +
                    " and course_pay = @pay and course_has_guides = @guide and course_designer = @des";

                com.Parameters.AddWithValue("@user_id", user_id);
                com.Parameters.AddWithValue("@loc_id", loc_id);
                com.Parameters.AddWithValue("@park_id", park_id);
                com.Parameters.AddWithValue("@cname", name);
                com.Parameters.AddWithValue("@c_web", website);
                com.Parameters.AddWithValue("@phone", phone);
                com.Parameters.AddWithValue("@btype", basket_type);
                com.Parameters.AddWithValue("@year_est", year_established);
                com.Parameters.AddWithValue("@ttype", tee_type);
                com.Parameters.AddWithValue("@ctype", course_type);
                com.Parameters.AddWithValue("@terr", terrain);
                com.Parameters.AddWithValue("@manu", basket_maker);
                com.Parameters.AddWithValue("@pri", course_private);
                com.Parameters.AddWithValue("@pay", p2p);
                com.Parameters.AddWithValue("@guide", guide);
                com.Parameters.AddWithValue("@des", course_designer);

                MySqlDataReader read = com.ExecuteReader();
                while (read.Read())
                {
                    c_id = Convert.ToInt32(read["course_id"]);
                }
                read.Close();

            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return c_id;
        }

        //Desc: Gets Course ID using user id and course name
        public int getCourseID2(int usr_id, string course_name)
        {
            int count = 666;

            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select course_id From course Where user_id = @usr_id and course_name = @course_name";
                cmd.Parameters.AddWithValue("@usr_id", usr_id);
                cmd.Parameters.AddWithValue("@course_name", course_name);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    count = int.Parse((rdr["course_id"]).ToString());
                }

                rdr.Close();
            }
            catch
            {

            }
            finally
            {

            }

            return count;
        }

        public bool checkCourseUserExists(int usr_id, string course_name)
        {
            int count = -1;

            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select count(course_id) AS Count From course Where user_id = @usr_id and course_name = @course_name";
                cmd.Parameters.AddWithValue("@usr_id", usr_id);
                cmd.Parameters.AddWithValue("@course_name", course_name);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    count = int.Parse((rdr["Count"]).ToString());
                }

                rdr.Close();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            if(count > 0)
            {
                return true;
            }

            return false;
        }

        //Desc: Inserts Park
        public void insertPark(string name, string hour_h, string hour_l, char? guide, char? pet, char? pri)
        {
            
            try
            {
                if (parkExist(name, hour_h, hour_l, guide, pet, pri) == false)
                {
                    
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Insert into park(park_name, park_private, park_hours_high, park_hours_low, park_has_guides, park_pet_friendly) " +
                    "Values(@name, @pri, @hour_h, @hour_l, @guide, @pet) ";
                
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@pri", pri);
                cmd.Parameters.AddWithValue("@hour_h", hour_h);
                cmd.Parameters.AddWithValue("@hour_l", hour_l);
                cmd.Parameters.AddWithValue("@guide", guide);
                cmd.Parameters.AddWithValue("@pet", pet);

                cmd.ExecuteNonQuery();
                }
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }
        }

        public bool parkExist(string name, string hour_h, string hour_l, char? guide, char? pet, char? pri)
        {
            int count = 1;
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select count(park_id) AS Count From park Where park_name = @name and park_private = @pri and park_hours_high = @hour_h and park_hours_low = @hour_l and park_has_guides = @guide and park_pet_friendly = @pet";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@hour_h", hour_h);
                cmd.Parameters.AddWithValue("@hour_l", hour_l);
                cmd.Parameters.AddWithValue("@pri", pri);
                cmd.Parameters.AddWithValue("@guide", guide);
                cmd.Parameters.AddWithValue("@pet", pet);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    count = Convert.ToInt32(read["Count"]);
                }

                read.Close();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();

            }

            if (count > 0)
            {
                return true;
            }
            return false;
        }

        //Desc: Returns park_id
        //Pre: park database parameters
        //Post: int
        public int getParkId(string park_name, char? park_private, string park_hours_high, string park_hours_low, char? park_has_guides, char? park_pet_friendly)
        {
            int park_id = 666;

            try
            {
                myConn.Open();
                MySqlCommand com = new MySqlCommand();
                com.Connection = myConn;
                com.CommandText = "select park_id from park where park_name = @park_name and park_private = @park_private and park_hours_high = @park_hours_high and park_hours_low = @park_hours_low and park_has_guides = @park_has_guides and park_pet_friendly = @park_pet_friendly";
                com.Parameters.AddWithValue("@park_name", park_name);
                com.Parameters.AddWithValue("@park_private", park_private);
                com.Parameters.AddWithValue("@park_hours_high", park_hours_high);
                com.Parameters.AddWithValue("@park_hours_low", park_hours_low);
                com.Parameters.AddWithValue("@park_has_guides", park_has_guides);
                com.Parameters.AddWithValue("@park_pet_friendly", park_pet_friendly);

                MySqlDataReader read = com.ExecuteReader();
                while(read.Read())
                {
                    park_id = Convert.ToInt32(read["park_id"]);
                }
                read.Close();
                
            }
            catch
            {
                
            }
            finally
            {
                myConn.Close();
            }

            return park_id;
        }

        //DESC: needs work...
        public holeLib getHole() { holeLib hole = new holeLib(); return hole; }   

        //Desc: Submits Course to DB
        public void insertHole(holeLib h, int course_id, int tee, int basket, int misc, int line)
        {
            try
            {
                //Get Course id
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "INSERT INTO hole(course_id, tee_id, basket_id, misc_id, hole_lines_id, hole_num, hole_yardage, hole_par, hole_unit, hole_name, hole_mando, hole_hazards) " + 
                                             " VALUES(@course, @tee, @basket, @misc, @line, @num, @yard, @par, @unit, @name, @mando, @haz)";
                cmd.Parameters.AddWithValue("@course", course_id);
                cmd.Parameters.AddWithValue("@tee", tee);
                cmd.Parameters.AddWithValue("@basket", basket);
                cmd.Parameters.AddWithValue("@misc", misc);
                cmd.Parameters.AddWithValue("@line", line);
                cmd.Parameters.AddWithValue("@num", h.h_num);
                cmd.Parameters.AddWithValue("@yard", h.h_yardage);
                cmd.Parameters.AddWithValue("@par", h.h_par);
                cmd.Parameters.AddWithValue("@unit", h.h_unit);
                cmd.Parameters.AddWithValue("@name", h.h_name);
                cmd.Parameters.AddWithValue("@mando", h.h_mando);
                cmd.Parameters.AddWithValue("@haz", h.h_hazzards);
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

        //Desc: Submits Count to BD based on dynamic SQL
        public void insertHoleInput(string holeinput)
        {
            try
            {
                //Get Course id
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "INSERT INTO hole(course_id, tee_id, basket_id, misc_id, hole_lines_id, hole_num, hole_yardage, hole_par, hole_unit, hole_name, hole_mando, hole_hazards) " +
                                             " VALUES "  + holeinput;
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

        //Desc: Inserts one basket into basket table
        public void insertBasket(holeLib h)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Insert Into basket(basket_letter, basket_deduction, basket_note) values(@letter, @deduction, @note)";
                cmd.Parameters.AddWithValue("@letter", h.b_letter);
                cmd.Parameters.AddWithValue("@deduction", h.b_deduction);
                cmd.Parameters.AddWithValue("@note", h.b_note);

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

        public void insertBasketInput(string basketinput)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Insert Into basket(basket_letter, basket_deduction, basket_note) values" + basketinput;
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

        //Desc: Checks if basket exists
        //Post: Returns true if basket exists
        public bool basketExists(holeLib h)
        {
            int count = -1;
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select count(basket_id) AS Count From basket where basket_letter = @letter and basket_deduction = @de and basket_note = @note";
                cmd.Parameters.AddWithValue("@letter", h.b_letter);
                cmd.Parameters.AddWithValue("@de", h.b_deduction);
                cmd.Parameters.AddWithValue("@note", h.b_note);
                MySqlDataReader read = cmd.ExecuteReader();
                while(read.Read())
                {
                    count = Convert.ToInt32(read["Count"]);
                }

                read.Close();
            }
            catch
            {
               
            }
            finally
            {
                myConn.Close();

            }

             if(count > 0)
            {
                return true;
            }
            return false;
        }

        //Desc: Checks if Basket ID exists
        //Post: Returns Basket ID
        public int getBasketID(holeLib h)
        {
            int count = 666;
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select basket_id from basket where basket_letter = @letter and basket_deduction = @de and basket_note = @note";
                cmd.Parameters.AddWithValue("@letter", h.b_letter);
                cmd.Parameters.AddWithValue("@de", h.b_deduction);
                cmd.Parameters.AddWithValue("@note", h.b_note);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    count = int.Parse((rdr["basket_id"]).ToString());
                }
                rdr.Close();

            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return count;
        }

        public void insertTee(holeLib h)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Insert Into tee(tee_color, tee_pad_type, tee_notes) Values(@color, @type, @notes)";
                cmd.Parameters.AddWithValue("@color", h.t_color);
                cmd.Parameters.AddWithValue("@type", h.t_pad_type);
                cmd.Parameters.AddWithValue("@notes", h.t_notes);

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

        public void insertTeeInput(string teeinput)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Insert Into tee(tee_color, tee_pad_type, tee_notes) Values" + teeinput;
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

        public bool teeExists(holeLib h)
        {
            int count = -1;
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select count(tee_id) AS Count from tee where tee_color = @color and tee_pad_type = @type and tee_notes = @notes";
                cmd.Parameters.AddWithValue("@color", h.t_color);
                cmd.Parameters.AddWithValue("@type", h.t_pad_type);
                cmd.Parameters.AddWithValue("@notes", h.t_notes);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    count = Convert.ToInt32(read["Count"]);
                }

                read.Close();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();

            }

            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public int getTeeID(holeLib h)
        {
            int count = 666;
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select tee_id From tee Where tee_color = @color and tee_pad_type = @pad and tee_notes = @notes";
                cmd.Parameters.AddWithValue("@color", h.t_color);
                cmd.Parameters.AddWithValue("@pad", h.t_pad_type);
                cmd.Parameters.AddWithValue("@notes", h.t_notes);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    count = int.Parse((rdr["tee_id"]).ToString());
                }

                rdr.Close();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return count;
        }

        public void insertMisc(holeLib h)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Insert Into misc(misc_guidetohole, misc_trashcan, misc_trailsnearby, misc_roadsnearby, misc_generalcomments) Values(@guide, @trash, @trail, @road, @comment)";
                cmd.Parameters.AddWithValue("@guide", h.m_guide);
                cmd.Parameters.AddWithValue("@trash", h.m_trash);
                cmd.Parameters.AddWithValue("@trail", h.m_trail);
                cmd.Parameters.AddWithValue("@road", h.m_road);
                cmd.Parameters.AddWithValue("@comment", h.m_general_comments);

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

        public void insertMiscInput(string miscinput)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Insert Into misc(misc_guidetohole, misc_trashcan, misc_trailsnearby, misc_roadsnearby, misc_generalcomments) Values" + miscinput;
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

        public bool miscExists(holeLib h)
        {
            int count = -1;
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select count(misc_id) as Count From misc Where misc_guidetohole = @guide and misc_trashcan = @trash and misc_trailsnearby = @trail and misc_roadsnearby = @road and misc_generalcomments = @comment";
                cmd.Parameters.AddWithValue("@guide", h.m_guide);
                cmd.Parameters.AddWithValue("@trash", h.m_trash);
                cmd.Parameters.AddWithValue("@trail", h.m_trail);
                cmd.Parameters.AddWithValue("@road", h.m_road);
                cmd.Parameters.AddWithValue("@comment", h.m_general_comments);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    count = Convert.ToInt32(read["Count"]);
                }

                read.Close();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();

            }

            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public int getMiscID(holeLib h)
        {
            int count = 666;
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select misc_id From misc Where misc_guidetohole = @guide and misc_trashcan = @trash and misc_trailsnearby = @trails and misc_roadsnearby = @road and misc_generalcomments = @com";
                cmd.Parameters.AddWithValue("@guide", h.m_guide);
                cmd.Parameters.AddWithValue("@trash", h.m_trash);
                cmd.Parameters.AddWithValue("@trails", h.m_trail);
                cmd.Parameters.AddWithValue("@road", h.m_road);
                cmd.Parameters.AddWithValue("@com", h.m_general_comments);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    count = int.Parse((rdr["misc_id"]).ToString());
                }

                rdr.Close();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return count;
        }

        public void insertHoleLines(holeLib h)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Insert Into hole_lines(hole_lines_rec_shot, hole_lines_rec_disc) Values(@shot, @disc)";
                cmd.Parameters.AddWithValue("@shot", h.r_shots);
                cmd.Parameters.AddWithValue("@disc", h.r_disc);

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

        public void insertHoleLinesInput(string holelinesinput)
        {
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Insert Into hole_lines(hole_lines_rec_shot, hole_lines_rec_disc) Values" + holelinesinput;
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

        public bool holelinesExists(holeLib h)
        {
            int count = -1;
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select count(hole_lines_id) AS Count From hole_lines Where hole_lines_rec_shot = @shot and hole_lines_rec_disc = @disc";
                cmd.Parameters.AddWithValue("@shot", h.r_shots);
                cmd.Parameters.AddWithValue("@disc", h.r_disc);
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    count = Convert.ToInt32(read["Count"]);
                }

                read.Close();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();

            }

            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public int getHoleLinesID(holeLib h)
        {
            int count = 111;
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select hole_lines_id From hole_lines Where hole_lines_rec_shot = @rec and hole_lines_rec_disc = @disc";
                cmd.Parameters.AddWithValue("@rec", h.r_shots);
                cmd.Parameters.AddWithValue("@disc", h.r_disc);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    count = int.Parse((rdr["hole_lines_id"]).ToString());
                }

                rdr.Close();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return count;
        }

        //Desc: Returns course info
        //Pre: int user id
        //Post: Course name, id
        public List<courselist> getMyCourseList(int user_id)
        {
            List<courselist> c = new List<courselist>();

            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select c.course_id AS c_id, c.course_name AS c_name From course c Join user u on u.user_id = c.user_id Where u.user_id = @user_id Order By c.course_name";
                cmd.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.HasRows && rdr.Read())
                {
                    courselist l = new courselist();
                    l.c_id = int.Parse((rdr["c_id"]).ToString());
                    l.c_name = rdr["c_name"].ToString();
                    c.Add(l);
                }
                rdr.Close();
                
            }
            catch
            {
                
            }
            finally
            {
                myConn.Close();
            }


            return c.ToList();
        }

        //Desc: Returns back location details for park
        //Pre: int course id
        //Post: Park address, city, state, country, zip
        public location getParkLoc(int c_id)
        {
            location loc = new location();
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select l.loc_address as addrs, l.loc_state as st, l.loc_city as city, l.loc_country as co, l.loc_zip as zip " +
                    " From course c Join location l on l.loc_id = c.loc_id Where c.course_id = @c_id";
                cmd.Parameters.AddWithValue("@c_id", c_id);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.HasRows && rdr.Read())
                {
                    loc.l_address = rdr["addrs"].ToString();
                    loc.l_state = rdr["st"].ToString();
                    loc.l_city = rdr["city"].ToString();
                    loc.l_country = rdr["co"].ToString();
                    loc.l_zip = rdr["zip"].ToString();
                }
                rdr.Close();

            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return loc;
        }

        //Desc: Returns course values
        //Pre: course_id, user_id
        //Post: Course
        public course getCourse(int course_id, int user_id)
        {
            course c = new course();

            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select *" +
                    " From course Where course_id = @course_id and user_id = @user_id";
                cmd.Parameters.AddWithValue("@course_id", course_id);
                cmd.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.HasRows && rdr.Read())
                {
                    c.c_name = rdr["course_name"].ToString();
                    c.c_website = rdr["course_website"].ToString();
                    c.c_phone = rdr["course_phone_number"].ToString();
                    c.c_email = rdr["course_email"].ToString();
                    c.c_basket_type = rdr["course_basket_type"].ToString();
                    c.c_year_est = rdr["course_year_established"].ToString();
                    c.c_tee_type = rdr["course_tee_type"].ToString();
                    c.c_type = rdr["course_type"].ToString();
                    c.c_terrain = rdr["course_terrain"].ToString();
                    c.c_basket_manu = rdr["course_basket_manufacturer"].ToString();
                    c.c_pri = (rdr["course_private"]).ToString();
                    c.c_pay = rdr["course_pay"].ToString();
                    c.c_has_guide = (rdr["course_has_guides"]).ToString();
                    c.c_design = rdr["course_designer"].ToString();
                }

                rdr.Close();
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return c;
        }

        public park getPark(int course_id, int user_id)
        {
            park p = new park();
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select p.park_name name, p.park_private pri, p.park_hours_high hi, p.park_hours_low lo, p.park_has_guides gui, p.park_pet_friendly pet From park p Join course c on c.park_id = p.park_id Where course_id = @cid and c.user_id = @uid";
                cmd.Parameters.AddWithValue("@cid", course_id);
                cmd.Parameters.AddWithValue("@uid", user_id);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read() && rdr.HasRows)
                {
                    p.p_name = rdr["name"].ToString();
                    p.p_private = rdr["pri"].ToString();
                    p.p_hours_high = rdr["hi"].ToString();
                    p.p_hours_low = rdr["lo"].ToString();
                    p.p_has_guides = rdr["gui"].ToString();
                    p.p_pet_friendly = rdr["pet"].ToString();
                }
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return p;
        }

        //Desc: Gets all hole colors from choosen course and corresponding number of holes and overall par
        //Pre: course_id, user_id
        //Post: List of desc
        public List<course_view_course> getCourseViewCourse(int course_id, int user_id)
        {
            List<course_view_course> cvh = new List<course_view_course>();
            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "select t.tee_color as 'Color', count(h.hole_id) as 'Count', sum(h.hole_par) as 'Par', sum(havg.hole_average) as 'Yardage' " +
                                   " from hole h " + 
                                   " join course c on c.course_id = h.course_id " +
                                   " join user u on u.user_id = c.user_id " +
                                   " join tee t on t.tee_id = h.tee_id " +
                                   " join (Select avg(h.hole_yardage) as 'hole_average', h.hole_id from hole h join course c on c.course_id = h.course_id join basket b on b.basket_id = h.basket_id where c.course_id = @course_id1 and c.user_id = @user_id1 group by h.hole_num) havg on havg.hole_id = h.hole_id " + 
                                   " where c.course_id = @course_id and u.user_id = @user_id " +
                                   " group by t.tee_color";
                cmd.Parameters.AddWithValue("@course_id", course_id);
                cmd.Parameters.AddWithValue("@user_id", user_id);
                cmd.Parameters.AddWithValue("@course_id1", course_id);
                cmd.Parameters.AddWithValue("@user_id1", user_id);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.HasRows && rdr.Read())
                {
                    course_view_course c = new course_view_course();
                    c.h_color = rdr["Color"].ToString();
                    c.h_count = int.Parse(rdr["Count"].ToString());
                    c.h_par = int.Parse(rdr["Par"].ToString());
                    c.h_yardage = int.Parse(rdr["Yardage"].ToString());
                    cvh.Add(c);
                }

            }
            catch
            {
                    
            }
            finally
            {
                myConn.Close();
            }

            return cvh;
        }

        //Desc: Gets all holes num, par, yardage, basket letter, tee color
        //Pre: course_id, user_id
        //Post: List of desc
        public List<course_view_holes> getCourseViewHoles(int course_id, int user_id)
        {
            List<course_view_holes> cl = new List<course_view_holes>();

            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select h.hole_num as 'Num', (h.hole_yardage - b.basket_deduction) as 'Yard', h.hole_par as 'Par', b.basket_letter as 'Letter', t.tee_color as 'Color' " +
                                    " from course c " +
                                    " join hole h on h.course_id = c.course_id " +
                                    " join basket b on b.basket_id = h.basket_id " +
                                    " join tee t on t.tee_id = h.tee_id " +
                                    " where c.course_id = @c_id and c.user_id = @u_id" + 
                                    " order by h.hole_num, b.basket_letter, t.tee_color";
                cmd.Parameters.AddWithValue("@c_id", course_id);
                cmd.Parameters.AddWithValue("@u_id", user_id);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read() && rdr.HasRows)
                {
                    course_view_holes c = new course_view_holes();
                    c.h_num = rdr["Num"].ToString();
                    c.h_yard = rdr["Yard"].ToString();
                    c.h_par = rdr["Par"].ToString();
                    c.b_letter = rdr["Letter"].ToString();
                    c.t_color = rdr["Color"].ToString();
                    cl.Add(c);
                }
            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return cl;
        }

        public List<combobox_item_string> getCourseDistinctHoleColors(int course_id, int user_id)
        {
            List<combobox_item_string> c = new List<combobox_item_string>();

            try
            {
                myConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConn;
                cmd.CommandText = "Select Distinct t.tee_color as 'Color'" + 
                                    " from course c" + 
                                    " join hole h on h.course_id = c.course_id " +
                                    " join tee t on t.tee_id = h.tee_id" + 
                                    " where c.course_id = @c_id and c.user_id = @u_id";
                cmd.Parameters.AddWithValue("@c_id", course_id);
                cmd.Parameters.AddWithValue("@u_id", user_id);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read() && rdr.HasRows)
                {
                    combobox_item_string cb = new combobox_item_string();
                    cb.v_string = rdr["Color"].ToString();
                    c.Add(cb);

                }

            }
            catch
            {

            }
            finally
            {
                myConn.Close();
            }

            return c;
        }


    }
}
