using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace StaticNeuron
{
    class Enemy
    {
        public Point Position { get; private set; }
        bool isVert;
        bool isForward;
        public Enemy(int x, int y, bool vert, bool forward = true)
        {
            Position = new Point(x, y);
            isVert = vert;
            isForward = forward;
        }
        void Move()
        {
            if (isVert)
            {
                if (isForward)
                {
                    if (Position.Y + 1 < Program.height - 1
                        && (Game.screen[Position.X, Position.Y + 1] != Pieces.Wall
                            || Game.screen[Position.X, Position.Y + 1] != Pieces.Window))
                    { 
                        Position = new Point(Position.X, Position.Y + 1);
                    }
                    else
                        isForward = !isForward;
                    
                }
                else
                {
                    if (Position.Y - 1 > 1
                        && (Game.screen[Position.X, Position.Y - 1] != Pieces.Wall
                            || Game.screen[Position.X, Position.Y - 1] != Pieces.Window))
                    {
                        Position = new Point(Position.X, Position.Y - 1);
                    }
                    else
                        isForward = !isForward;
                }
            }
            else
            {
                if (isForward)
                {
                    if (Position.X + 1 < Program.height - 1
                        && (Game.screen[Position.X + 1, Position.Y] != Pieces.Wall
                            || Game.screen[Position.X + 1, Position.Y] != Pieces.Window))
                    {
                        Position = new Point(Position.X + 1, Position.Y);
                    }
                    else
                        isForward = !isForward;

                }
                else
                {
                    if (Position.X - 1 > 1
                        && (Game.screen[Position.X - 1, Position.Y] != Pieces.Wall
                            || Game.screen[Position.X - 1, Position.Y] != Pieces.Window))
                    {
                        Position = new Point(Position.X - 1, Position.Y);
                    }
                    else
                        isForward = !isForward;

                }
            }
        }
    }
}
