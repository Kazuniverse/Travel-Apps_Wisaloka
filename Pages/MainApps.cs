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
        SettingPage setting;
        AdminPage admin;
        Dashboard dashboard;
        Loading loading;
        HistoryPage history;
        ListMenu menu;
        public Boolean isSigned = false;
        public List<Panel> panels = new List<Panel>();

        public MainApps()
        {
            InitializeComponent();

            login = new LoginPage();
            register = new RegisterPage();
            setting = new SettingPage();
            admin = new AdminPage();
            dashboard = new Dashboard();
            history = new HistoryPage();
            menu = new ListMenu(0);

            login.RegisterRequested += (s, e) => LoadPage(panel1, register);
            register.LoginRequested += (s, e) => LoadPage(panel1, login);
            login.masuk += (s, e) => BackToHome();
            register.masuk += (s, e) => BackToHome();
            admin.logout += (s, e) => logout();
            setting.logout += (s, e) => logout();
            setting.home += (s, e) => BackToHome();
            history.home += (s, e) => BackToHome();
            dashboard.setting += (s, e) => LoadPage(panel1, setting);
            dashboard.history += (s, e) => LoadPage(panel1, history);
            menu.home += (s, e) => BackToHome();
            dashboard.list += (s, placeId) =>
            {
                menu = new ListMenu(placeId);
                LoadPage(panel1, menu);
            };
        }

        private void MainApps_Load(object sender, EventArgs e)
        {
            if (Session.CustomerID == 0)
            {
                LoadPage(panel1, new Intro());
            }
            else
            {
                LoadPage(panel1, menu);
            }
        }

        public static void LoadPage(Panel panel, UserControl page)
        {
            panel.Controls.Clear();
            panel.Dock = DockStyle.Fill;
            page.Dock = DockStyle.Fill;
            panel.Controls.Add(page);
        }

        async void BackToHome()
        {
            loading = new Loading();
            var showTask = Task.Run(() => loading.ShowDialog());

            await Task.Delay(1000);

            if (loading.InvokeRequired)
            {
                LoadPage(panel1, dashboard);
                loading.Invoke(new Action(() => loading.Close()));
            }
            else
            {
                LoadPage(panel1, dashboard);
                loading.Close();
            }
        }

        void Loged()
        {
            isSigned = !isSigned;

            if (isSigned)
            {
                panel2.Dispose();
            }
        }

        void logout()
        {
            Session.CustomerID = 0;
            LoadPage(panel1, login);
        }
    }
}
