﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace StaticNeuron
{
    class Fire  
    {
        public int Size { get; set; }
        public Point Position { get; set; }
        public Point[] Vision { get; private set; }

        public Fire (int x, int y, int size)
        {
            Size = size;
            Position = new Point(x, y);
            Vision = new Point[size*size];
            SetVision();
        }

        public void SetVision()
        {
            GetMatrix(Size);
            int index = 0;
            for (int i = 0; i < Vision.Length; i++)
            {
                OcclusionChecker(Vision[i].X, Vision[i].Y, index);
                index++;
            }
        }

        public void GetMatrix(int size)
        {
            int offset = 0;

            if (Size == 3)
            {
                offset = 1;
            }
            if (Size == 5)
            {
                offset = 2;
            }
            int min = 0;
            int max = size - 1;
            int x = 0;
            int y = 0;
            for (int i = 0; i < size * size; i++)
            {
                if (WithinBounds(Position.X + x - offset, Position.Y + y - offset))
                {
                    Vision[i] = new Point(x, y);
                    //Console.WriteLine($"x={Vision[i].X} y={Vision[i].Y}");
                }
                else Vision[i] = new Point(-1, -1);

                if (y == max && x != min)
                    x--;
                else if (x == max)
                    y++;
                else if (y == min)
                    x++;
                else if (x == min && y != min + 1)
                    y--;
                else
                {
                    min++;
                    max--;
                    x++;
                }
            }
        }

        public void Occlusion(int n)
        {
            if (n == 17)
            {
                Vision[1] = new Point(-1, -1);
                Vision[2] = new Point(-1, -1);
                Vision[16] = new Point(-1, -1);
            }
            if (n == 18)
            {
                Vision[2] = new Point(-1, -1);
                Vision[3] = new Point(-1, -1);
                Vision[4] = new Point(-1, -1);
            }
            if (n == 19)
            {
                Vision[4] = new Point(-1, -1);
                Vision[5] = new Point(-1, -1);
                Vision[6] = new Point(-1, -1);
            }
            if (n == 20)
            {
                Vision[6] = new Point(-1, -1);
                Vision[7] = new Point(-1, -1);
                Vision[8] = new Point(-1, -1);
            }
            if (n == 21)
            {
                Vision[8] = new Point(-1, -1);
                Vision[9] = new Point(-1, -1);
                Vision[10] = new Point(-1, -1);
            }
            if (n == 22)
            {
                Vision[10] = new Point(-1, -1);
                Vision[11] = new Point(-1, -1);
                Vision[12] = new Point(-1, -1);
            }
            if (n == 23)
            {
                Vision[12] = new Point(-1, -1);
                Vision[13] = new Point(-1, -1);
                Vision[14] = new Point(-1, -1);
            }
            if (n == 24)
            {
                Vision[14] = new Point(-1, -1);
                Vision[15] = new Point(-1, -1);
                Vision[16] = new Point(-1, -1);
            }
        }

        void OcclusionChecker(int x, int y, int index)
        {
            if (WithinBounds(Position.X + x - 1, Position.Y + y - 1))
            {
                if (Game.screen[Position.X + x - 1, Position.Y + y - 1] == Pieces.Wall
                    && Vision[index].X != -1)
                {
                    Vision[index] = new Point(Position.X + x - 1, Position.Y + y - 1);
                    Occlusion(index);
                }
                else if (Vision[index].X != -1)
                    Vision[index] = new Point(Position.X + x - 1, Position.Y + y - 1);
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

    }
}
