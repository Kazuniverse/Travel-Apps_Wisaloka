using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pariwisata_Apps
{
    public partial class MenuCard : UserControl
    {
        public MenuCard()
        {
            InitializeComponent();
        }

        public string MenuTitle
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        public string MenuPrice
        {
            get => lblPrice.Text;
            set => lblPrice.Text = value;
        }

        public string RealPrice
        {
            get => guna2HtmlLabel2.Text;
            set => guna2HtmlLabel2.Text = value;
        }

        public Image MenuImage
        {
            get => picMenu.Image;
            set => picMenu.Image = value;
        }

        public string Description
        {
            get => descBox.Text;
            set => descBox.Text = value;
        }

        public Boolean DetailBtn
        {
            get => guna2Button1.Enabled;
            set => guna2Button1.Enabled = value;
        }

        public string Available
        {
            get => guna2HtmlLabel1.Text;
            set => guna2HtmlLabel1.Text = value;
        }


        // Tombol aksi (misal: lihat detail)
        private void btnDetail_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Kamu memilih menu: {lblTitle.Text}");
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void MenuCard_Load(object sender, EventArgs e)
        {
            if (guna2HtmlLabel1.Text == "Available")
            {
                guna2CirclePictureBox1.Image = Properties.Resources.green;
                guna2HtmlLabel1.Text = "Available";
                guna2HtmlLabel1.Location = new Point(32, 5);
                guna2Panel1.FillColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                guna2CirclePictureBox1.Image = Properties.Resources.red;
                guna2HtmlLabel1.Text = "Unavailable";
                guna2HtmlLabel1.Location = new Point(26, 5);
                guna2Panel1.FillColor = Color.FromArgb(255, 192, 192);
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}