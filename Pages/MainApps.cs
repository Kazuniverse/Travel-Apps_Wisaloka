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
        LoginPage login;
        RegisterPage register;
        public Boolean isSigned = false;
        public List<Panel> panels = new List<Panel>();

        public MainApps()
        {
            InitializeComponent();

            login = new LoginPage();
            register = new RegisterPage();

            login.RegisterRequested += (s, e) => LoadPage(panel2, register);
            register.LoginRequested += (s, e) => LoadPage(panel2, login);
        }

        private void MainApps_Load(object sender, EventArgs e)
        {
            LoadPage(panel1, new Intro());
            LoadPage(panel2, login);
            LoadPage(panel3, new Dashboard());
            LoadPage(panel4, new AdminPage());
        }

        public static void LoadPage(Panel panel, UserControl page)
        {
            panel.Controls.Clear();
            panel.Dock = DockStyle.Fill;
            page.Dock = DockStyle.Fill;
            panel.Controls.Add(page);
        }

        void Loged()
        {
            isSigned = !isSigned;

            if (isSigned)
            {
                panel2.Dispose();
            }
        }

        private void ToRegis(object sender, EventArgs e)
        {
            LoadPage(panel2, new RegisterPage());
        }

        private void ToLogin(object sender, EventArgs e)
        {
            LoadPage(panel2, new LoginPage());
        }

    }
}
