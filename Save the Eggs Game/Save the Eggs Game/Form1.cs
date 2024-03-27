using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Save_the_Eggs_Game
{
    public partial class Form1 : Form
    {

        bool goLeft, goRight;

        int speed = 8;
        int score = 0;
        int missed = 0;

        Random randX = new Random();
        Random randY = new Random();

        PictureBox splash = new PictureBox();



        public Form1()
        {
            InitializeComponent();
            RestartGame();  
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            if (missed >=6)
            {
                GameTimer.Stop();
                MessageBox.Show("Game Over!" + Environment.NewLine + "We've lost good Eggs!" + Environment.NewLine + "Click ok to retry");
                RestartGame();
            }

            if (goLeft == true && player.Left > 0)
            {
                player.Left -= 12;
                player.Image = Properties.Resources.chicken_normal2;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += 12;
                player.Image = Properties.Resources.chicken_normal;
            }

            txtScore.Text = "Saved: " + score;
            txtMiss.Text = "Missed: " + missed;

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "eggs")
                {

                    x.Top += speed;

                    if (x.Top + x.Height > this.ClientSize.Height)
                    {
                        splash.Image = Properties.Resources.splash; splash.Location = x.Location; splash.Height = 60; splash.Width = 60; splash.BackColor = Color.Transparent;

this.Controls.Add(splash);

                        x.Top = -x.Height; // Изменение координаты Top при достижении нижней границы
                        x.Left = randX.Next(6, this.ClientSize.Width - x.Width);
                        missed += 1;
                        player.Image = Properties.Resources.chicken_hurt;
                    }

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(6, this.ClientSize.Width - x.Width);
                        score += 1;
                    }
                }
            }
            if (missed >= 6)
            {
                txtMiss.Text = "Missed: " + 6;
                GameTimer.Stop();
                MessageBox.Show("Game Over!" + Environment.NewLine + "We've lost good Eggs!" + Environment.NewLine + "Click ok to retry");
                RestartGame();
                return;
            }
            if (score > 10)
            {
                speed = 12;
            }
        }

       

            GameTimer.Start();

        }
    }
}
