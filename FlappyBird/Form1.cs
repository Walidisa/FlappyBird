using System;
using System.Drawing;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        int birdX = 82;
        float birdY = 145;
        int pipeUpX = -115;
        int pipeDownX = 280;
        int pipe1Y = 316;
        int pipe2Y = 584;
        int pipe3Y = 852;
        int score = 0;
        float birdVelocity = 0;
        float gravity = 0.5f;
        private System.Windows.Forms.Timer gameTimer = null!;
        public Form1()
        {
            InitializeComponent();
            InitializeGameTimer();
            this.DoubleBuffered = true;
            pictureBox4.Invalidate();
            this.Invalidate();
            this.Update();
            this.KeyPreview = true;

        }
        private void InitializeGameTimer()
        {
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 10;
            gameTimer.Tick += GameTimer_Tick;
        }

        private void pipeMovement()
        {
            Random rand1 = new Random();
            Random rand2 = new Random();
            Random rand3 = new Random();
            int pipeHeight1 = rand1.Next(-65, 70);
            int pipeHeight2 = rand2.Next(-65, 70);
            int pipeHeight3 = rand3.Next(-65, 70);

            pipeDown1.Left -= 5;
            pipeUp1.Left -= 5;
            pipeDown2.Left -= 5;
            pipeUp2.Left -= 5;
            pipeDown3.Left -= 5;
            pipeUp3.Left -= 5;
            space1.Left -= 5;
            space2.Left -= 5;
            space3.Left -= 5;

            if (pipeDown1.Right < 0)
            {
                pipeDown1.Left = this.ClientSize.Width;
                pipeUp1.Left = this.ClientSize.Width;
                space1.Left = this.ClientSize.Width;
                if (pipeUp1.Top <= -180 || pipeDown1.Top >= 370)
                {
                    pipeHeight1 = 0;
                }
                else
                {
                    pipeUp1.Top += pipeHeight1;
                    pipeDown1.Top += pipeHeight1;
                    space1.Top += pipeHeight1;
                }

            }
            if (pipeDown2.Right < 0)
            {
                pipeDown2.Left = this.ClientSize.Width;
                pipeUp2.Left = this.ClientSize.Width;
                space2.Left = this.ClientSize.Width;
                if (pipeUp2.Top <= -180 || pipeDown2.Top >= 370)
                {
                    pipeHeight2 = 0;
                }
                else
                {
                    pipeUp2.Top += pipeHeight2;
                    pipeDown2.Top += pipeHeight2;
                    space2.Top += pipeHeight2;
                }
            }
            if (pipeDown3.Right < 0)
            {
                pipeDown3.Left = this.ClientSize.Width;
                pipeUp3.Left = this.ClientSize.Width;
                space3.Left = this.ClientSize.Width;
                if (pipeUp3.Top <= -180 || pipeDown3.Top >= 370)
                {
                    pipeHeight3 = 0;
                }
                else
                {
                    pipeUp3.Top += pipeHeight3;
                    pipeDown3.Top += pipeHeight3;
                    space3.Top += pipeHeight3;
                }
            }   

        }
        private void GameOver()
        {
            if (birdY >= 380)
            {
                gameTimer.Stop();
                MessageBox.Show($"Game Over!! Your Score was: {score/11}");                
                ResetGame();
            }
            if (pictureBox4.Bounds.IntersectsWith(pipeUp1.Bounds) || pictureBox4.Bounds.IntersectsWith(pipeDown1.Bounds) || pictureBox4.Bounds.IntersectsWith(pipeUp2.Bounds) || pictureBox4.Bounds.IntersectsWith(pipeDown2.Bounds) || pictureBox4.Bounds.IntersectsWith(pipeUp3.Bounds) || pictureBox4.Bounds.IntersectsWith(pipeDown3.Bounds))
            {
                gameTimer.Stop();
                MessageBox.Show($"Game Over!! Your Score was: {score/11}");

                ResetGame();
            }
        }
        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            pipeMovement();
            birdVelocity += gravity;
            birdY += birdVelocity;
            pictureBox4.Location = new Point(birdX, (int)birdY);
            if (birdY <= 0)
            {
                birdY = 0;
                birdVelocity = 0;
            }
            if(pictureBox4.Bounds.IntersectsWith(space1.Bounds) || pictureBox4.Bounds.IntersectsWith(space2.Bounds) || pictureBox4.Bounds.IntersectsWith(space3.Bounds))
            {
                score += 1;
            }
            this.Text = $"Y: {(int)birdY}, Vel: {birdVelocity} , Score: {score/11}";

            GameOver();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BringToFront();

            pipeDown1.BackColor = Color.Transparent;
            pipeUp1.BackColor = Color.Transparent;
            pipeDown2.BackColor = Color.Transparent;
            pipeUp2.BackColor = Color.Transparent;
            pipeDown3.BackColor = Color.Transparent;
            pipeUp3.BackColor = Color.Transparent;
            space1.BackColor = Color.Transparent;
            space2.BackColor = Color.Transparent;
            space3.BackColor = Color.Transparent;


            pictureBox4.BackColor = Color.Transparent;
            pictureBox6.BackColor = Color.Transparent;
            this.BackColor = Color.White;
            //pictureBox4.Parent = this;

        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Space)
            {
                HandleSpacebarPress();
            }
        }
 

        private void HandleSpacebarPress()
        {
            birdVelocity = -5;
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            gameTimer.Start();
        }
        private void ResetGame()
        {
            birdX = 82;
            birdY = 145;
            pipeUp1.Location = new Point(pipe1Y, pipeUpX);
            pipeDown1.Location = new Point(pipe1Y, pipeDownX);
            pipeUp2.Location = new Point(pipe2Y, pipeUpX);
            pipeDown2.Location = new Point(pipe2Y, pipeDownX);
            pipeUp3.Location = new Point(pipe3Y, pipeUpX);
            pipeDown3.Location = new Point(pipe3Y, pipeDownX);
            birdVelocity = 0;
            score = 0;
            pictureBox4.Location = new Point(birdX, (int)birdY);
            panel1.Visible = true;
        }
    }
}
