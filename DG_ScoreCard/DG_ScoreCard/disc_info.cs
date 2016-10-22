using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG_ScoreCard
{
    class disc_info
    {
        string type;
        string name;
        string brand;
        string mold;
        int weight_high;
        int weight_low;
        int speed;
        int glide;
        int fade;
        int turn;

        disc_info()
        {
            type = "NULL";
            name = "NULL";
            brand = "NULL";
            weight_high = 99999;
            weight_low = -99999;

            return;
        }
        public disc_info(string d_type, string d_name, string d_brand, string d_mold, int d_weight_high, int d_weight_low)
        {
            type = d_type;
            name = d_name;
            brand = d_brand;
            weight_high = d_weight_high;
            weight_low = d_weight_low;
            mold = d_mold;
            return;
        }
        public disc_info(string d_type, string d_name, string d_brand, string d_mold, int d_weight_high, int d_weight_low, int d_speed, int d_glide, int d_fade, int d_turn)
        {
            type = d_type;
            name = d_name;
            brand = d_brand;
            weight_high = d_weight_high;
            weight_low = d_weight_low;
            mold = d_mold;
            speed = d_speed;
            glide = d_glide;
            fade = d_fade;
            turn = d_turn;
            return;
        }
        public string get_type() { return type; }
        public string get_name() { return name; }
        public string get_brand() { return brand; }
        public int get_high() { return weight_high; }
        public int get_low() { return weight_low; }
        public string get_mold() { return mold; }
        public int get_speed() { return speed; }
        public int get_glide() { return glide; }
        public int get_fade() { return fade; }
        public int get_turn() { return turn; }

    }
}
