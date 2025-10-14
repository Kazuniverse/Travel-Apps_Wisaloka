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
    public partial class History : UserControl
    {
        public History()
        {
            InitializeComponent();
        }

        private void History_Load(object sender, EventArgs e)
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {
                var history = db.Reservations
                    .Select(u => new
                    {
                        ID = u.ReservationID,
                        Name = u.Customer.Name,
                        Email = u.Customer.Email,
                        Phone = u.Customer.Phone,
                        Transportation = u.Transportation.Name,
                        PlannedDate = u.PlannedDate,
                        TourMenu = u.TourMenu.Name,
                        TotalPrice = "Rp. " + u.TotalPrice
                    })
                    .ToList();

                guna2DataGridView2.AutoGenerateColumns = true;
                guna2DataGridView2.DataSource = history;
            }
        }
    }
}
