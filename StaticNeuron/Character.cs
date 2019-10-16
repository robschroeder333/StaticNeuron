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
        Pieces[,] screen;

        public Character(int width, int height, Pieces[,] screen)
        {
            this.screen = screen;
            this.height = height;
            this.width = width;
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
                        for (int x = -y; x <= y; x++)
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
                    for (int x = -1; x >= -3; x--)
                    {
                        for (int y = x; y <= -x; y++)
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
                    for (int x = 1; x <= 3; x++)
                    {
                        for (int y = -x; y <= x; y++)
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
            if (x <= (120 - 2) && x > 0 && y <= (30 - 2) && y > 0)
            {
                return true;
            }
            return false;
        }

         
         public void Move()
         {
             bool isTurnOver = false;
             int newPositionX;
             int newPositionY;

            while (!isTurnOver) // each iteration of this inner loop is the placement of an "X" or a "Y as a single move
                {
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true); 
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.RightArrow:
                            newPositionX = Position.X + 1;
                            if (((WithinBounds(newPositionX, Position.Y)) && screen[newPositionX, Position.Y] != Pieces.Player) || ((WithinBounds(newPositionX, Position.Y)) && screen[newPositionX, Position.Y] != Pieces.Wall))
                            {
                                Position = new Point (newPositionX, Position.Y);
                                dir = Direction.Right;
                                isTurnOver = true;
                                SetVision();
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            newPositionX = Position.X - 1;
                            if (((WithinBounds(newPositionX, Position.Y)) && screen[newPositionX, Position.Y] != Pieces.Player) || ((WithinBounds(newPositionX, Position.Y)) && screen[newPositionX, Position.Y] != Pieces.Wall))
                            {
                                Position = new Point (newPositionX, Position.Y);
                                dir = Direction.Left;
                                isTurnOver = true;
                                SetVision();
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            newPositionY = Position.Y + 1;
                            if (((WithinBounds(Position.X, newPositionY)) && screen[Position.X, newPositionY] != Pieces.Player) || ((WithinBounds(Position.X, newPositionY)) && screen[Position.X, newPositionY] != Pieces.Wall))
                            {
                                Position = new Point (Position.X, newPositionY);
                                dir = Direction.Down;
                                isTurnOver = true;
                                SetVision();
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            newPositionY = Position.Y - 1; 
                            if (((WithinBounds(Position.X, newPositionY)) && screen[Position.X, newPositionY] != Pieces.Player) || ((WithinBounds(Position.X, newPositionY)) && screen[Position.X, newPositionY] != Pieces.Wall))
                            {
                                Position = new Point (Position.X, newPositionY);
                                dir = Direction.Up;
                                isTurnOver = true;
                                SetVision();
                            }
                            break;

                    }

                }
         }
    }
}
