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
        int height;
        int width;

        public Character(int height, int width)
        {
            this.height = height;
            this.width = width;
            Position = new Point(10, 10);
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
                    for (int y = -1; y >= -3; y--)
                    {
                        for (int x = y; x <= -y; x++)
                        {
                            if (WithinBounds(Position.X + x, Position.Y + y))
                                Vision[index] = new Point(Position.X + x, Position.Y + y);
                            else
                                Vision[index] = new Point(-1, -1);

                            index++;
                        }
                    }
                    break;
                case Direction.Down:
                    for (int y = 1; y <= 3; y++)
                    {
                        for (int x = y; x <= -y; x++)
                        {
                            if (WithinBounds(Position.X + x, Position.Y + y))
                                Vision[index] = new Point(Position.X + x, Position.Y + y);
                            else
                                Vision[index] = new Point(-1, -1);

                            index++;
                        }
                    }
                    break;
                case Direction.Left:
                    for (int x = 1; x <= 3; x++)
                    {
                        for (int y = x; y <= -y; y++)
                        {
                            if (WithinBounds(Position.X + x, Position.Y + y))
                                Vision[index] = new Point(Position.X + x, Position.Y + y);
                            else
                                Vision[index] = new Point(-1, -1);

                            index++;
                        }
                    }
                    break;
                case Direction.Right:
                    for (int x = -1; x >= -3; x--)
                    {
                        for (int y = x; y <= -y; y++)
                        {
                            if (WithinBounds(Position.X + x, Position.Y + y))
                                Vision[index] = new Point(Position.X + x, Position.Y + y);
                            else
                                Vision[index] = new Point(-1, -1);

                            index++;
                        }
                    }
                    break;
                default:
                    //do nothing
                    break;
            }
        }
    
        bool WithinBounds(int x, int y)
        {
            if (x <= height - 2 && x > 0 && y <= height -2 && y > 0)
            {
                return true;
            }
            return false;
        }
    }
}
