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
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Fungsi helper untuk resize gambar
        private Image ByteArrayToThumbnail(byte[] bytes, int width, int height)
        {
            using (var ms = new System.IO.MemoryStream(bytes))
            {
                var img = Image.FromStream(ms);
                return new Bitmap(img, new Size(width, height));
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadData();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var placeID = (int)guna2DataGridView1.CurrentRow.Cells["placeIDDataGridViewTextBoxColumn"].Value;
            Form edit = new EditingMenu(placeID);
            if (edit.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form add = new AddingMenu();
            if (add.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        public void LoadData()
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {
                var menu = db.Places
                    .AsEnumerable()
                    .Select(u => new
                    {
                        u.PlaceID,
                        u.Name,
                        Image = u.Image != null ? ByteArrayToThumbnail(u.Image, 35, 30) : null,
                        u.Description,
                        BasicPrice = "Rp. " + u.BasicPrice,
                        u.Available,
                        AvgTourDuration = u.AvgTourDuration + " Day",
                        u.UpdatedAt
                    })
                    .ToList();

                guna2DataGridView1.AutoGenerateColumns = true;
                guna2DataGridView1.DataSource = menu;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {
                var name = (string)guna2DataGridView1.CurrentRow.Cells["nameDataGridViewTextBoxColumn"].Value;
                int id = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells["placeIDDataGridViewTextBoxColumn"].Value);
                var place = db.Places.FirstOrDefault(p => p.PlaceID == id);

                DialogResult result = MessageBox.Show($"Are You Sure To Delete \"{name}\"?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (place != null)
                    {
                        db.Places.Remove(place);
                        db.SaveChanges();
                        MessageBox.Show("Delete Successfull!");
                    }
                }
                else
                {
                    MessageBox.Show("Delete Cancelled!");
                }

                LoadData();
            }
        }
    }
}
