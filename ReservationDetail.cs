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
    public partial class ReservationDetail : Form
    {
        private int resID;
        public ReservationDetail(int ID)
        {
            InitializeComponent();
            this.resID = ID;
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void LoadData()
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {
                var detail = db.Reservations
                    .Include("TourMenu.Place")
                    .FirstOrDefault(r => r.ReservationID == resID);

                var place = detail.TourMenu.Place;

                guna2HtmlLabel2.Text = guna2HtmlLabel2.Text + detail.ReservationID.ToString();
                guna2HtmlLabel3.Text = guna2HtmlLabel3.Text + place.Name;
                guna2HtmlLabel4.Text = guna2HtmlLabel4.Text + detail.TourMenu.Name;
                guna2HtmlLabel5.Text = guna2HtmlLabel5.Text + detail.Transportation.Name;
                guna2HtmlLabel6.Text = guna2HtmlLabel6.Text + detail.PlannedDate.ToString();
                guna2HtmlLabel7.Text = guna2HtmlLabel7.Text + detail.OrderDate.ToString();
                guna2HtmlLabel8.Text = guna2HtmlLabel8.Text + detail.TotalPrice.ToString();

            }
        }

        private void ReservationDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
