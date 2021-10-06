using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImpossibleGame
{
    public partial class GameoverScreen : UserControl
    {
        bool bDown;

        public GameoverScreen()
        {
            InitializeComponent();
        }

        private void GameoverScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.B:
                    bDown = true;
                    break;
            }
        }

        private void GameoverScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.B:
                    bDown = false;
                    break;
            }
        }

        private void gameoverLoop_Tick(object sender, EventArgs e)
        {
            outputLabel.Text = $"You Died at {Form1.timeCount / 50} seconds";       //Timer Score

            if (bDown)      //Replay
            {
                Form f = this.FindForm();
                f.Controls.Remove(this);

                GameScreen gs = new GameScreen();
                f.Controls.Add(gs);

                gs.Location = new Point((f.Width - gs.Width) / 2 - 8, (f.Height - gs.Height) / 2 - 19);
                bDown = false;
            }
        }
    }
}
