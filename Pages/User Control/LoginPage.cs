using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class LoginPage : UserControl
    {
        public event EventHandler RegisterRequested;

        public LoginPage()
        {
            InitializeComponent();
            label5.Click += (s, e) => RegisterRequested?.Invoke(this, EventArgs.Empty);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string email = guna2TextBox1.Text.Trim();
            string password = guna2TextBox2.Text.Trim();

            using (var db = new PariwisataEntities())
            {
                var customer = db.Customers.SingleOrDefault(u => u.Email == email);
                bool valid = PasswordHelper.VerifyPassword(password, customer.PasswordHash);

                if (customer == null || !valid)
                {
                    MessageBox.Show("Email Or Password Invalid!");
                    return;
                }
                else
                {
                    MessageBox.Show("Login Success!");
                    return;
                }
            }
        }
    }
}
