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
    public partial class UserData : UserControl
    {
        public UserData()
        {
            InitializeComponent();
        }

        private void UserData_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_Click(object sender, EventArgs e)
        {
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {
                var role = (string)guna2DataGridView2.CurrentRow.Cells["roleDataGridViewTextBoxColumn1"].Value;
                int id = Convert.ToInt32(guna2DataGridView2.SelectedRows[0].Cells["userIDDataGridViewTextBoxColumn1"].Value);

                var user = db.Users.FirstOrDefault(u => u.UserID == id);
                if (role == "STAFF")
                {
                    DialogResult result = MessageBox.Show("Change This User Role To Admin?", "Confirm Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        user.Role = "ADMIN";
                        db.SaveChanges();
                        MessageBox.Show("Change Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Change Cancelled!");
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Change This User Role To Staff?", "Confirm Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        user.Role = "STAFF";
                        db.SaveChanges();
                        MessageBox.Show("Change Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Change Cancelled!");
                    }
                }

                LoadData();
            }

        }
        void LoadData()
        {
            using (PariwisataEntities db = new PariwisataEntities())
            {
                var cus = db.Customers
                    .Select(u => new
                    {
                        CusID = u.CustomerID,
                        u.Name,
                        u.Email,
                        u.Phone,
                        u.UpdatedAt
                    })
                    .ToList();
                guna2DataGridView1.AutoGenerateColumns = true;
                guna2DataGridView1.DataSource = cus;

                var user = db.Users
                    .Select(u => new
                    {
                        UserID = u.UserID,
                        u.Username,
                        u.PasswordHash,
                        u.FullName,
                        u.Role,
                        u.CreatedAt
                    })
                    .ToList();
                guna2DataGridView2.AutoGenerateColumns = true;
                guna2DataGridView2.DataSource = user;
            }
        }
    }
}
