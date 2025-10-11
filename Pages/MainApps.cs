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
    public partial class MainApps : Form
    {
        public MainApps()
        {
            InitializeComponent();
        }

        private void MainApps_Load(object sender, EventArgs e)
        {
            Intro intro = new Intro();
            intro.Dock = DockStyle.Fill;
            panel1.Controls.Add(intro);
            panel2.Controls.Add(new LoginPage());
        }
    }
}
