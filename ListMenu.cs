using Guna.UI2.WinForms;
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
    public partial class ListMenu : UserControl
    {
        public event EventHandler home;
        public int PlaceID;
        public ListMenu(int id)
        {
            InitializeComponent();

            this.PlaceID = id;

            guna2PictureBox1.Click += (s, e) => home?.Invoke(this, EventArgs.Empty);
        }

        private void ListMenu_Load(object sender, EventArgs e)
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {
                // Ambil semua tour dari place terkait
                var tours = db.TourMenus
                              .Where(t => t.PlaceID == PlaceID)
                              .ToList();

                // Asumsikan kamu punya 3 card di ListMenu
                //if (tours.Count >= 1) SetTourCard(menuCard1, tours[0]);
                //if (tours.Count >= 2) SetTourCard(menuCard2, tours[1]);
                //if (tours.Count >= 3) SetTourCard(menuCard2, tours[2]);
            }
        }

        void SetTourCard(ListMenu card, TourMenu tour)
        {
            //card.guna2PictureBox2
        }
    }
}
