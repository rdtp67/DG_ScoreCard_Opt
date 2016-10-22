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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DG_ScoreCard.DGserviceReference;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media;


namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for Disc_view.xaml
    /// </summary>
    public partial class Disc_view : Page
    {
        DGserviceClient client = new DGserviceClient();
        List<disc> discs = new List<disc>();
        disc cur_disc = new disc();
        byte[] imageBytes = null;
        int username;
        string user;
        int cur_c = -9999;
        public Disc_view()
        {
            InitializeComponent();
        }

        public Disc_view(string user)
        {
            InitializeComponent();
            username = client.getUserID(user);
            update_page();

        }

        private void update_page()
        {
            string output;
            int putter_c = 0;
            int mid_c = 0;
            int fair_c = 0;
            int dri_c = 0;
            int other_c = 0;
            discs = client.get_discNames(username);

            putter_cb.Items.Clear();
            mid_cb.Items.Clear();
            fair_cb.Items.Clear();
            dist_cb.Items.Clear();
            other_cb.Items.Clear();

            for (int i = 0; i < discs.Count(); i++)
            {
                output = "Name: " + discs[i].d_name;
                if (discs[i].d_mold != "")
                    output += " Mold: " + discs[i].d_mold;
                if (discs[i].d_weight != -9999)
                    output += " Weight: " + discs[i].d_weight;
                if (discs[i].d_color != "")
                    output += " Color: " + discs[i].d_color;

                if (discs[i].d_type == "Putter")
                {
                    putter_c++;
                    putter_cb.Items.Add(putter_c.ToString() + ". " + output);
                    discs[i].d_speed = putter_c;
                }
                else if (discs[i].d_type == "Mid-Range")
                {
                    mid_c++;
                    mid_cb.Items.Add(mid_c.ToString() + ". " + output);
                    discs[i].d_speed = mid_c;
                }
                else if (discs[i].d_type == "Fairway Driver")
                {
                    fair_c++;
                    fair_cb.Items.Add(fair_c.ToString() + ". " + output);
                    discs[i].d_speed = fair_c;
                }
                else if (discs[i].d_type == "Distance Driver")
                {
                    dri_c++;
                    dist_cb.Items.Add(dri_c.ToString() + ". " + output);
                    discs[i].d_speed = dri_c;
                }
                else
                {
                    other_c++;
                    other_cb.Items.Add(other_c.ToString() + ". " + output);
                    discs[i].d_speed = other_c;
                }


            }

            set_colors();
        }

        private void speed_color_set(int i)
        {
            string[] col = { "#FFD4D400", "#FF72D100", "#FF00D113", "#FF00CF96", "#FF00D3A3", "#FF00C5CF", "#FF0096CF", "#FF004ACD", "#FF004ACD", "#FF2F00CF", "#FF7300D3", "#FF9600CF", "#FFD100D1", "#FFD30069" };
            Color color = (Color)ColorConverter.ConvertFromString(col[i - 1]);
            speed_tbl.Foreground = new System.Windows.Media.SolidColorBrush(color);
        }

        private void glide_color_set(int i)
        {
            string[] col = { "#FFD4D400", "#FF43D400", "#FF00D4C1", "#FF0074D4", "#FF7400D4", "#FFCA00D4", "#FFD40043" };
            Color color = (Color)ColorConverter.ConvertFromString(col[i - 1]);
            glide_tbl.Foreground = new System.Windows.Media.SolidColorBrush(color);
        }

        private void turn_color_set(int i)
        {
            string[] col = { "#FFD4D400", "#FF43D400", "#FF00D4C1", "#FF0074D4", "#FF7400D4", "#FFCA00D4", "#FFD40043" };
            Color color = (Color)ColorConverter.ConvertFromString(col[i + 5]);
            turn_tbl.Foreground = new System.Windows.Media.SolidColorBrush(color);
        }

        private void fade_color_set(int i)
        {
            string[] col = { "#FFD4D400", "#FF30D400", "#FF00D4C1", "#FF0087D4", "#FF7400D4", "#FFD4004D" };
            Color color = (Color)ColorConverter.ConvertFromString(col[i]);
            fade_tbl.Foreground = new System.Windows.Media.SolidColorBrush(color);
        }

        private string check_edit_fields()
        {
            string output = "";
            bool weight_b = false;
            int weight_val = -9999;
            if (weight_tb.Text != "" && GenLib.isFieldNumberic(weight_tb.Text) && weight_tb.Text != "NA")
            {
                if (GenLib.isValueBetween(int.Parse(weight_tb.Text), 200, 0) == false)
                {
                    weight_b = true;
                }
                weight_val = int.Parse(weight_tb.Text.ToString());
            }

            if (name_tb.Text == "")
            {
                output = "Error ~ Name must be specified!";
                name_tb.Background = Brushes.LightBlue;
            }
            else if (!GenLib.isFieldLength_setOne(type_tb.Text, 50))
            {
                output = "Error ~ Type Field is over 50 characters!";
                type_tb.Background = Brushes.LightBlue;
            }
            else if (!GenLib.isFieldLength_setOne(name_tb.Text, 50))
            {
                output = "Error ~ Name Field is over 50 characters!";
                name_tb.Background = Brushes.LightBlue;
            }
            else if (!GenLib.isFieldLength_setOne(brand_tb.Text, 50))
            {
                output = "Error ~ Brand Field is over 50 characters!";
                brand_tb.Background = Brushes.LightBlue;
            }
            else if (!GenLib.isFieldLength_setOne(mold_tb.Text, 50))
            {
                output = "Error ~ Mold Field is over 50 characters!";
                mold_tb.Background = Brushes.LightBlue;
            }
            else if (!GenLib.isFieldLength_setOne(color_tb.Text, 50))
            {
                output = "Error ~ Name Field is over 50 characters!";
                color_tb.Background = Brushes.LightBlue;
            }
            else if (!GenLib.isFieldLength_setOne(comment_tb.Text, 200))
            {
                output = "Error ~ Comment Field is over 200 characters!";
                comment_tb.Background = Brushes.LightBlue;
            }
            else if (!GenLib.isFieldNumberic(weight_tb.Text) && weight_tb.Text != "" && weight_tb.Text != "NA")
            {
                output = "Error ~ Weight needs to be numeric!";
                weight_tb.Background = Brushes.LightBlue;
            }
            else if (weight_b)
            {
                output = "Error ~ Weight needs to be between 200-0 grams!";
                weight_tb.Background = Brushes.LightBlue;
            }
            else
            {
                type_tb.Background = Brushes.White;
                name_tb.Background = Brushes.White;
                brand_tb.Background = Brushes.White;
                mold_tb.Background = Brushes.White;
                weight_tb.Background = Brushes.White;
                color_tb.Background = Brushes.White;
                comment_tb.Background = Brushes.White;
            }

            return output;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //adddddd iinnnnnn eeeeddddiiitttt vvvvaaaalllluuuueeee ccccchhhheeeecccckkkksss
            string edit_text = check_edit_fields();
            if(edit_text == "")
            {
                cur_disc.d_type = type_tb.Text;
                cur_disc.d_name = name_tb.Text;
                cur_disc.d_brand = brand_tb.Text;
                cur_disc.d_mold = mold_tb.Text;

                if (weight_tb.Text != "" && weight_tb.Text != "NA" && GenLib.isFieldNumberic(weight_tb.Text))
                    cur_disc.d_weight = int.Parse(weight_tb.Text);
                else
                    cur_disc.d_weight = -9999;

                cur_disc.d_color = color_tb.Text;
                cur_disc.d_comment = comment_tb.Text;
                cur_disc.d_image = imageBytes;
                cur_disc.d_speed = (speed_cb.Text == "") ? -9999 : int.Parse(speed_cb.Text);
                cur_disc.d_glide = (glide_cb.Text == "") ? -9999 : int.Parse(glide_cb.Text);
                cur_disc.d_turn = (turn_cb.Text == "") ? -9999 : int.Parse(turn_cb.Text);
                cur_disc.d_fade = (fade_cb.Text == "") ? -9999 : int.Parse(fade_cb.Text);

                titlediscname_tbl.Text = name_tb.Text;
                type_tbl.Text = type_tb.Text;
                name_tbl.Text = name_tb.Text;
                brand_tbl.Text = brand_tb.Text;
                mold_tbl.Text = mold_tb.Text;
                weight_tbl.Text = weight_tb.Text;
                color_tbl.Text = color_tb.Text;
                comment_tbl.Text = comment_tb.Text;
                speed_tbl.Text = (speed_cb.Text == "") ? "NA" : speed_cb.Text;
                glide_tbl.Text = (glide_cb.Text == "") ? "NA" : glide_cb.Text;
                turn_tbl.Text = (turn_cb.Text == "") ? "NA" : turn_cb.Text;
                fade_tbl.Text = (fade_cb.Text == "") ? "NA" : fade_cb.Text;
                set_tbl_vis(true);
                set_tb_vis(false);
                MessageBox.Show(client.update_disc(cur_disc));
                button.Visibility = Visibility.Hidden;
                update_page();
            }
            else
            {
                MessageBox.Show(edit_text);
            }

        }

        private void set_tb_vis(bool set)
        {
            Visibility v = (set == true) ? Visibility.Visible : Visibility.Hidden;
            type_tb.Visibility = v;
            name_tb.Visibility = v;
            brand_tb.Visibility = v;
            mold_tb.Visibility = v;
            weight_tb.Visibility = v;
            color_tb.Visibility = v;
            comment_tb.Visibility = v;
            image_g.Visibility = v;
            speed_cb.Visibility = v;
            glide_cb.Visibility = v;
            turn_cb.Visibility = v;
            fade_cb.Visibility = v;
        }

        private void set_tbl_vis(bool set)
        {
            Visibility v = (set == true) ? Visibility.Visible : Visibility.Hidden;
            type_tbl.Visibility = v;
            name_tbl.Visibility = v;
            brand_tbl.Visibility = v;
            mold_tbl.Visibility = v;
            weight_tbl.Visibility = v;
            color_tbl.Visibility = v;
            comment_tbl.Visibility = v;
        }

        private void editdisc_btn_Click(object sender, RoutedEventArgs e)
        {
            if(cur_c != -9999)
            {
                type_tb.Text = cur_disc.d_type;
                name_tb.Text = cur_disc.d_name;
                brand_tb.Text = cur_disc.d_brand;
                mold_tb.Text = cur_disc.d_mold;
                weight_tb.Text = cur_disc.d_weight != -9999 ? cur_disc.d_weight.ToString() : "NA";
                color_tb.Text = cur_disc.d_color;
                comment_tb.Text = cur_disc.d_comment;
                speed_cb.SelectedIndex = (cur_disc.d_speed == -9999 ) ? 0 : int.Parse(cur_disc.d_speed.ToString());
                glide_cb.SelectedIndex = (cur_disc.d_fade == -9999) ? 0 : int.Parse(cur_disc.d_glide.ToString());
                turn_cb.SelectedIndex = (cur_disc.d_turn == -9999) ? 0 : (int.Parse(cur_disc.d_turn.ToString()) + 6 );
                fade_cb.SelectedIndex = (cur_disc.d_fade == -9999) ? 0 : (int.Parse(cur_disc.d_fade.ToString()) + 1);
                set_tbl_vis(false);
                set_tb_vis(true);
                button.Visibility = Visibility.Visible;
            }

 
        }
        private void yes_btn_Click(object sender, RoutedEventArgs e)
        {
            if (cur_c != -9999)
            {
                MessageBox.Show(client.delete_disc(cur_disc.d_id));
                cur_c = -9999;
                titlediscname_tbl.Text = "View Discs";
                type_tbl.Text = "";
                name_tbl.Text = "";
                brand_tbl.Text = "";
                brand_tbl.Text = "";
                mold_tbl.Text = "";
                weight_tbl.Text = "";
                color_tbl.Text = "";
                comment_tbl.Text = "";
               
                update_page();

                speed_tbl.Text = "";
                glide_tbl.Text = "";
                turn_tbl.Text = "";
                fade_tbl.Text = "";

                cur_c = -9999;
            }

            check_g.Visibility = Visibility.Hidden;
        }

        private void no_btn_Click(object sender, RoutedEventArgs e)
        {
            check_g.Visibility = Visibility.Hidden;
        }

        private void deletedisc_btn_Click(object sender, RoutedEventArgs e)
        {
            if(cur_c != -9999)
            {
                set_tbl_vis(true);
            set_tb_vis(false);
            button.Visibility = Visibility.Hidden;
            check_g.Visibility = Visibility.Visible;
            }
            
            
            //set cur c to -9999, reset page
        }

        private void update_disc_info(int c, string type)
        {
            cur_c = c;
            set_tbl_vis(true);
            set_tb_vis(false);
            button.Visibility = Visibility.Hidden;

            for(int i = 0; i < discs.Count(); i++)
            {
                if (type == "Other")
                {
                    if (discs[i].d_speed == c && discs[i].d_type != "Putter" && discs[i].d_type != "Mid-Range" && discs[i].d_type != "Fairway Driver" && discs[i].d_type != "Distance Driver")
                    {
                        cur_disc = client.get_discInfo(discs[i].d_id);
                        break;
                    }
                }
                else
                {
                    if(discs[i].d_speed == c && discs[i].d_type == type)
                    {
                         cur_disc = client.get_discInfo(discs[i].d_id);
                         break;
                    }
                }
                   
                
            }

            if (cur_disc.d_image != null)
            {
                MemoryStream ms = new MemoryStream(cur_disc.d_image);
                DiscImage.Source = GenLib.convertMemoryStreamtoBitMapImage(ms);
                DiscImage.Stretch = Stretch.Fill;
            }
            else
            {
                DiscImage.Source = null;
            }
            titlediscname_tbl.Text = cur_disc.d_name;
            type_tbl.Text = cur_disc.d_type;
            name_tbl.Text = cur_disc.d_name;
            brand_tbl.Text = cur_disc.d_brand;
            mold_tbl.Text = cur_disc.d_mold;
            weight_tbl.Text = (cur_disc.d_weight.ToString() != "-9999") ? cur_disc.d_weight.ToString() : "NA";
            color_tbl.Text = cur_disc.d_color;
            comment_tbl.Text = cur_disc.d_comment;
            speed_tbl.Text = (cur_disc.d_speed.ToString() != "-9999") ? cur_disc.d_speed.ToString() : "NA";
            glide_tbl.Text = (cur_disc.d_glide.ToString() != "-9999") ? cur_disc.d_glide.ToString() : "NA";
            turn_tbl.Text = (cur_disc.d_turn.ToString() != "-9999") ? cur_disc.d_turn.ToString() : "NA";
            fade_tbl.Text = (cur_disc.d_fade.ToString() != "-9999") ? cur_disc.d_fade.ToString() : "NA";

            set_colors();
           
        }

        private void set_colors()
        {
            if (speed_tbl.Text != "NA" && speed_tbl.Text != "0")
            {
                speed_color_set(int.Parse(speed_tbl.Text));
            }
            else
            {
                speed_tbl.Foreground = Brushes.Black;
            }
            if (glide_tbl.Text != "NA" && glide_tbl.Text != "0")
            {
                glide_color_set(int.Parse(glide_tbl.Text));
            }
            else
            {
                glide_tbl.Foreground = Brushes.Black;
            }
            if (turn_tbl.Text != "NA" && turn_tbl.Text != "0")
            {
                turn_color_set(int.Parse(turn_tbl.Text));
            }
            else
            {
                turn_tbl.Foreground = Brushes.Black;
            }
            if (fade_tbl.Text != "NA" && fade_tbl.Text != "0")
            {
                fade_color_set(int.Parse(fade_tbl.Text));
            }
            else
            {
                fade_tbl.Foreground = Brushes.Black;
            }
        }

        private void putter_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox cb = sender as ComboBox;
            if(cb.SelectedIndex.ToString() != "-1")
            {
                  string c = cb.SelectedItem.ToString();
                  update_disc_info( int.Parse(c[0].ToString()), "Putter");
                  cb.SelectedIndex = -1;
            }
        }

        private void mid_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex.ToString() != "-1")
            {
                string c = cb.SelectedItem.ToString();
                update_disc_info(int.Parse(c[0].ToString()), "Mid-Range");
                cb.SelectedIndex = -1;
            }
        }

        private void fair_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex.ToString() != "-1")
            {
                string c = cb.SelectedItem.ToString();
                update_disc_info(int.Parse(c[0].ToString()), "Fairway Driver");
                cb.SelectedIndex = -1;
            }
            
        }

        private void dist_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex.ToString() != "-1")
            {
                string c = cb.SelectedItem.ToString();
                update_disc_info(int.Parse(c[0].ToString()), "Distance Driver");
                cb.SelectedIndex = -1;
            }
        }

        private void other_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex.ToString() != "-1")
            {
                string c = cb.SelectedItem.ToString();
                update_disc_info(int.Parse(c[0].ToString()), "Other");
                cb.SelectedIndex = -1;
            }
        }

        private void brownse_btn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                Filter = "Images (*.jpg,*.png)|*.jpg;*.png|All Files(*.*)|*.*"
            };

            if (dialog.ShowDialog() != true) { return; }

            discimagepath_tb.Text = dialog.FileName;
            DiscImage.Source = new BitmapImage(new Uri(discimagepath_tb.Text));
            DiscImage.Stretch = Stretch.Fill;

            using (var fs = new FileStream(discimagepath_tb.Text, FileMode.Open, FileAccess.Read))
            {
                imageBytes = new byte[fs.Length];
                fs.Read(imageBytes, 0, System.Convert.ToInt32(fs.Length));
            }
        }
    }
}
