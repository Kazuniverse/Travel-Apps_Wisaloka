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
            string email = (string)guna2TextBox1.Text;
        }
    }
}
