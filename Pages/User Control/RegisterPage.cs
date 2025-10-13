using Guna.UI2.WinForms;
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
    public partial class RegisterPage : UserControl
    {
        public event EventHandler LoginRequested;
        Customer customer = new Customer();

        public RegisterPage()
        {
            InitializeComponent();
            label7.Click += (s, e) => LoginRequested?.Invoke(this, EventArgs.Empty);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string pass = guna2TextBox4.Text.Trim();

            string hashed = PasswordHelper.HashPassword(pass);
            });

            customer.Name = guna2TextBox1.Text.Trim();
            customer.Email = guna2TextBox2.Text.Trim();
            customer.Phone = guna2TextBox3.Text.Trim();
            customer.PasswordHash = hashed;
            customer.UpdatedAt = DateTime.Now;

            using (var db = new PariwisataEntities())
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }

            main.panel2.Dispose();
        }
    }
}
