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
    public partial class PassChangeForm : Form
    {
        private int userID = Session.CustomerID;
        public PassChangeForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string oldpass = guna2TextBox2.Text.Trim();
            string newpass = guna2TextBox1.Text.Trim();
            bool confirmpass = guna2TextBox3.Text == newpass;

            using (PariwisataEntities db = new PariwisataEntities())
            {
                var change = db.Customers.FirstOrDefault(p => p.CustomerID == userID);
                bool valid = PasswordHelper.VerifyPassword(oldpass, change.PasswordHash);

                if (valid && confirmpass)
                {
                    change.PasswordHash = PasswordHelper.HashPassword(newpass);
                    db.SaveChanges();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (!valid)
                {
                    MessageBox.Show("Old Password Wrong!");
                }
                else if (!confirmpass)
                {
                    MessageBox.Show("Confirm New Password Invalid!");
                }
                else
                {
                    MessageBox.Show("New Password Invalid");
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
