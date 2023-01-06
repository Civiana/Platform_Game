using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatForm_Game
{
    public partial class Form1 : Form
    {
        bool moveLeft, moveRight, Up, isGameOver;

        int jumpVelocity, force, points = 0, playerVelocity = 7;
        int ySpeed = 3,  xSpeed = 5, NPC1Speed = 5, NPC2Speed = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtPoints.Text = "Points: " + points;
            player.Top += jumpVelocity;

            if (moveLeft == true)
            {
                player.Left -= playerVelocity;
            }
            if (moveRight == true)
            {
                player.Left += playerVelocity;
            }

            if (Up == true && force < 0)
            {
                Up = false;
            }

            if (Up == true)
            {
                jumpVelocity = -8;
                force -= 1;
            }
            else
            {
                jumpVelocity = 10;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            if ((string)x.Name == "horizontalPlatform" && moveLeft == false || (string)x.Name == "horizontalPlatform" && moveRight == false)
                            {
                                player.Left -= xSpeed;
                            }

                        }

                        x.BringToFront();
                    }

                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            points++;
                        }
                    }

                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtPoints.Text = "Points: " + points + Environment.NewLine + "You were killed in your journey!!";
                        }
                    }
                }
            }

            xPlatform.Left -= xSpeed;

            if (xPlatform.Left < 0 || xPlatform.Left + xPlatform.Width > this.ClientSize.Width)
            {
                xSpeed = -xSpeed;
            }

            yPlatform.Top += ySpeed;

            if (yPlatform.Top < 195 || yPlatform.Top > 581)
            {
                ySpeed = -ySpeed;
            }

            NPC1.Left -= NPC1Speed;

            if (NPC1.Left < pictureBox5.Left || NPC1.Left + NPC1.Width > pictureBox5.Left + pictureBox5.Width)
            {
                NPC1Speed = -NPC1Speed;
            }

            NPC2.Left += NPC2Speed;

            if (NPC2.Left < pictureBox2.Left || NPC2.Left + NPC2.Width > pictureBox2.Left + pictureBox2.Width)
            {
                NPC2Speed = -NPC2Speed;
            }

            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtPoints.Text = "Points: " + points + Environment.NewLine + "You fell to your death!";
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && points == 26)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtPoints.Text = "Points: " + points + Environment.NewLine + "Your quest is complete!";
            }
            else
            {
                txtPoints.Text = "Points: " + points + Environment.NewLine + "Collect all the coins";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }
            if (e.KeyCode == Keys.Space && Up == false)
            {
                Up = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }
            if (Up == true)
            {
                Up = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }

        }

        private void RestartGame()
        {
            Up = false;
            moveLeft = false;
            moveRight = false;
            isGameOver = false;
            points = 0;

            txtPoints.Text = "Points: " + points;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            player.Left = 72;
            player.Top = 656;

            NPC1.Left = 471;
            NPC2.Left = 360;

            xPlatform.Left = 275;
            yPlatform.Top = 581;

            gameTimer.Start();

        }
    }
}