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
    public partial class EditingMenu : Form
    {
        public int placeID;
        private byte[] imageBytes = null;
        public EditingMenu(int PlaceID)
        {
            InitializeComponent();
            this.placeID = PlaceID;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string name = guna2TextBox1.Text.Trim();
            string price = guna2TextBox2.Text.Trim();
            string time = guna2TextBox4.Text.ToString().Trim();
            string desc = guna2TextBox6.Text.Trim();
            Boolean available = guna2RadioButton2.Checked? true : false;

            using (PariwisataEntities db = new PariwisataEntities())
            {
                var data = db.Places.FirstOrDefault(p => p.PlaceID == placeID);
                if (data != null)
                {
                    data.Name = name;
                    data.BasicPrice = int.Parse(price);
                    data.AvgTourDuration = int.Parse(time);
                    data.Description = desc;
                    data.Available = available;
                    if (imageBytes != null)
                    {
                        data.Image = imageBytes;
                    }
                    data.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                    MessageBox.Show("Data Successfully Updated!");

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            var file = openFileDialog1;
            using (file)
            {
                file.Title = "Select an Image";
                file.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    guna2PictureBox1.Image = Image.FromFile(file.FileName);
                    imageBytes = System.IO.File.ReadAllBytes(file.FileName);
                }
            }
        }

        void LoadData()
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {
                var data = db.Places
                    .Where(p => p.PlaceID == placeID)
                    .FirstOrDefault();

                guna2RadioButton2.Checked = data.Available == true;

                if (data.Image != null)
                {
                    using (var ms = new System.IO.MemoryStream(data.Image))
                    {
                        guna2PictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    guna2PictureBox1.Image = null;
                }

                guna2TextBox1.Text = data.Name;
                guna2TextBox2.Text = data.BasicPrice.ToString();
                guna2TextBox4.Text = data.AvgTourDuration.ToString();
                guna2TextBox6.Text = data.Description;
            }
        }

        private void EditingMenu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
