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
    public partial class Intro : UserControl
    {
        private List<Panel> slides = new List<Panel>();
        private int currentIndex = 0;
        private bool isSliding = false;
        private int slideSpeed = 300;
        private int slideDirection = 0;
        private Panel nextPanel;
        private List<Label> dotIndicators = new List<Label>();

        public Intro()
        {
            InitializeComponent();
            SetupSlides();
            UpdateButtonVisibility();
            CreateDotIndicators();
            UpdateDotIndicators();

            label4.Parent = pictureBox3;
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(
                (pictureBox3.Width - label4.Width) / 2,
                (pictureBox3.Height - label4.Height) / 2
            );
        }

        private void CreateDotIndicators()
        {
            FlowLayoutPanel dotPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = false,
                WrapContents = false,
                Padding = new Padding(0, 5, 0, 0),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Bottom,
            };

            dotPanel.Width = MainPanel.Width;
            dotPanel.Left = (this.Width - dotPanel.Width) / 2;
            dotPanel.BackColor = Color.Transparent;
            dotPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(dotPanel);

            for (int i = 0; i < slides.Count; i++)
            {
                Label dot = new Label
                {
                    AutoSize = false,
                    Width = 15,
                    Height = 15,
                    Margin = new Padding(5, 0, 5, 0),
                    BackColor = Color.Gray,
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = i,
                    Cursor = Cursors.Hand
                };

                dot.Click += Dot_Click;
                dotIndicators.Add(dot);
                dotPanel.Controls.Add(dot);
            }
        }

        private void Dot_Click(object sender, EventArgs e)
        {
            if (isSliding) return;

            Label clickedDot = sender as Label;
            int index = (int)clickedDot.Tag;

            if (index != currentIndex)
            {
                int direction = (index > currentIndex) ? 1 : -1;
                StartSlides(index, direction);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            foreach (var p in slides)
            {
                if (p.Visible)
                    p.Left -= slideSpeed * slideDirection;
            }

            if ((slideDirection == 1 && nextPanel.Left <= 0) ||
                (slideDirection == -1 && nextPanel.Left >= 0))
            {
                timer1.Stop();

                foreach (var p in slides)
                {
                    if (p != nextPanel)
                    {
                        p.Visible = false;
                        p.Left = 0;
                    }
                }

                nextPanel.Left = 0;
                isSliding = false;
            }
        }

        private void UpdateDotIndicators()
        {
            DotPanel.Controls.Clear();

            for (int i = 0; i < slides.Count; i++)
            {
                Label dot = new Label();
                dot.AutoSize = false;
                dot.Width = 10;
                dot.Height = 10;
                dot.Margin = new Padding(3);
                dot.BackColor = (i == currentIndex) ? Color.DodgerBlue : Color.Gray;
                dot.BorderStyle = BorderStyle.FixedSingle;
                dot.Cursor = Cursors.Hand;

                int index = i;
                dot.Click += (s, e) =>
                {
                    if (!isSliding && index != currentIndex)
                        StartSlides(index, index > currentIndex ? 1 : -1);
                };

                DotPanel.Controls.Add(dot);
            }
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

        void StartSlides(int next, int dir)
        {
            if (isSliding) return;

            isSliding = true;
            slideDirection = dir;
            nextPanel = slides[next];
            nextPanel.Visible = true;

            nextPanel.Left = (dir == 1) ? MainPanel.Width : -MainPanel.Width;
            nextPanel.Top = 0;

            timer1.Start();

            currentIndex = next;
            UpdateDotIndicators();
        }

        void UpdateButtonVisibility()
        {
            if (currentIndex < slides.Count - 1)
            {
                parrotButton1.Visible = true;
                parrotButton1.ButtonText = "Lanjut";
                parrotButton1.Click -= MulaiButton;
                parrotButton1.Click += NextButton;
            }
            else
            {
                parrotButton1.ButtonText = "Mulai";
                parrotButton1.Click += MulaiButton;
            }
            parrotButton2.Visible = currentIndex > 0;
        }

        void NextButton(object sender, EventArgs e)
        {
            int nextIndex = currentIndex + 1;
            if (nextIndex < slides.Count)
            {
                StartSlides(nextIndex, 1);
                UpdateButtonVisibility();
            }
        }

        void MulaiButton(object sender, EventArgs e)
        {
            Form main = this.FindForm();

            Panel target = main.Controls["panel1"] as Panel;

            target.Dispose();
        }

        void PrevButton(object sender, EventArgs e)
        {
            int prevIndex = currentIndex - 1;
            if (prevIndex >= 0)
            {
                StartSlides(prevIndex, -1);
                UpdateButtonVisibility();
            }
        }
    }
}
