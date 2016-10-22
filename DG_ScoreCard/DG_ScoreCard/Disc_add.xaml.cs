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
using System.IO;
using Microsoft.Win32;
using DG_ScoreCard.DGserviceReference;

namespace DG_ScoreCard
{
    /// <summary>
    /// Interaction logic for Disc_add.xaml
    /// </summary>
    public partial class Disc_add : Page
    {
        private int username = -99999;
        DGserviceClient client = new DGserviceClient();
        byte[] imageBytes = null;
        List<disc_info> dlist = new List<disc_info>();
        int speed = -9999, glide = -9999, turn = -9999, fade = -9999;


        public Disc_add()
        {
            InitializeComponent();
        }
        public Disc_add(string user)
        {
            InitializeComponent();
            username = client.getUserID(user);
            create_disc_list();
        }

        private void browse_btn_Click(object sender, RoutedEventArgs e)
        {
            
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                Filter = "Images (*.jpg,*.png)|*.jpg;*.png|All Files(*.*)|*.*"
            };

            if (dialog.ShowDialog() != true) { return; }

            DiscImagePath.Text = dialog.FileName;
            DiscImage.Source = new BitmapImage(new Uri(DiscImagePath.Text));
            DiscImage.Stretch = Stretch.Fill;

            using (var fs = new FileStream(DiscImagePath.Text, FileMode.Open, FileAccess.Read))
            {
                imageBytes = new byte[fs.Length];
                fs.Read(imageBytes, 0, System.Convert.ToInt32(fs.Length));
            }
        }

        private void submit_btn_Click(object sender, RoutedEventArgs e)
        {
            check_g.Visibility = Visibility.Visible;
        }

        private void set_fieldBordersBlack()
        {
            type_tb.BorderBrush = Brushes.Black;
            name_tb.BorderBrush = Brushes.Black;
            brand_tb.BorderBrush = Brushes.Black;
            mold_tb.BorderBrush = Brushes.Black;
            weight_tb.BorderBrush = Brushes.Black;
            color_tb.BorderBrush = Brushes.Black;
            comment_tb.BorderBrush = Brushes.Black;
        }

        //Drop Downs


        private void type_cb_DropDownClosed(object sender, EventArgs e)
        {
            type_tb.Text = type_cb.Text;
            type_cb.SelectedIndex = 0;
            if(type_tb.Text == "")
            {

            }
            else if (check_brand_set() == true)
            {
                set_type_brand(type_tb.Text, brand_tb.Text);
                set_mold(brand_tb.Text);
            }
            else
            {
                set_type_only(type_tb.Text);
                clear_mold();
            }
        }

        private void name_cb_DropDownClosed(object sender, EventArgs e)
        {
            name_tb.Text = name_cb.Text;
            name_cb.SelectedIndex = 0;
            clear_weight();
            if(name_tb.Text != "")
            {
                set_name(name_tb.Text);
            }
        }

        private void brand_cb_DropDownClosed(object sender, EventArgs e)
        {
            brand_tb.Text = brand_cb.Text;
            brand_cb.SelectedIndex = 0;
            clear_weight();
            clear_mold();
            if(brand_tb.Text == "")
            {

            }
            else if (check_type_set())
            {
                set_type_brand(type_tb.Text, brand_tb.Text);
            }
            else
            {
                set_brand_only(brand_tb.Text);
            }
        }

        private void mold_cb_DropDownClosed(object sender, EventArgs e)
        {
            mold_tb.Text = mold_cb.Text;
            mold_cb.SelectedIndex = 0;
            clear_weight();
            if(mold_tb.Text != "")
            {
                set_name_mold(name_tb.Text, mold_tb.Text);
            }
        }

        private void weight_cb_DropDownClosed(object sender, EventArgs e)
        {
            weight_tb.Text = weight_cb.Text;
            weight_cb.SelectedIndex = 0;
        }

        private void color_cb_DropDownClosed(object sender, EventArgs e)
        {
            color_tb.Text = color_cb.Text;
            color_cb.SelectedIndex = 0;
        }
        //End Drop Downs

        private void create_disc_list()
        {
            dlist.Add(new disc_info("Putter", "Nova", "Innova", "Star", 190, 170));

            //Axiom
            dlist.Add(new disc_info("Distance Driver", "Thrill", "Axiom", "Neutron", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Defy", "Axiom", "Neutron", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Vanish", "Axiom", "Neutron", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Vanish", "Axiom", "Proton", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Fireball", "Axiom", "Neutron", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Fireball", "Axiom", "Proton", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Wrath", "Axiom", "Neutron", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Wrath", "Axiom", "Proton", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Insanity", "Axiom", "Neutron", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Insanity", "Axiom", "Proton", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Virus", "Axiom", "Neutron", 175, 155));
            dlist.Add(new disc_info("Distance Driver", "Virus", "Axiom", "Proton", 175, 155));
            dlist.Add(new disc_info("Fairway Driver", "Clash", "Axiom", "Neutron", 175, 158));
            dlist.Add(new disc_info("Fairway Driver", "Clash", "Axiom", "Proton", 175, 158));
            dlist.Add(new disc_info("Fairway Driver", "Crave", "Axiom", "Neutron", 175, 158));
            dlist.Add(new disc_info("Fairway Driver", "Crave", "Axiom", "Proton", 175, 158));
            dlist.Add(new disc_info("Fairway Driver", "Inspire", "Axiom", "Neutron", 175, 155));
            dlist.Add(new disc_info("Fairway Driver", "Inspire", "Axiom", "Proton", 175, 155));
            dlist.Add(new disc_info("Mid-Range", "Alias", "Axiom", "Neutron", 178, 174));
            dlist.Add(new disc_info("Mid-Range", "Alias", "Axiom", "Proton", 178, 174));
            dlist.Add(new disc_info("Mid-Range", "Theory", "Axiom", "Neutron", 178, 166));
            dlist.Add(new disc_info("Mid-Range", "Theory", "Axiom", "Plasma", 178, 166));
            dlist.Add(new disc_info("Putter", "Envy", "Axiom", "Neutron", 175, 165));
            dlist.Add(new disc_info("Putter", "Envy", "Axiom", "Proton", 175, 165));
            dlist.Add(new disc_info("Putter", "Envy", "Axiom", "Plasma", 175, 165));
            dlist.Add(new disc_info("Putter", "Proxy", "Axiom", "Neutron", 175, 165));
            dlist.Add(new disc_info("Putter", "Proxy", "Axiom", "Plasma", 175, 165));

            //Discmania
            dlist.Add(new disc_info("Putter", "P1", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "P1", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "P1", "Discmania", "P-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "P1", "Discmania", "D-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "PX1", "Discmania", "X-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "PX1", "Discmania", "D-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "P2", "Discmania", "X-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "P2", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "P2", "Discmania", "P-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "P2", "Discmania", "D-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "P3", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Putter", "P3", "Discmania", "D-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "MD", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "MD1", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "MD1", "Discmania", "D-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "MD2", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "MD2", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "MD2", "Discmania", "D-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "MD2", "Discmania", "P-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "MD3", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "MD3", "Discmania", "G-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "GM", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Mid-Range", "GM", "Discmania", "D-Line", 0, 1));
            dlist.Add(new disc_info("Fairway Driver", "FD", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Fairway Driver", "FD", "Discmania", "G-Line", 0, 1));
            dlist.Add(new disc_info("Fairway Driver", "FD", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Fairway Driver", "FD", "Discmania", "D-Line", 0, 1));
            dlist.Add(new disc_info("Fairway Driver", "FD2", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Fairway Driver", "FD3", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "CD2", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "CD2", "Discmania", "G-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "CD", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "CD", "Discmania", "P-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "TD", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "TD", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "TD", "Discmania", "D-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "TD2", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "TD2", "Discmania", "G-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "PD", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "PD", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "PD", "Discmania", "P-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "PD", "Discmania", "G-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "DD", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "DD", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "DD", "Discmania", "P-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "PD2", "Discmania", "G-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "PD2", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "PD2", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "PD2", "Discmania", "P-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "DDX", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "DDX", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "DD2", "Discmania", "C-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "DD2", "Discmania", "S-Line", 0, 1));
            dlist.Add(new disc_info("Distance Driver", "DD2", "Discmania", "P-Line", 0, 1));

            //Discraft
            dlist.Add(new disc_info("Distance Driver", "Undertaker", "Discraft", "Z", 174, 160));
            dlist.Add(new disc_info("Distance Driver", "Crank SS", "Discraft", "Z", 174, 140));
            dlist.Add(new disc_info("Distance Driver", "Heat", "Discraft", "Z", 174, 140));
            dlist.Add(new disc_info("Distance Driver", "Heat", "Discraft", "FLX", 174, 160));
            dlist.Add(new disc_info("Distance Driver", "Heat", "Discraft", "X", 174, 160));
            dlist.Add(new disc_info("Distance Driver", "Heat", "Discraft", "Pro D", 174, 160));
            dlist.Add(new disc_info("Distance Driver", "Mantis", "Discraft", "Z", 176, 140));
            dlist.Add(new disc_info("Distance Driver", "Mantis", "Discraft", "X", 176, 160));
            dlist.Add(new disc_info("Distance Driver", "Mantis", "Discraft", "Pro D", 176, 160));
            dlist.Add(new disc_info("Distance Driver", "Crank", "Discraft", "ESP", 174, 160));
            dlist.Add(new disc_info("Distance Driver", "Crank", "Discraft", "Z", 174, 140));
            dlist.Add(new disc_info("Distance Driver", "Crank", "Discraft", "FLX", 174, 140));
            dlist.Add(new disc_info("Distance Driver", "Crank", "Discraft", "X", 174, 160));
            dlist.Add(new disc_info("Distance Driver", "Crank", "Discraft", "Pro D", 174, 160));
            dlist.Add(new disc_info("Distance Driver", "Nuke", "Discraft", "ESP", 174, 160));
            dlist.Add(new disc_info("Distance Driver", "Nuke", "Discraft", "Z", 174, 140));
            dlist.Add(new disc_info("Distance Driver", "Nuke", "Discraft", "Ti", 174, 140));
            dlist.Add(new disc_info("Distance Driver", "Nuke", "Discraft", "X", 174, 160));
            dlist.Add(new disc_info("Distance Driver", "Nuke", "Discraft", "Pro D", 174, 160));

            //Dynamic Discs

            //Gateway

            //Innova

            //Latitude 64

            //Legacy

            //MVP

            //Prodigy

            //Vibram

            //Westside

        }

        private bool check_brand_set()
        {
            bool output = false;

            if(brand_tb.Text == "Axiom" || brand_tb.Text == "Discmania" || brand_tb.Text == "Discraft" || brand_tb.Text == "Dynamic Discs" || brand_tb.Text == "Gateway" || brand_tb.Text == "Innova" ||
                brand_tb.Text == "Latitude 64" || brand_tb.Text == "Legacy" || brand_tb.Text == "MVP" || brand_tb.Text == "Prodigy" || brand_tb.Text == "Vibram" || brand_tb.Text == "Westside")
            {
                output = true;
            }
            return output;
        }

        private bool check_type_set()
        {
            bool output = false;
            if(type_tb.Text == "Putter" || type_tb.Text == "Mid-Range" || type_tb.Text == "Fairway Driver" || type_tb.Text == "Distance Driver")
            {
                output = true;
            }

            return output;
        }

        private void set_type_only(string type)
        {
            clear_name();
            for(int i = 0; i < dlist.Count(); i++)
            {
                if (dlist[i].get_type() == type )
                {
                    if(check_in_namecb(dlist[i].get_name()) == false)
                    {
                        name_cb.Items.Add(new ComboBoxItem() { Content = dlist[i].get_name() });
                    }
                }
            }
        }

        private void set_brand_only(string brand)
        {
            set_mold(brand);
            clear_name();
            for(int i = 0; i<dlist.Count(); i++)
            {
                if(dlist[i].get_brand() == brand)
                {
                    if(check_in_namecb(dlist[i].get_name()) == false)
                    name_cb.Items.Add(new ComboBoxItem() { Content = dlist[i].get_name() });
                }
            }
        }

        private void set_type_brand(string type, string brand)
        {
            clear_name();
            set_mold(brand);
            for (int i = 0; i < dlist.Count(); i++)
            {
                if (dlist[i].get_type() == type && dlist[i].get_brand() == brand)
                {
                    if (check_in_namecb(dlist[i].get_name()) == false)
                    {
                        name_cb.Items.Add(new ComboBoxItem() { Content = dlist[i].get_name() });
                    }
                }
            }
        }

        private void set_mold(string brand)
        {
            clear_mold();
            if(brand == "Axiom")
            {
                mold_cb.Items.Add(new ComboBoxItem() { Content = "Neutron" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "Proton" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "Plasma" });
            }
            if(brand == "Discmania")
            {
                mold_cb.Items.Add(new ComboBoxItem() { Content = "D-Line" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "P-Line" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "C-Line" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "S-Line" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "G-Line" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "X-Line" });
            }
            if(brand == "Discraft")
            {
                mold_cb.Items.Add(new ComboBoxItem() { Content = "Ti" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "FLX" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "ESP" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "Z" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "X" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "Pro D" });
            }
            if (brand == "Innova")
            {
                mold_cb.Items.Add(new ComboBoxItem() { Content = "Star" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "Champion" });
                mold_cb.Items.Add(new ComboBoxItem() { Content = "DX" });
            }
        }

        private void clear_mold()
        {
            mold_cb.Items.Clear();
            mold_cb.Items.Add(new ComboBoxItem() { Content = "" });
        }

        private void clear_weight()
        {
            weight_cb.Items.Clear();
            weight_cb.Items.Add(new ComboBoxItem() { Content = "" });
        }

        private void clear_name()
        {
            name_cb.Items.Clear();
            name_cb.Items.Add(new ComboBoxItem() { Content = "" });
        }

        private void set_name(string name)
        {
            clear_weight();
            for(int i = 0; i < dlist.Count(); i++)
            {
                if(dlist[i].get_name() == name)
                {
                    type_tb.Text = dlist[i].get_type();
                    brand_tb.Text = dlist[i].get_brand();
                    set_mold(dlist[i].get_brand());
                    break;
                }
            }
        }

        private void set_name_mold(string name, string mold)
        {
            clear_weight();
            for (int i = 0; i < dlist.Count(); i++)
            {
                if (dlist[i].get_name() == name && dlist[i].get_mold() == mold)
                {
                    type_tb.Text = dlist[i].get_type();
                    brand_tb.Text = dlist[i].get_brand();
                    set_weights(dlist[i].get_high(), dlist[i].get_low());
                }
            }
        }

        private void set_weights(int high, int low)
        {
            clear_weight();
            for(int i = low; i<= high; i++)
            {
                weight_cb.Items.Add(new ComboBoxItem() { Content = i.ToString() });
            }
        }

        private bool check_in_namecb(string name)
        {
            bool output = false;
            foreach(ComboBoxItem item in name_cb.Items)
            {
                if(name == item.Content.ToString())
                {
                    output = true;
                    
                }
            }

            return output;
        }

        private void comment_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void disc_stats_btn__click(object sender, TextChangedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            Rectangle[] rs = { speed1_r, speed2_r };
            if(tag[0] == 's')
            {
                rs[int.Parse(tag[1].ToString()) - 1].Visibility = Visibility.Visible;
            }
        }

        private void speed1_btn_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Content.ToString();
            Rectangle[] rs = { speed1_r, speed2_r, speed3_r, speed4_r, speed5_r, speed6_r, speed7_r, speed8_r, speed9_r, speed10_r, speed11_r, speed12_r, speed13_r, speed14_r };
            Rectangle[] rg = { glide1_r, glide2_r, glide3_r, glide4_r, glide5_r, glide6_r, glide7_r };
            Rectangle[] rt = { turn1_r, turn2_r, turn3_r, turn4_r, turn5_r, turn6_r, turn7_r};
            Rectangle[] rf = { fade1_r, fade2_r, fade3_r, fade4_r, fade5_r, fade6_r };

            string btn_con;
            if (tag[2] == '-')
            {
                btn_con = tag[1].ToString();
            }
            else
            {
                btn_con = tag[1].ToString() + tag[2].ToString();
            }

            if (tag[0] == 's')
            {
                speed = int.Parse(btn_con);
                rs[int.Parse(btn_con) - 1].Visibility = Visibility.Visible;
                close_recs(int.Parse(btn_con) - 1, tag[0]);
            }
            else if (tag[0] == 'g')
            {
                glide = int.Parse(btn_con);
                rg[int.Parse(btn_con) - 1].Visibility = Visibility.Visible;
                close_recs(int.Parse(btn_con) - 1, tag[0]);
            }
            else if(tag[0] == 't')
            {
                turn = int.Parse(btn_con) - 6;
                rt[int.Parse(btn_con) - 1].Visibility = Visibility.Visible;
                close_recs(int.Parse(btn_con) - 1, tag[0]);
            }
            else if(tag[0] == 'f')
            {
                fade = int.Parse(btn_con) - 1;
                rf[int.Parse(btn_con) - 1].Visibility = Visibility.Visible;
                close_recs(int.Parse(btn_con) - 1, tag[0]);
            }
            else
            {
                MessageBox.Show("Error ~ Button content incorrect!");
            }
        }

        private void close_recs(int j, char tag)
        {
            Rectangle[] rs = { speed1_r, speed2_r, speed3_r, speed4_r, speed5_r, speed6_r, speed7_r, speed8_r, speed9_r, speed10_r, speed11_r, speed12_r, speed13_r, speed14_r };
            Rectangle[] rg = { glide1_r, glide2_r, glide3_r, glide4_r, glide5_r, glide6_r, glide7_r };
            Rectangle[] rt = { turn1_r, turn2_r, turn3_r, turn4_r, turn5_r, turn6_r, turn7_r };
            Rectangle[] rf = { fade1_r, fade2_r, fade3_r, fade4_r, fade5_r, fade6_r};

            if (tag == 's')
            {
                for (int i = 0; i < rs.Count(); i++)
                {
                    if (rs[i].Visibility == Visibility.Visible && j != i)
                    {
                        rs[i].Visibility = Visibility.Hidden;
                    }
                }
            }
            else if(tag == 'g')
            {
                for (int i = 0; i < rg.Count(); i++)
                {
                    if (rg[i].Visibility == Visibility.Visible && j != i)
                    {
                        rg[i].Visibility = Visibility.Hidden;
                    }
                }
            }
            else if(tag == 't')
            {
                for (int i = 0; i < rt.Count(); i++)
                {
                    if (rt[i].Visibility == Visibility.Visible && j != i)
                    {
                        rt[i].Visibility = Visibility.Hidden;
                    }
                }
            }
            else if(tag == 'f')
            {
                for (int i = 0; i < rf.Count(); i++)
                {
                    if (rf[i].Visibility == Visibility.Visible && j != i)
                    {
                        rf[i].Visibility = Visibility.Hidden;
                    }
                }
            }
            else
            {

            }

        }

        private void speed_clear_btn_Click(object sender, RoutedEventArgs e)
        {
            Rectangle[] rs = { speed1_r, speed2_r, speed3_r, speed4_r, speed5_r, speed6_r, speed7_r, speed8_r, speed9_r, speed10_r, speed11_r, speed12_r, speed13_r, speed14_r };
            for (int i = 0; i < rs.Count(); i++)
            {
                if (rs[i].Visibility == Visibility.Visible)
                {
                    rs[i].Visibility = Visibility.Hidden;
                }
            }

            speed = -9999;
        }


        private void glide_clear_btn_Click(object sender, RoutedEventArgs e)
        {
            Rectangle[] rg = { glide1_r, glide2_r, glide3_r, glide4_r, glide5_r, glide6_r, glide7_r };
            for (int i = 0; i < rg.Count(); i++)
            {
                if (rg[i].Visibility == Visibility.Visible)
                {
                    rg[i].Visibility = Visibility.Hidden;
                }
            }

            glide = -9999;
        }

        private void turn_clear_btn_Click(object sender, RoutedEventArgs e)
        {
            Rectangle[] rt = { turn1_r, turn2_r, turn3_r, turn4_r, turn5_r, turn6_r, turn7_r };
            for (int i = 0; i < rt.Count(); i++)
            {
                if (rt[i].Visibility == Visibility.Visible)
                {
                    rt[i].Visibility = Visibility.Hidden;
                }
            }

            turn = -9999;

        }


        private void fade_clear_btn_Click(object sender, RoutedEventArgs e)
        {
            Rectangle[] rf = { fade1_r, fade2_r, fade3_r, fade4_r, fade5_r, fade6_r };
            for(int i = 0; i < rf.Count(); i++)
            {
                if(rf[i].Visibility == Visibility.Visible)
                {
                    rf[i].Visibility = Visibility.Hidden;
                }
            }

            fade = -9999;
        }

        private void yes_btn_Click(object sender, RoutedEventArgs e)
        {
            string output;
            bool weight_b = false;
            int weight_val = -9999;
            if (weight_tb.Text != "" && GenLib.isFieldNumberic(weight_tb.Text))
            {
                if (!GenLib.isValueBetween(int.Parse(weight_tb.Text), 200, 0))
                {
                    weight_b = true;
                }
                weight_val = int.Parse(weight_tb.Text.ToString());
            }
            if (!GenLib.isFieldLength_setOne(type_tb.Text, 50))
            {
                output = "Error ~ Type Field is over 50 characters!";
                type_tb.BorderBrush = Brushes.Red;
            }
            else if (name_tb.Text == "")
            {
                output = "Error ~ Name must be specified!";
                name_tb.BorderBrush = Brushes.Red;
            }
            else if (!GenLib.isFieldLength_setOne(name_tb.Text, 50))
            {
                output = "Error ~ Name Field is over 50 characters!";
                name_tb.BorderBrush = Brushes.Red;
            }
            else if (!GenLib.isFieldLength_setOne(brand_tb.Text, 50))
            {
                output = "Error ~ Brand Field is over 50 characters!";
                brand_tb.BorderBrush = Brushes.Red;
            }
            else if (!GenLib.isFieldLength_setOne(mold_tb.Text, 50))
            {
                output = "Error ~ Mold Field is over 50 characters!";
                mold_tb.BorderBrush = Brushes.Red;
            }
            else if (!GenLib.isFieldLength_setOne(color_tb.Text, 50))
            {
                output = "Error ~ Name Field is over 50 characters!";
                color_tb.BorderBrush = Brushes.Red;
            }
            else if (!GenLib.isFieldLength_setOne(comment_tb.Text, 200))
            {
                output = "Error ~ Comment Field is over 200 characters!";
                comment_tb.BorderBrush = Brushes.Red;
            }
            else if (!GenLib.isFieldNumberic(weight_tb.Text) && weight_tb.Text != "")
            {
                output = "Error ~ Weight needs to be numeric!";
                weight_tb.BorderBrush = Brushes.Red;
            }
            else if (weight_b)
            {
                output = "Error ~ Weight needs to be between 200-0 grams!";
                weight_tb.BorderBrush = Brushes.Red;
            }
            else
            {
                output = client.Load_Disc_Store_Proc(type_tb.Text, name_tb.Text, brand_tb.Text, mold_tb.Text, weight_val, color_tb.Text, imageBytes, comment_tb.Text, speed, glide, turn, fade, username);
                set_fieldBordersBlack();
            }
            check_g.Visibility = Visibility.Hidden;
            MessageBox.Show(output);
        }

        private void no_btn_Click(object sender, RoutedEventArgs e)
        {
            check_g.Visibility = Visibility.Hidden;
        }
    }
}
