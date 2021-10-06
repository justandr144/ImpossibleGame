/*October 2021
 * Justin Andrews
 * A single level of the impossible game
 */

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
    public partial class Form1 : Form
    {
        public static double timeCount = 0;

        public Form1()
        {
            InitializeComponent();
            Cursor.Hide();
        }

        private void Form1_Load(object sender, EventArgs e) //Add a centered main screen
        {
            MainScreen ms = new MainScreen();
            this.Controls.Add(ms);

            ms.Location = new Point((this.Width - ms.Width) / 2 - 8, (this.Height - ms.Height) / 2 - 19);
        }
    }
}
