using System.Security;
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
        public int LightLevel { get; set; } = 0;
        public Point Position { get; set; }
        public Point[] Vision { get; private set; }

        public Character(int x, int y)
        {
            Position = new Point(x, y);
            dir = Direction.Right;
            Vision = new Point[(int)(Math.Pow(LightLevel + 1, 2)) - 1];
            SetVision();
        }

        public void SetVision()
        {
            int index = 0;
            Vision = LightLevel != 0 ? new Point[(int)(Math.Pow(LightLevel + 1, 2)) - 1] : new Point[0];
            switch (dir)
            {
                case Direction.Up:
                    for (int y = -1; y >= -LightLevel; y--)
                    {
                        for (int x = y; x <= -y; x++)
                        {
                            OcclusionChecker(x, y, index);
                            index++;
                        }
                    }
                    if (Game.screen[Position.X, Position.Y - 1] == Pieces.Wall)
                    {
                        if (Game.screen[Position.X - 1, Position.Y] == Pieces.Wall)
                        {
                            Vision[0] = new Point (-1,-1);
                        }
                        if (Game.screen[Position.X + 1, Position.Y] == Pieces.Wall)
                        {
                            Vision[2] = new Point (-1,-1);
                        }
                    }
                    break;
                case Direction.Down:
                    for (int y = 1; y <= LightLevel; y++)
                    {
                        for (int x = -y; x <= y; x++)
                        {
                            OcclusionChecker(x, y, index);
                            index++;
                        }
                    }
                    if (Game.screen[Position.X, Position.Y + 1] == Pieces.Wall)
                    {
                        if (Game.screen[Position.X + 1, Position.Y] == Pieces.Wall)
                        {
                            Vision[2] = new Point (-1,-1);
                        }
                        if (Game.screen[Position.X - 1, Position.Y] == Pieces.Wall)
                        {
                            Vision[0] = new Point (-1,-1);
                        }
                    }
                    break;
                case Direction.Left:
                    for (int x = -1; x >= -LightLevel; x--)
                    {
                        for (int y = x; y <= -x; y++)
                        {
                            OcclusionChecker(x, y, index);
                            index++;
                        }
                    }
                    if (Game.screen[Position.X - 1, Position.Y] == Pieces.Wall)
                    {
                        if (Game.screen[Position.X, Position.Y - 1] == Pieces.Wall)
                        {
                            Vision[0] = new Point (-1,-1);
                        }
                        if (Game.screen[Position.X, Position.Y + 1] == Pieces.Wall)
                        {
                            Vision[2] = new Point (-1,-1);
                        }
                    }
                    break;
                case Direction.Right:
                    for (int x = 1; x <= LightLevel; x++)
                    {
                        for (int y = -x; y <= x; y++)
                        {
                            OcclusionChecker(x, y, index);
                            index++;
                        }
                    }
                     if (Game.screen[Position.X + 1, Position.Y] == Pieces.Wall)
                    {
                        if (Game.screen[Position.X, Position.Y - 1] == Pieces.Wall)
                        {
                            Vision[0] = new Point (-1,-1);
                        }
                        if (Game.screen[Position.X, Position.Y + 1] == Pieces.Wall)
                        {
                            Vision[2] = new Point (-1,-1);
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
                Vision[4] = new Point(-1, -1);
                if (LightLevel > 2)
                {
                    Vision[8] = new Point(-1, -1);
                    Vision[9] = new Point(-1, -1);
                }
                
                if (LightLevel > 3)
                {
                    Vision[15] = new Point (-1, -1);
                    Vision[16] = new Point (-1, -1);
                }
            }
            if (n == 1)
            {
                Vision[4] = new Point (-1, -1);
                Vision[5] = new Point(-1, -1);
                Vision[6] = new Point (-1, -1);
                if (LightLevel > 2)
                {
                    Vision[9] = new Point (-1, -1);
                    Vision[10] = new Point(-1, -1);
                    Vision[11] = new Point(-1, -1);
                    Vision[12] = new Point(-1, -1);
                    Vision[13] = new Point (-1, -1);
                }
                if (LightLevel > 3)
                {
                    Vision[16] = new Point(-1, -1);
                    Vision[17] = new Point(-1, -1);
                    Vision[18] = new Point(-1, -1);
                    Vision[19] = new Point(-1, -1);
                    Vision[20] = new Point(-1, -1);
                    Vision[21] = new Point(-1, -1);
                    Vision[22] = new Point(-1, -1);
                }                    
            }
            if (n == 2)
            {
                Vision[6] = new Point(-1, -1);
                Vision[7] = new Point(-1, -1);
                
                if (LightLevel > 2)
                {
                    Vision[13] = new Point(-1, -1);
                    Vision[14] = new Point(-1, -1);
                }

                if (LightLevel > 3)
                {
                    Vision[22] = new Point (-1, -1);
                    Vision[23] = new Point (-1, -1);
                }
            }
            
            if (LightLevel > 2)
            {
                if (n == 3)
                {
                    Vision[8] = new Point(-1, -1);

                    if (LightLevel > 3)
                        Vision[15] = new Point (-1, -1);
                }
                if (n == 4)
                {
                    Vision[9] = new Point(-1, -1);
                    Vision[10] = new Point(-1, -1);

                    if (LightLevel > 3)
                    {
                        Vision[16] = new Point (-1, -1);
                        Vision[17] = new Point (-1, -1);
                        Vision[18] = new Point (-1, -1);
                    }
                }
                if (n == 5)
                {
                    Vision[10] = new Point(-1, -1);
                    Vision[11] = new Point(-1, -1);
                    Vision[12] = new Point(-1, -1);

                    if (LightLevel > 3)
                    {
                        Vision[18] = new Point (-1, -1);
                        Vision[19] = new Point (-1, -1);
                        Vision[20] = new Point (-1, -1);
                    }
                }
                if (n == 6)
                {
                    Vision[12] = new Point(-1, -1);
                    Vision[13] = new Point(-1, -1);

                    if (LightLevel > 3)
                    {
                        Vision[21] = new Point (-1, -1);
                        Vision[22] = new Point (-1, -1);
                    }
                }
                if (n == 7)
                {
                    Vision[14] = new Point(-1, -1);
                    
                    if (LightLevel > 3)
                        Vision[23] = new Point (-1, -1);
                }
            }

            if (LightLevel > 3)
            {
                if (n == 8)
                {
                    Vision[15] = new Point(-1, -1);
                }
                if (n == 9)
                {
                    Vision[16] = new Point(-1, -1);
                    Vision[17] = new Point(-1, -1);
                }
                if (n == 10)
                {
                    Vision[17] = new Point(-1, -1);
                    Vision[18] = new Point(-1, -1);
                }
                if (n == 11)
                {
                    Vision[18] = new Point(-1, -1);
                    Vision[19] = new Point(-1, -1);
                    Vision[20] = new Point(-1, -1);
                }
                if (n == 12)
                {
                    Vision[20] = new Point(-1, -1);
                    Vision[21] = new Point(-1, -1);
                }
                if (n == 13)
                {
                    Vision[21] = new Point(-1, -1);
                    Vision[22] = new Point(-1, -1);
                }
                if (n == 14)
                {
                    Vision[23] = new Point(-1, -1);
                }
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
                        if (LightLevel > 1)
                        {
                            Occlusion(index);
                        }
                    }
                    else if (Vision[index].X != -1)
                        Vision[index] = new Point(Position.X + x, Position.Y + y);
                }
                else
                    Vision[index] = new Point(-1, -1);
        }

            
            

        bool WithinBounds(int x, int y)
        {
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

            switch (key)
            {
                case ConsoleKey.RightArrow:
                    newPositionX = Position.X + 1;
                    if (WithinBounds(newPositionX, Position.Y) 
                        && Game.screen[newPositionX, Position.Y] != Pieces.Fire
                        && Game.screen[newPositionX, Position.Y] != Pieces.Wall
                        && Game.screen[newPositionX, Position.Y] != Pieces.Window)
                    {
                        if (Game.screen[newPositionX, Position.Y] == Pieces.NextLevel)
                            Game.CurrentLevel++;

                        Position = new Point (newPositionX, Position.Y);
                    }
                        dir = Direction.Right;
                        if (LightLevel > 0)
                            SetVision();
                    break;
                case ConsoleKey.LeftArrow:
                    newPositionX = Position.X - 1;
                    if (WithinBounds(newPositionX, Position.Y) 
                        && Game.screen[newPositionX, Position.Y] != Pieces.Fire
                        && Game.screen[newPositionX, Position.Y] != Pieces.Wall
                        && Game.screen[newPositionX, Position.Y] != Pieces.Window)
                    {
                        if (Game.screen[newPositionX, Position.Y] == Pieces.NextLevel)
                            Game.CurrentLevel++;

                        Position = new Point (newPositionX, Position.Y);
                    }
                        dir = Direction.Left;
                         if (LightLevel > 0)
                            SetVision();
                    break;
                case ConsoleKey.DownArrow:
                    newPositionY = Position.Y + 1;
                    if (WithinBounds(Position.X, newPositionY) 
                        && Game.screen[Position.X, newPositionY] != Pieces.Fire 
                        && Game.screen[Position.X, newPositionY] != Pieces.Wall
                        && Game.screen[Position.X, newPositionY] != Pieces.Window)
                    {
                        if (Game.screen[Position.X, newPositionY] == Pieces.NextLevel)
                            Game.CurrentLevel++;

                        Position = new Point (Position.X, newPositionY);
                    }
                        dir = Direction.Down;
                        if (LightLevel > 0)
                            SetVision();
                    break;
                case ConsoleKey.UpArrow:
                    newPositionY = Position.Y - 1; 
                    if (WithinBounds(Position.X, newPositionY) 
                        && Game.screen[Position.X, newPositionY] != Pieces.Fire
                        && Game.screen[Position.X, newPositionY] != Pieces.Wall
                        && Game.screen[Position.X, newPositionY] != Pieces.Window)
                    {
                        if (Game.screen[Position.X, newPositionY] == Pieces.NextLevel)
                            Game.CurrentLevel++;

                        Position = new Point (Position.X, newPositionY);
                    }
                        dir = Direction.Up;
                        if (LightLevel > 0)
                            SetVision();
                    break;

            }
         }
    }
}
