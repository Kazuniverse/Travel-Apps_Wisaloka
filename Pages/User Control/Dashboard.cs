using Guna.UI2.WinForms;
using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pariwisata_Apps
{
    public partial class Dashboard : UserControl
    {
        private int cusID = Session.CustomerID;
        PariwisataEntities dbscope = new PariwisataEntities();
        private string[] say = { "Mau Kemana Nih Kita?", "Hmm... Enaknya Kemana Ya", "Wih! Lagi Banyak Duit Nih!"};
        private Random rand = new Random();
        public event EventHandler setting;
        public event EventHandler history;

        public Dashboard()
        {
            InitializeComponent();

            int nilai = foreverTrackBar1.Value * 1000000;
            guna2HtmlLabel4.Text = "Price : " + nilai.ToString();

            label2.Text = say[rand.Next(say.Length)];

            guna2CirclePictureBox2.Click += (s, e) => setting?.Invoke(this, EventArgs.Empty);
            guna2CirclePictureBox3.Click += (s, e) => history?.Invoke(this, EventArgs.Empty);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            var cus = dbscope.Customers.FirstOrDefault(p => p.CustomerID == cusID);
            label1.Text = cus != null ? "Halo, " + cus.Name + "!" : "Halo, Unknown Customer!";
            
            LoadData();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int price = foreverTrackBar1.Value * 1000000;
            Boolean avCheck = guna2RadioButton1.Checked || guna2RadioButton2.Checked;
            Boolean available = guna2RadioButton1.Checked;
            string name = guna2TextBox2.Text.ToLower();

            using (PariwisataEntities db = new PariwisataEntities())
            {
                flowLayoutPanel1.Controls.Clear();

                var menuList = db.Places.
                    Where(p => p.BasicPrice <= price && (!avCheck || p.Available == available) && (string.IsNullOrEmpty(name) || p.Name.ToLower().Contains(name) || p.Description.ToLower().Contains(name)))
                    .ToList();

                foreach (var menu in menuList)
                {
                    MenuCard card = new MenuCard();

                    int basic = menu.BasicPrice - (menu.BasicPrice / 5);

                    card.MenuTitle = menu.Name;
                    card.RealPrice = "Rp. " + menu.BasicPrice.ToString("N0");
                    card.MenuPrice = "Rp. " + basic.ToString("N0");
                    card.Description = menu.Description;

                    if (card.DetailBtn = menu.Available == true)
                    {
                        card.DetailBtn = true;
                        card.Available = "Available";
                    }
                    else
                    {
                        card.DetailBtn = false;
                        card.Available = "Unavailable";
                    }

                    if (menu.Image != null)
                    {
                        using (var ms = new MemoryStream(menu.Image))
                        {
                            card.MenuImage = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        card.MenuImage = Properties.Resources.arrow_small_left;
                    }

                    flowLayoutPanel1.Controls.Add(card);
                }
            }
        }

        void LoadData()
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {
                flowLayoutPanel1.Controls.Clear();

                var menuList = db.Places.ToList();

                foreach (var menu in menuList)
                {
                    MenuCard card = new MenuCard();

                    int basic = menu.BasicPrice - (menu.BasicPrice / 5);

                    card.MenuTitle = menu.Name;
                    card.RealPrice = "Rp. " + menu.BasicPrice.ToString("N0");
                    card.MenuPrice = "Rp. " + basic.ToString("N0");
                    card.Description = menu.Description;

                    if (card.DetailBtn = menu.Available == true)
                    {
                        card.DetailBtn = true;
                        card.Available = "Available";
                    }
                    else
                    {
                        card.DetailBtn = false;
                        card.Available = "Unavailable";
                    }

                    if (menu.Image != null)
                    {
                        using (var ms = new MemoryStream(menu.Image))
                        {
                            card.MenuImage = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        card.MenuImage = Properties.Resources.arrow_small_left;
                    }

                    flowLayoutPanel1.Controls.Add(card);
                }
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            string search = guna2TextBox1.Text.ToLower();

            using (PariwisataEntities db = new PariwisataEntities())
            {
                var cari = db.Places
                    .Where(p => (string.IsNullOrEmpty(search) || p.Name.ToLower().Contains(search) || p.Description.ToLower().Contains(search)))
                    .ToList();

                foreach (var menu in cari)
                {
                    MenuCard card = new MenuCard();
                    int basic = menu.BasicPrice - (menu.BasicPrice / 5);

                    card.MenuTitle = menu.Name;
                    card.RealPrice = "Rp. " + menu.BasicPrice.ToString("N0");
                    card.MenuPrice = "Rp. " + basic.ToString("N0");
                    card.Description = menu.Description;

                    if (card.DetailBtn = menu.Available == true)
                    {
                        card.DetailBtn = true;
                        card.Available = "Available";
                    }
                    else
                    {
                        card.DetailBtn = false;
                        card.Available = "Unavailable";
                    }

                    if (menu.Image != null)
                    {
                        using (var ms = new MemoryStream(menu.Image))
                        {
                            card.MenuImage = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        card.MenuImage = Properties.Resources.arrow_small_left;
                    }

                    flowLayoutPanel1.Controls.Add(card);
                }
            }
        }

        private void foreverTrackBar1_Scroll(object sender)
        {
            int nilai = foreverTrackBar1.Value * 1000000;

            guna2HtmlLabel4.Text = "Price : " + nilai.ToString();
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2TextBox2.Clear();
            foreverTrackBar1.Value = 100;
            guna2RadioButton1.Checked = false;
            guna2RadioButton2.Checked = false;

            LoadData();
        }
    }
}
