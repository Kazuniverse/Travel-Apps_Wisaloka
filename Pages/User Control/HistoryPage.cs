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
    public partial class HistoryPage : UserControl
    {
        private int id = Session.CustomerID;
        public event EventHandler home;
        public HistoryPage()
        {
            InitializeComponent();

            guna2Button7.Click += (s, e) => home?.Invoke(this, EventArgs.Empty);

            guna2Panel4.FillColor = Color.FromArgb(30, Color.Black);
            guna2Panel4.Visible = false;
        }

        void LoadData()
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {

                flowLayoutPanel1.Controls.Clear();
                var histories = db.Reservations
                    .Include("TourMenu.Place")
                    .Where(p => p.CustomerID == id)
                    .ToList();

                foreach (var history in histories)
                {
                    HistoryCard card = new HistoryCard();
                    var place = history.TourMenu.Place;

                    card.Title = ($"{place.Name}" + $" - {history.TourMenu.Name}");
                    card.PlannedDate = "Planned Date - " + history.PlannedDate.ToString();
                    card.OderDate = "Order Date - " + history.OrderDate.ToString();
                    card.Price = history.TotalPrice.ToString();

                    if (place.Image != null)
                    {
                        using (var ms = new MemoryStream(place.Image))
                        {
                            card.Cover = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        card.Cover = Properties.Resources.arrow_small_left;
                    }

                    card.Page = this;
                    card.ReservationID = history.ReservationID;

                    card.Page = this;
                    flowLayoutPanel1.Controls.Add(card);
                    card.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                }
            }
        }

        private void HistoryPage_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void flowLayoutPanel1_Layout(object sender, LayoutEventArgs e)
        {
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                ctrl.Width = flowLayoutPanel1.ClientSize.Width - 20;
            }
        }
    }
}
