using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace StaticNeuron
{
    class Level
    {
        public List<Point> Walls { get; set; }
        public List<Point> Windows { get; set; }

        delegate void TilePicker(Point origin);
        enum Prefabs {FourWay, Hall_H, Hall_V, T_H_S, Loop_W_S, Loop_N_E }
        
        int tileSize = 10;

        public Level(int choice = 0)
        {
            Walls = new List<Point>();
            Windows = new List<Point>();
            CreateLevel(choice);
        }

        void CreateLevel(int levelChoice)
        {
            Walls.Clear();
            int tilesHigh = (Program.height - 2) / tileSize;
            int tilesWide = (Program.width - 2) / tileSize;
            Point origin = new Point(1, 1);
            Prefabs[] bluePrint = new Prefabs[tilesHigh * tilesWide];
            TilePicker tilePicker;
            int index = 0;

            switch (levelChoice)
            {
                case 1:


                    break;
                default:
                    for (int i = 0; i < bluePrint.Length; i++)
                    {

                        if (i == 1)
                            bluePrint[i] = Prefabs.Hall_H;
                        else if (i == 2)
                            bluePrint[i] = Prefabs.T_H_S;
                        else if (i == tilesWide - 1)
                            bluePrint[i] = Prefabs.Loop_W_S;
                        else if (i == tilesWide)
                            bluePrint[i] = Prefabs.Hall_V;
                        else
                            bluePrint[i] = Prefabs.FourWay;
                    }
                    break;
            }

            for (int y = 0; y < tilesHigh; y++)
            {
                for (int x = 0; x < tilesWide; x++)
                {
                    switch (bluePrint[index])
                    {
                        case Prefabs.FourWay:
                            tilePicker = FourWay;
                            break;
                        case Prefabs.Hall_H:
                            tilePicker = Hall_H;
                            break;
                        case Prefabs.Hall_V:
                            tilePicker = Hall_V;
                            break;
                        case Prefabs.T_H_S:
                            tilePicker = T_H_S;
                            break;
                        case Prefabs.Loop_W_S:
                            tilePicker = Loop_W_S;
                            break;
                        case Prefabs.Loop_N_E:
                            tilePicker = Loop_N_E;
                            break;
                        default:
                            tilePicker = FourWay;
                            break;
                    }
                    tilePicker(new Point(origin.X + (x * tileSize), origin.Y + (y * tileSize)));
                    index++;
                }
            }
        }

        void Hall_H(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 0 || y == 9)
                    {
                        if (x >= 3 && x <= 5)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));
                        else    
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 1 || y == 8)
                    {
                        if (x < 3 || x > 5)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 2 || y == 7)
                    {
                        if (x > 0 && x < 3 || x > 5 && x < 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 3 || y == 6)
                    {
                        if (x > 1 && x < 4 || x > 4 && x < 7)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                        
                    }
                }
            }
        }
        void Hall_V(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 0 || y == 9)
                    {
                        if (x >= 3 && x <= 5)
                            Windows.Add(new Point(origin.X + y, origin.Y + x));
                        else
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 1 || y == 8)
                    {
                        if (x < 3 || x > 5)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 2 || y == 7)
                    {
                        if (x > 0 && x < 3 || x > 5 && x < 9)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 3 || y == 6)
                    {
                        if (x > 1 && x < 4 || x > 4 && x < 7)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));

                    }
                }
            }
        }
        void FourWay(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 0 || y == 9)
                    {
                        if (x == 2 || x == 7)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y < 2 || y > 7)
                    {
                        if (x < 2 || x > 7)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 2 || y == 7)
                    {
                        if (x == 0 || x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 3 || y == 6)
                    {
                        if (x == 3 || x == 6)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));     
                    }
                }
            }
        }
        void T_H_S(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 0 && (x == 0 || x == 9))
                        Walls.Add(new Point(origin.X + x, origin.Y + y));

                    if (y == 1)
                    {
                        if (x < 2 || x > 7)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                        else
                            Windows.Add(new Point(origin.X + x, origin.Y + y));
                    }

                    if (y > 5)
                    {
                        if (x < 2 || x > 6)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                }
            }
        }
        void Loop_W_S(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 0)
                    {
                        if (x == 0 || (x > 4 && x < 8))
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 1)
                    {
                        if (x == 0 || x == 3 || x == 4 || x == 8)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 2)
                    {
                        if (x < 3 || x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 3)
                    {
                        if (x == 6 || x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 4)
                    {
                        if (x == 5 || x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 5)
                    {
                        if (x == 4)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));
                        if (x == 8)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 6)
                    {
                        if (x < 4 || x == 7)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 7 || y == 8)
                    {
                        if (x < 4 || x == 6)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 9)
                    {
                        if (x < 4 || x > 6)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                }
            }
        }
        void Loop_N_E(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 0)
                    {
                        if (x == 0 || (x > 4 && x < 8))
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 1)
                    {
                        if (x == 0 || x == 3 || x == 4 || x == 8)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 2)
                    {
                        if (x < 3 || x == 9)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 3)
                    {
                        if (x == 6 || x == 9)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 4)
                    {
                        if (x == 5 || x == 9)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 5)
                    {
                        if (x == 4)
                            Windows.Add(new Point(origin.X + y, origin.Y + x));
                        if (x == 8)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 6)
                    {
                        if (x < 4 || x == 7)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 7 || y == 8)
                    {
                        if (x < 4 || x == 6)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                    if (y == 9)
                    {
                        if (x < 4 || x > 6)
                            Walls.Add(new Point(origin.X + y, origin.Y + x));
                    }
                }
            }
        }

        void TestRoom(Point origin)
        {

        }
    }
}
