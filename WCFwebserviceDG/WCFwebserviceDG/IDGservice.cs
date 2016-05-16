using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFwebserviceDG
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDGservice" in both code and config file together.
    [ServiceContract]
    public interface IDGservice
    {
        [OperationContract]
        int getUserID(string id);

        [OperationContract]
        void insertLocation(string loc_address, string loc_state, string loc_city, string loc_country, string loc_zip);

        [OperationContract]
        int getLocID(string loc_address, string loc_state, string loc_city, string loc_country, string loc_zip);

        [OperationContract]
        void insertUser(string username, string fname, string lname, string email, string phone, string shash, string loc_address, string loc_state, string loc_city, string loc_country, string loc_zip);

        [OperationContract]
        string checkUsername(string username);

        [OperationContract]
        bool checkLocation(string address, string state, string city, string country, string zip);

        [OperationContract]
        List<login> returnCstringLists(string username);

        [OperationContract]
        void insertCourse(string name, string website, string phone, string email, string basket_type, string year_established, string tee_type, string course_type, string terrain, string basket_maker, char? course_private, char? p2p, char? guide, string course_designer, int user_id, int loc_id, int park_id);

        [OperationContract]
        int getCourseID (int user_id, int park_id, int loc_id, string name, string website, string phone, string basket_type, string year_established, string tee_type, string course_type, string terrain, string basket_maker, char? course_private, char? p2p, char? guide, string course_designer);

        [OperationContract]
        int getCourseID2(int usr_id, string course_name);

        [OperationContract]
        bool checkCourseUserExists(int usr_id, string course_name);

        [OperationContract]
        void insertPark(string name, string hour_h, string hour_l, char? guide, char? pet, char? pri);

        [OperationContract]
        bool parkExist(string name, string hour_h, string hour_l, char? guide, char? pet, char? pri);

        [OperationContract]
        int getParkId(string park_name, char? park_private, string park_hours_high, string park_hours_low, char? park_has_guides, char? park_pet_friendly);

        [OperationContract]
        holeLib getHole();

        [OperationContract]
        void insertHole(holeLib h, int course_id, int tee, int basket, int misc, int line);

        [OperationContract]
        void insertHoleInput(string holeinput);

        [OperationContract]
        void insertBasket(holeLib h);

        [OperationContract]
        void insertBasketInput(string basketinput);

        [OperationContract]
        bool basketExists(holeLib h);

        [OperationContract]
        int getBasketID(holeLib h);

        [OperationContract]
        void insertTee(holeLib h);

        [OperationContract]
        void insertTeeInput(string teeinput);

        [OperationContract]
        bool teeExists(holeLib h);

        [OperationContract]
        int getTeeID(holeLib h);

        [OperationContract]
        void insertMisc(holeLib h);

        [OperationContract]
        void insertMiscInput(string miscinput);

        [OperationContract]
        bool miscExists(holeLib h);

        [OperationContract]
        int getMiscID(holeLib h);

        [OperationContract]
        void insertHoleLines(holeLib h);

        [OperationContract]
        void insertHoleLinesInput(string holelinesinput);

        [OperationContract]
        bool holelinesExists(holeLib h);

        [OperationContract]
        int getHoleLinesID(holeLib h);

        [OperationContract]
        List<courselist> getMyCourseList(int user_id);

        [OperationContract]
        location getParkLoc(int course_id);

        [OperationContract]
        course getCourse(int course_id, int user_id);

        [OperationContract]
        park getPark(int course_id, int user_id);

        [OperationContract]
        List<course_view_course> getCourseViewCourse(int course_id, int user_id);

        [OperationContract]
        List<course_view_holes> getCourseViewHoles(int course_id, int user_id);

        [OperationContract]
        List<combobox_item_string> getCourseDistinctHoleColors(int course_id, int user_id);

        [OperationContract]
        void Load_Course_Store_Prod(string name, string hour_h, string hour_l, char? guide, char? pet, char? pri, string loc_address, string loc_state, string loc_city, 
            string loc_country, string loc_zip, string user_id,
            string c_name, string website, string phone, string email, string basket_type, string year_established, string tee_type, string course_type, string terrain, string basket_maker, char? course_private, 
            char? p2p, char? c_guide, string course_designer);

        [OperationContract]
        string Load_Holes_Stored_Proc(List<holeLib> holes, string course_id);
    }

    [DataContract]
    public class login
    {
        string cstring;
        string user;
        char active;

        public login()
        {
            cstring = "";
            user = "";
            active = 'F';
        }

        [DataMember]
        public string user_cstring
        {
            get { return cstring; }
            set { cstring = value; }
        }
        [DataMember]
        public string user_username
        {
            get { return user; }
            set { user = value; }
        }
        [DataMember]
        public char user_active
        {
            get { return active; }
            set { active = value; }
        }
           
    }

    [DataContract]
    public class holeLib
    {
        //hole
        int hole_num;
        int hole_yardage;
        int hole_par;
        string hole_unit;
        string hole_name;
        char? hole_mando;
        char? hole_hazards;
        //basket
        char? basket_letter;
        int basket_deduction;
        string basket_note;
        //tee
        string tee_color;
        string tee_pad_type;
        string tee_notes;
        //Misc
        string misc_guide;
        char? misc_trash;
        char? misc_trail;
        char? misc_road;
        string misc_general_comments;
        //Hole Lines
        string recommended_shot;
        string recommended_disc;

        [DataMember]
        public int h_num
        {
            get { return hole_num; }
            set { hole_num = value; }
        }
        [DataMember]
        public int h_yardage
        {
            get { return hole_yardage; }
            set { hole_yardage = value; }
        }


        [DataMember]
        public int h_par
        {
            get { return hole_par; }
            set { hole_par = value; }
        }

        [DataMember]
        public string h_unit
        {
            get { return hole_unit; }
            set { hole_unit = value; }
        }

        [DataMember]
        public string h_name
        {
            get { return hole_name; }
            set { hole_name = value; }
        }

        [DataMember]
        public char? h_mando
        {
            get { return hole_mando; }
            set { hole_mando = value; }
        }

        [DataMember]
        public char? h_hazzards
        {
            get { return hole_hazards; }
            set { hole_hazards = value; }
        }

        [DataMember]
        public char? b_letter
        {
            get { return basket_letter; }
            set { basket_letter = value; }
        }

        [DataMember]
        public int b_deduction
        {
            get { return basket_deduction; }
            set { basket_deduction = value; }
        }
        [DataMember]
        public string b_note
        {
            get { return basket_note; }
            set { basket_note = value; }
        }

        [DataMember]
        public string t_color
        {
            get { return tee_color; }
            set { tee_color = value; }
        }

        [DataMember]
        public string t_pad_type
        {
            get { return tee_pad_type; }
            set { tee_pad_type = value; }
        }

        [DataMember]
        public string t_notes
        {
            get { return tee_notes; }
            set { tee_notes = value; }
        }

        [DataMember]
        public string m_guide
        {
            get { return misc_guide; }
            set { misc_guide = value; }
        }

        [DataMember]
        public char? m_trash
        {
            get { return misc_trash; }
            set { misc_trash = value; }
        }

        [DataMember]
        public char? m_trail
        {
            get { return misc_trail; }
            set { misc_trail = value; }
        }

        [DataMember]
        public char? m_road
        {
            get { return misc_road; }
            set { misc_road = value; }
        }

        [DataMember]
        public string m_general_comments
        {
            get { return misc_general_comments; }
            set { misc_general_comments = value; }
        }

        [DataMember]
        public string r_shots
        {
            get { return recommended_shot; }
            set { recommended_shot = value; }
        }

        [DataMember]
        public string r_disc
        {
            get { return recommended_disc; }
            set { recommended_disc = value; }
        }

    }

    [DataContract]
    public class courselist
    {
        int course_id;
        string course_name;

        [DataMember]
        public int c_id
        {
            get { return course_id; }
            set { course_id = value; }
        }

        [DataMember]
        public string c_name
        {
            get { return course_name; }
            set { course_name = value; }
        }
    }

    [DataContract]
    public class location
    {
        string loc_address;
        string loc_state;
        string loc_city;
        string loc_country;
        string loc_zip;

        [DataMember]
        public string l_address
        {
            get { return loc_address; }
            set { loc_address = value; }
        }

        [DataMember]
        public string l_state
        {
            get { return loc_state; }
            set { loc_state = value; }
        }

        [DataMember]
        public string l_city
        {
            get { return loc_city; }
            set { loc_city = value; }
        }

        [DataMember]
        public string l_country
        {
            get { return loc_country; }
            set { loc_country = value; }
        }

        [DataMember]
        public string l_zip
        {
            get { return loc_zip; }
            set { loc_zip = value; }
        }
    }

    [DataContract]
    public class course
    {
        string course_name;
        string course_website;
        string course_phone;
        string course_email;
        string course_basket_type;
        string course_year_est;
        string course_tee_type;
        string course_type;
        string course_terrain;
        string course_basket_manufacturer;
        string course_private;
        string course_pay;
        string course_has_guides;
        string course_designer;

        [DataMember]
        public string c_name
        {
            get { return course_name; }
            set { course_name = value; }
        }

        [DataMember]
        public string c_website
        {
            get { return course_website; }
            set { course_website = value; }
        }

        [DataMember]
        public string c_phone
        {
            get { return course_phone; }
            set { course_phone = value; }
        }

        [DataMember]
        public string c_email
        {
            get { return course_email; }
            set { course_email = value; }
        }

        [DataMember]
        public string c_basket_type
        {
            get { return course_basket_type; }
            set { course_basket_type = value; }
        }

        [DataMember]
        public string c_year_est
        {
            get { return course_year_est; }
            set { course_year_est = value; }
        }

        [DataMember]
        public string c_tee_type
        {
            get { return course_tee_type; }
            set { course_tee_type = value; }
        }

        [DataMember]
        public string c_type
        {
            get { return course_type; }
            set { course_type = value; }
        }

        [DataMember]
        public string c_terrain
        {
            get { return course_terrain; }
            set { course_terrain = value; }
        }

        [DataMember]
        public string c_basket_manu
        {
            get { return course_basket_manufacturer; }
            set { course_basket_manufacturer = value; }
        }

        [DataMember]
        public string c_pri
        {
            get { return course_private; }
            set { course_private = value; }
        }

        [DataMember]
        public string c_pay
        {
            get { return course_pay; }
            set { course_pay = value; }
        }

        [DataMember]
        public string c_has_guide
        {
            get { return course_has_guides; }
            set { course_has_guides = value; }
        }

        [DataMember]
        public string c_design
        {
            get { return course_designer; }
            set { course_designer = value; }
        }

    }

    [DataContract]
    public class park
    {
        string park_name;
        string park_private;
        string park_hours_high;
        string park_hours_low;
        string park_has_guides;
        string park_pet_friendly;

        [DataMember]
        public string p_name
        {
            get { return park_name; }
            set { park_name = value; }
        }

        [DataMember]
        public string p_private
        {
            get { return park_private; }
            set { park_private = value; }
        }

        [DataMember]
        public string p_hours_high
        {
            get { return park_hours_high; }
            set { park_hours_high = value; }
        }

        [DataMember]
        public string p_hours_low
        {
            get { return park_hours_low; }
            set { park_hours_low = value; }
        }

        [DataMember]
        public string p_has_guides
        {
            get { return park_has_guides; }
            set { park_has_guides = value; }
        }

        [DataMember]
        public string p_pet_friendly
        {
            get { return park_pet_friendly; }
            set { park_pet_friendly = value; }
        }

    }

    [DataContract]
    public class course_view_course
    {
        string color;
        int hole_count;
        int par;
        int yardage;

        [DataMember]
        public string h_color
        {
            get { return color; }
            set { color = value; }
        } 

        [DataMember]
        public int h_count
        {
            get { return hole_count; }
            set { hole_count = value; }
        }

        [DataMember]
        public int h_par
        {
            get { return par; }
            set { par = value; }
        }

        [DataMember]
        public int h_yardage
        {
            get { return yardage; }
            set { yardage = value; }
        }
    }

    [DataContract]
    public class course_view_holes
    {
        string num;
        string yard;
        string par;
        string letter;
        string color;

        [DataMember]
        public string h_num
        {
            get { return num; }
            set { num = value; }
        }

        [DataMember]
        public string h_yard
        {
            get { return yard; }
            set { yard = value; }
        }

        [DataMember]
        public string h_par
        {
            get { return par; }
            set { par = value; }
        }

        [DataMember]
        public string b_letter
        {
            get { return letter; }
            set { letter = value; }
        }

        [DataMember]
        public string t_color
        {
            get { return color; }
            set { color = value; }
        }

    }

    [DataContract]
    public class combobox_item_string
    {
        string value_string;
        [DataMember]
        public string v_string
        {
            get { return value_string; }
            set { value_string = value; }
        }
    }

}
