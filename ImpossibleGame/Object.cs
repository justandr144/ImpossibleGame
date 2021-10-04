using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImpossibleGame
{
    class Object
    {
        public int x, y, size, speed;
        public SolidBrush brushColour;

        public Object(int _x, int _y, int _size, int _speed, SolidBrush _brushColor)
        {
            x = _x;
            y = _y;
            size = _size;
            speed = _speed;
            brushColour = _brushColor;
        }

        public void Move(string direction)
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
    }
}
