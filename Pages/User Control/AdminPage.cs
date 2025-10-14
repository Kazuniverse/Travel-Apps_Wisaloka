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
    public partial class AdminPage : UserControl
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        void LoadPage(UserControl page)
        {
            panel1.Controls.Clear();
            page.Dock = DockStyle.Fill;
            panel1.Controls.Add(page);
        }

        private void AdminPage_Load(object sender, EventArgs e)
        {
            LoadPage(new UserData());
        }
    }
}
