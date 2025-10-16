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
        public event EventHandler masuk;

        public LoginPage()
        {
            InitializeComponent();
            label5.Click += (s, e) => RegisterRequested?.Invoke(this, EventArgs.Empty);
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            Form parent = this.FindForm();

            Loading load = new Loading();

            load.Show();
            Application.DoEvents();

            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(1000);
            });

            string email = guna2TextBox1.Text.Trim();
            string password = guna2TextBox2.Text.Trim();

            using (var db = new PariwisataEntities())
            {
                var customer = db.Customers.SingleOrDefault(u => u.Email == email);
                bool valid = PasswordHelper.VerifyPassword(password, customer.PasswordHash);

                if (customer == null || !valid)
                {
                    load.Hide();
                    MessageBox.Show("Email Or Password Invalid!");
                    return;
                }
                else
                {
                    load.Hide();
                    Session.CustomerID = customer.CustomerID;
                    masuk?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show("Login Success!");
                }

                load.Show();
                Panel target = parent.Controls["panel2"] as Panel;
                target?.Dispose();
                load.Hide();
            }
        }
    }
}
