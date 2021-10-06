using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace ImpossibleGame
{
    public partial class GameScreen : UserControl
    {
        #region variables
        bool upArrowDown, downArrowDown, leftArrowDown, rightArrowDown;

        Object player;
        Object finish;
        Object start;

        SolidBrush playerBrush = new SolidBrush(Color.Red);
        SolidBrush wallBrush = new SolidBrush(Color.Silver);
        SolidBrush finishBrush = new SolidBrush(Color.Green);
        SolidBrush enemyBrush = new SolidBrush(Color.Blue);

        Object[] walls = new Object[6];
        Object[] enemies = new Object[8];
        Rectangle[] borders = new Rectangle[6];

        Rectangle finishBox;

        SoundPlayer death = new SoundPlayer(Properties.Resources.deathNoise);
        SoundPlayer victory = new SoundPlayer(Properties.Resources.victory);
        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart();

            Form1.timeCount = 0;        //Seperated for easier use of the timer system
            gameLoop.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        } //Key presses

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        } //Key presses

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            Form1.timeCount++;

            if (upArrowDown) //Movement of player
            {
                player.Move("up");
            }
            if (downArrowDown)
            {
                player.Move("down");
            }
            if (leftArrowDown)
            {
                player.Move("left");
            }
            if (rightArrowDown)
            {
                player.Move("right");
            }

            Rectangle playerBoxTop = new Rectangle(player.x, player.y, player.width, 5);    //Hitboxes on each side of player
            Rectangle playerBoxRight = new Rectangle(player.x + player.width - 5, player.y, 5, player.height);
            Rectangle playerBoxLeft = new Rectangle(player.x, player.y, 5, player.height);
            Rectangle playerBoxBottom = new Rectangle(player.x, player.y + player.height - 5, player.width, 5);

            if (playerBoxRight.IntersectsWith(finishBox)) //Change to victory screen upon reaching finish
            {
                victory.Play();

                gameLoop.Enabled = false;
                
                Form f = this.FindForm();
                f.Controls.Remove(this);

                VictoryScreen vs = new VictoryScreen();
                f.Controls.Add(vs);
                vs.Focus();

                vs.Location = new Point((f.Width - vs.Width) / 2 - 8, (f.Height - vs.Height) / 2 - 19);
                finishBox.Height = 2;
            }

            foreach (Rectangle b in borders) //Preventing player from leaving area
            {
                if (b.IntersectsWith(playerBoxTop))
                {
                    player.y += 7;
                }
                if (b.IntersectsWith(playerBoxRight))
                {
                    player.x -= 7;
                }
                if (b.IntersectsWith(playerBoxLeft))
                {
                    player.x += 7;
                }
                if (b.IntersectsWith(playerBoxBottom))
                {
                    player.y -= 7;
                }
            }

            foreach (Object n in enemies)   //Movement of enemies and collisions with enemies and player
            {
                n.y += n.speed;

                if (player.Collision(n))
                {
                    death.Play();

                    gameLoop.Enabled = false;

                    Form f = this.FindForm();
                    f.Controls.Remove(this);

                    GameoverScreen gos = new GameoverScreen();
                    f.Controls.Add(gos);
                    gos.Focus();

                    gos.Location = new Point((f.Width - gos.Width) / 2 - 8, (f.Height - gos.Height) / 2 - 19);

                    OnStart(); //moves the hitboxes and prevents it from attempting this if statement twice
                }

                if (n.y > 345 || n.y < 79)
                {
                    n.speed *= -1;
                }
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)  //painting all objects on screen
        {
            for (int i = 0; i < walls.Count(); i++)
            {
                e.Graphics.FillRectangle(walls[i].brushColour, walls[i].x, walls[i].y, walls[i].width, walls[i].height);
            }

            for (int i = 0; i < enemies.Count(); i++)
            {
                e.Graphics.FillEllipse(enemies[i].brushColour, enemies[i].x, enemies[i].y, enemies[i].width, enemies[i].height);
            }

            e.Graphics.FillRectangle(finish.brushColour, finish.x, finish.y, finish.width, finish.height);
            e.Graphics.FillRectangle(start.brushColour, start.x, start.y, start.width, start.height);

            e.Graphics.FillRectangle(player.brushColour, player.x, player.y, player.width, player.height);
        }

        private void OnStart() //Only exists to make the initializing look neater
        {
            player = new Object(55, 210, 30, 30, 7, playerBrush);   //Setting up all the locations of objects

            finish = new Object(680, 75, 100, 300, 0, finishBrush);
            start = new Object(20, 75, 100, 300, 0, finishBrush);

            walls[0] = new Object(0, 0, 20, 450, 0, wallBrush);
            walls[1] = new Object(11, 0, 779, 75, 0, wallBrush);
            walls[2] = new Object(11, 375, 779, 75, 0, wallBrush);
            walls[3] = new Object(780, 0, 20, 450, 0, wallBrush);
            walls[4] = new Object(120, 21, 30, 300, 0, wallBrush);
            walls[5] = new Object(650, 125, 30, 300, 0, wallBrush);

            enemies[0] = new Object(176, 350, 25, 25, -8, enemyBrush);
            enemies[1] = new Object(236, 74, 25, 25, 8, enemyBrush);
            enemies[2] = new Object(296, 350, 25, 25, -8, enemyBrush);
            enemies[3] = new Object(356, 74, 25, 25, 8, enemyBrush);
            enemies[4] = new Object(416, 350, 25, 25, -8, enemyBrush);
            enemies[5] = new Object(476, 74, 25, 25, 8, enemyBrush);
            enemies[6] = new Object(536, 350, 25, 25, -8, enemyBrush);
            enemies[7] = new Object(596, 74, 25, 25, 8, enemyBrush);

            for (int i = 0; i < walls.Count(); i++)     //Border hitboxes
            {
                borders[i] = new Rectangle(walls[i].x + 3, walls[i].y, walls[i].width, walls[i].height);
            }

            finishBox = new Rectangle(finish.x, finish.y, finish.width, finish.height);     //Finish line hitbox
        }
    }
}
