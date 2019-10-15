using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace StaticNeuron
{
    public enum Direction { Up, Down, Left, Right }
    class Character
    {
        Direction dir;
        public Point Position { get; private set; }
        public Point[] Vision { get; private set; }

        public Character()
        {
            Position = new Point(3, 3);
            dir = Direction.Up;
            Vision = new Point[15];
            SetVision();
        }

        public void IsDead()
        {

        }

        public void SetVision()
        {
            int index = 0;
            switch (dir)
            {
                case Direction.Up:
                    for (int y = -1; y <= -3; y--)
                    {
                        for (int x = y; x <= -y; x++)
                        {
                            Vision[index] = new Point(Position.X + x, Position.Y + y);
                            index++;
                        }
                    }
                    break;
                case Direction.Down:
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
                default:
                    break;
            }
        }
    }
}
