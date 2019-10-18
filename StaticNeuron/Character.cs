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
        public int Actions { get; set; } = 1000;

        public Character()
        {
            Position = new Point(10, 10);
            dir = Direction.Right;
            Vision = new Point[15];
            SetVision();
        }

        public void IsDead()
        {

        }

        public void SetVision()
        {
            int index = 0;
            Vision = new Point[15];
            switch (dir)
            {
                case Direction.Up:
                    for (int y = -1; y >= -3; y--)
                    {
                        for (int x = y; x <= -y; x++)
                        {
                            OcclusionChecker(x, y, index);
                            index++;
                        }
                    }
                    break;
                case Direction.Down:
                    for (int y = 1; y <= 3; y++)
                    {
                        for (int x = -y; x <= y; x++)
                        {
                            OcclusionChecker(x, y, index);
                            index++;
                        }
                    }
                    break;
                case Direction.Left:
                    for (int x = -1; x >= -3; x--)
                    {
                        for (int y = x; y <= -x; y++)
                        {
                            OcclusionChecker(x, y, index);
                            index++;
                        }
                    }
                    break;
                case Direction.Right:
                    for (int x = 1; x <= 3; x++)
                    {
                        for (int y = -x; y <= x; y++)
                        {
                            OcclusionChecker(x, y, index);
                            index++;
                        }
                    }
                    break;
                default:
                    //do nothing
                    break;
            }
        }

        public void Occlusion(int n)
        {
            if (n == 0)
            {
                Vision[3] = new Point(-1, -1);
                Vision[8] = new Point(-1, -1);
            }
            if (n == 1)
            {
                Vision[5] = new Point(-1, -1);
                Vision[10] = new Point(-1, -1);
                Vision[11] = new Point(-1, -1);
                Vision[12] = new Point(-1, -1);
            }
            if (n == 2)
            {
                Vision[7] = new Point(-1, -1);
                Vision[14] = new Point(-1, -1);
            }
            if (n == 3)
            {
                Vision[8] = new Point(-1, -1);
            }
            if (n == 4)
            {
                Vision[9] = new Point(-1, -1);
                Vision[10] = new Point(-1, -1);
            }
            if (n == 5)
            {
                Vision[11] = new Point(-1, -1);
            }
            if (n == 6)
            {
                Vision[12] = new Point(-1, -1);
                Vision[13] = new Point(-1, -1);
            }
            if (n == 7)
            {
                Vision[14] = new Point(-1, -1);
            }
        }

        void OcclusionChecker(int x, int y, int index)
        {
            if (WithinBounds(Position.X + x, Position.Y + y))
            {
                if (Game.screen[Position.X + x, Position.Y + y] == Pieces.Wall
                    && Vision[index].X != -1)
                {
                    Vision[index] = new Point(Position.X + x, Position.Y + y);
                    Occlusion(index);
                }
                else if (Vision[index].X != -1)
                    Vision[index] = new Point(Position.X + x, Position.Y + y);
            }
            else
                Vision[index] = new Point(-1, -1);
        }

        bool WithinBounds(int x, int y)
        {
            //TODO: check if using width and height can work without error
            if (x <= (Program.width - 2) && x > 0 && y <= (Program.height - 2) && y > 0)
            {
                return true;
            }
            return false;
        }

         
         public void Move(ConsoleKey key)
         {
             int newPositionX;
             int newPositionY;

// each iteration of this inner loop is the placement of an "X" or a "Y as a single move
                    switch (key)
                    {
                        case ConsoleKey.RightArrow:
                            newPositionX = Position.X + 1;

                            Console.WriteLine(Game.screen[newPositionX, Position.Y]);

                            if (WithinBounds(newPositionX, Position.Y) 
                                && Game.screen[newPositionX, Position.Y] != Pieces.Player 
                                && Game.screen[newPositionX, Position.Y] != Pieces.Wall)
                            {
                                Position = new Point (newPositionX, Position.Y);
                                dir = Direction.Right;
                                Actions--;
                                SetVision();
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            newPositionX = Position.X - 1;
                            if (WithinBounds(newPositionX, Position.Y) 
                                && Game.screen[newPositionX, Position.Y] != Pieces.Player 
                                && Game.screen[newPositionX, Position.Y] != Pieces.Wall)
                            {
                                Position = new Point (newPositionX, Position.Y);
                                dir = Direction.Left;
                                Actions--;
                                SetVision();
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            newPositionY = Position.Y + 1;
                            if (WithinBounds(Position.X, newPositionY) 
                                && Game.screen[Position.X, newPositionY] != Pieces.Player 
                                && Game.screen[Position.X, newPositionY] != Pieces.Wall)
                            {
                                Position = new Point (Position.X, newPositionY);
                                dir = Direction.Down;
                                Actions--;
                                SetVision();
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            newPositionY = Position.Y - 1; 
                            if (WithinBounds(Position.X, newPositionY) 
                                && Game.screen[Position.X, newPositionY] != Pieces.Player
                                && Game.screen[Position.X, newPositionY] != Pieces.Wall)
                            {
                                Position = new Point (Position.X, newPositionY);
                                dir = Direction.Up;
                                Actions--;
                                SetVision();
                            }
                            break;

                }
         }
    }
}
