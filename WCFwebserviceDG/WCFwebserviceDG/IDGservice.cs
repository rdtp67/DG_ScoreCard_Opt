﻿using System;
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
        void insertUser(string username, string fname, string lname, string email, string phone, string shash, string loc_address, string loc_state, string loc_city, string loc_country, string loc_zip);

        [OperationContract]
        string checkUsername(string username);

        [OperationContract]
        bool checkLocation(string address, string state, string city, string country, string zip);

        [OperationContract]
        List<login> returnCstringLists(string username);
        
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

}