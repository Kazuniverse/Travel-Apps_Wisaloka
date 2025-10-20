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
    public partial class SettingPage : UserControl
    {
        public event EventHandler logout;
        private int id = Session.CustomerID;
        public event EventHandler home;
        public SettingPage()
        {
            InitializeComponent();
            guna2Button2.Click += (s, e) => logout?.Invoke(this, EventArgs.Empty);
            guna2Button7.Click += (s, e) => home?.Invoke(this, EventArgs.Empty);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            PassChangeForm change = new PassChangeForm();
            change.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            LoadInput();
        }

        void LoadInput()
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {
                var cus = db.Customers.FirstOrDefault(c => c.CustomerID == (id > 0 ? id : 1));

                guna2TextBox2.Text = cus.Name;
                guna2TextBox3.Text = cus.Phone;
                guna2TextBox4.Text = cus.Email;
            }
        }

        private void SettingPage_Load(object sender, EventArgs e)
        {
            guna2HtmlLabel10.Text = guna2TextBox6.Text;

            LoadInput();
        }

        private async void guna2Button4_Click(object sender, EventArgs e)
        {
            Loading load = new Loading();

            load.Show();
            Application.DoEvents();

            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(1000);
            });

            using (PariwisataEntities db = new PariwisataEntities())
            {
                string name = guna2TextBox2.Text;
                string phone = guna2TextBox3.Text;
                string email = guna2TextBox4.Text;

                var update = db.Customers.FirstOrDefault(c => c.CustomerID == (id > 0 ? id : 1));
                if (update != null)
                {
                    load.Hide();
                    update.Name = name;
                    update.Phone = phone;
                    update.Email = email;
                    db.SaveChanges();
                    MessageBox.Show("Profile Update Successfully!");
                }

                load.Show();
                LoadInput();
                load.Hide();
            }
        }

        void ValueChange(object s, EventArgs e)
        {
            guna2HtmlLabel7.Text = guna2TextBox2.Text;
            guna2HtmlLabel8.Text = guna2TextBox3.Text;
            guna2HtmlLabel9.Text = guna2TextBox4.Text;
        }
    }
}
