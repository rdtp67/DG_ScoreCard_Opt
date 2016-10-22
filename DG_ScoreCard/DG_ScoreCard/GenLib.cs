using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DG_ScoreCard
{
    public static class GenLib
    {

        //Desc: Checks if String is blank
        //Pre: String
        //Post: Return True if string is blank, false otherwise
        public static bool isBlank(String t)
        {
            if (string.IsNullOrWhiteSpace(t) || string.IsNullOrEmpty(t))
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

        //Desc: Checks if Field is number
        //Pre: string
        //Post: Return true if field is number
        public static bool isFieldNumberic(string s)
        {
            int n;
            return int.TryParse(s, out n);
        }

        //Desc: Checks if Field is Under or Equal to 50 Characters
        //Pre: String
        //Post: Return True if under or equal
        public static bool isField50Chars(string field)
        {
            if (field.Count() <= 50)
                return true;
            return false;
        }

        //Desc: Checks if Field is Under or Equal to 15 Characters
        //Pre: String
        //Post: Return True if under or equal
        public static bool isField15Chars(string field)
        {
            if (field.Count() <= 15)
                return true;
            return false;
        }

        //Desc: Checks if Field is Under or Equal to 30 Characters
        //Pre: String
        //Post: Return True if under or equal
        public static bool isField30Chars(string field)
        {
            if (field.Count() <= 30)
                return true;
            return false;
        }

        //Desc: Checks if Field is Under or Equal to 100 Characters
        //Pre: String
        //Post: Return True if under or equal
        public static bool isField100Chars(string field)
        {
            if (field.Count() <= 100)
                return true;
            return false;
        }

        //Desc: Checks if Field is Under or Equal to 11 Characters
        //Pre: String
        //Post: Return True if under or equal
        public static bool isField11Chars(string field)
        {
            if (field.Count() <= 11)
                return true;
            return false;
        }

        //Desc: Checks if Field is Under or Equal to ** Characters... Should have done that above... Idiot
        //Pre: String, Int
        //Post: Return Tru if under or equal
        public static bool isFieldLength_setOne(string field, int count)
        {
            if (field.Count() <= count)
                return true;
            return false;
        }

        //Desc: Checks if Field is length between max and min
        //Pre: String, Int, Int
        //Post: Return True if between max and min
        public static bool isFieldLength_setTwo(string field, int max, int min)
        {
            if (field.Count() <= max && field.Count() >= min)
                return true;
            return false;
        }

        //Desc: Checks if Field is less than or equal value
        //Pre: value, max
        //Post: Return true if field is <= value
        public static bool isValueLessthan(int field, int count)
        {
            if (field <= count)
                return true;
            return false;
        }

        //Desc: Checks if Field is greater than or equal value
        //Pre: value, min
        //Post: Return true if field is >= value
        public static bool isValueGreaterThan(int field, int count)
        {
            if (field >= count)
                return true;
            return false;
        }

        //Desc: Checks if value is between max and min values
        //Pre: value, max, min
        //Post: Return true if value is between max and min
        public static bool isValueBetween(int field, int max, int min)
        {
            if (field <= max && field >= min)
                return true;
            return false;
        }

        //Desc: Convert MemoryStream into image
        //Pre: MemoryStream
        //Post: return BitMapImage
        public static BitmapImage convertMemoryStreamtoBitMapImage(MemoryStream ms)
        {
            BitmapImage i = new BitmapImage();
            i.BeginInit();
            i.StreamSource = ms;
            i.EndInit();
            return i;
        }


    }
}
