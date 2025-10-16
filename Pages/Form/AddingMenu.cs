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
    public partial class AddingMenu : Form
    {
        private byte[] imageBytes = null;
        public AddingMenu()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string name = guna2TextBox1.Text.Trim();
            string price = guna2TextBox2.Text.Trim();
            string time = guna2TextBox4.Text.Trim();
            string desc = guna2TextBox6.Text.Trim();
            bool available = guna2RadioButton2.Checked ? true : false;

            using (PariwisataEntities db = new PariwisataEntities())
            {
                var data = new Place
                {
                    Name = name,
                    BasicPrice = int.Parse(price),
                    AvgTourDuration = int.Parse(time),
                    Description = desc,
                    Available = available,
                    UpdatedAt = DateTime.Now
                };

                if (imageBytes != null)
                {
                    data.Image = imageBytes;
                }

                db.Places.Add(data);
                db.SaveChanges();
                MessageBox.Show("Data Successfully Added!");

                this.DialogResult = DialogResult.OK;
                this.Close();
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
