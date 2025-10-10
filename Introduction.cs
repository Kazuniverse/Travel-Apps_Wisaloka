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
    public partial class Introduction : Form
    {
        private List<Panel> slides = new List<Panel>();
        private int currentIndex = 0;
        private Panel nextPanel;

        public Introduction()
        {
            InitializeComponent();

            label4.Parent = pictureBox3;
            label4.BackColor = Color.Transparent;
            label4.Location = CenterToParent;
        }

        void SetupSlides()
        {
            slides.Add(ChildPanel1);
            slides.Add(ChildPanel2);
            slides.Add(ChildPanel3);

            foreach (var s in slides)
                s.Visible = false;

            slides[currentIndex].Visible = true;
        }

        void StartSlides(int nextIndex)
        {
            nextPanel = slides[nextIndex];
            nextPanel.Visible = true;

            currentIndex = nextIndex;
        }

        void NextButton()
        {
            int nextIndex = currentIndex + 1;
            StartSlides(nextIndex);
        }
        void PrevButton()
        {
            int nextIndex = currentIndex + 1;
            StartSlides(nextIndex);
        }
    }
}
