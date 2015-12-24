using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG_ScoreCard
{
    public static class GenLib
    {

        //Desc: Checks if String is blank
        //Pre: String
        //Post: Return True if string is blank, false otherwise
        public static bool isBlank(String t)
        {
            if (t == "")
                return true;
            else
                return false;

        }

        //Desc: Checks user_active char
        //Pre: Char 
        //Post: Return true if T, false otherwise
        public static bool isUserActive(char c)
        {
            if(c == 'T')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

    }
}
