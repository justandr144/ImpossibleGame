using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImpossibleGame
{
    class Object        //Basic name to fit all objects used in game
    {
        public int x, y, width, height, speed;
        public SolidBrush brushColour;

        public Object(int _x, int _y, int _width, int _height, int _speed, SolidBrush _brushColor)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            speed = _speed;
            brushColour = _brushColor;
        }

        public void Move(string direction)      //Movement of player
        {
            if (direction == "left")
            {
                x -= speed;
            }
            else if (direction == "right")
            {
                x += speed;
            }
            else if (direction == "up")
            {
                y -= speed;
            }
            else if (direction == "down")
            {
                y += speed;
            }
        }

        public bool Collision (Object n)        //Collision of player and enemies
        {
            Rectangle playerRec = new Rectangle(x, y, width, height);
            Rectangle enemyRec = new Rectangle(n.x, n.y, n.width, n.height);

            if (playerRec.IntersectsWith(enemyRec))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
