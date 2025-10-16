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
        public event EventHandler masuk;
        Customer customer = new Customer();

        public RegisterPage()
        {
            InitializeComponent();
            label7.Click += (s, e) => LoginRequested?.Invoke(this, EventArgs.Empty);
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();

            Loading loading = new Loading();

            loading.Show();
            Application.DoEvents();

            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(1000);
            }); 

            customer.Name = guna2TextBox1.Text.Trim();
            customer.Email = guna2TextBox2.Text.Trim();
            customer.Phone = guna2TextBox3.Text.Trim();
            customer.PasswordHash = PasswordHelper.HashPassword(guna2TextBox4.Text.Trim());
            customer.UpdatedAt = DateTime.Now;

            using (var db = new PariwisataEntities())
            {
                if (db.Customers.Any(u => u.Email == customer.Email))
                {
                    loading.Hide();
                    MessageBox.Show("Email Is Already Used!");
                }
                else if (db.Customers.Any(u => u.Name == customer.Name))
                {
                    loading.Hide();
                    MessageBox.Show("Name Is Already Taken!");
                }
                else
                {
                    MessageBox.Show("Register Succes!");
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    Session.CustomerID = customer.CustomerID;
                    masuk?.Invoke(this, EventArgs.Empty);
                    loading.Hide();
                }
            }
            loading.Show();

            Panel target = parentForm.Controls["panel2"] as Panel;
            target?.Dispose();

            loading.Hide();
        }
    }
}
