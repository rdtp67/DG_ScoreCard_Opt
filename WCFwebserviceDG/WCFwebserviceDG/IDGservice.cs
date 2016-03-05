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
        string getUserID(string id);

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
        void insertCourse(string name, string website, string phone, string basket_type, string year_established, string tee_type, string course_type, string terrain, string basket_maker, char? course_private, char? p2p, char? guide, string course_designer, int user_id, int loc_id, int park_id);

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
        void submitCourse(holeLib[] h, int hole_count, string username, string c_name, string c_website, string c_phone, string basket_type, string year_established, string tee_type, string course_type, string terrain, string basket_maker, char? course_private, char? p2p, char? c_guide, string course_designer, string p_name, string hour_h, string hour_l, char? guide, char? pet, char? pri, string loc_address, string loc_state, string loc_city, string loc_country, string loc_zip);

        [OperationContract]
        void insertHole(holeLib h, int course_id);

        [OperationContract]
        void insertBasket(holeLib h);

        [OperationContract]
        bool basketExists(holeLib h);

        [OperationContract]
        int getBasketID(holeLib h);

        [OperationContract]
        void insertTee(holeLib h);

        [OperationContract]
        bool teeExists(holeLib h);

        [OperationContract]
        int getTeeID(holeLib h);

        [OperationContract]
        void insertMisc(holeLib h);

        [OperationContract]
        bool miscExists(holeLib h);

        [OperationContract]
        int getMiscID(holeLib h);

        [OperationContract]
        void insertHoleLines(holeLib h);

        [OperationContract]
        bool holelinesExists(holeLib h);

        [OperationContract]
        int getHoleLinesID(holeLib h);


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


}
