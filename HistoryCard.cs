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
    public partial class HistoryCard : UserControl
    {
        public HistoryCard()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => guna2HtmlLabel1.Text;
            set => guna2HtmlLabel1.Text = value;
        }

        public string PlannedDate
        {
            get => guna2HtmlLabel3.Text;
            set => guna2HtmlLabel3.Text = value;
        }

        public string OderDate
        {
            get => guna2HtmlLabel2.Text;
            set => guna2HtmlLabel2.Text = value;
        }

        public string Price
        {
            get => guna2HtmlLabel4.Text;
            set => guna2HtmlLabel4.Text = value;
        }

        public Image Cover
        {
            get => guna2PictureBox1.Image;
            set => guna2PictureBox1.Image = value;
        }
    }
}
