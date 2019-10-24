using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace StaticNeuron
{
    class Level
    {
        static public List<Point> Walls { get; set; } = new List<Point>();
        static public List<Point> Windows { get; set; } = new List<Point>();
        static public List<Point> NextLevelSpots { get; set; } = new List<Point>();       

        delegate void TilePicker(Point origin);
        enum Prefabs 
        {
            FourWay, Hall_H, Hall_V, T_H_S, T_H_N, 
            Loop_W_S, Loop_N_E, StartRoom, LevelStart, 
            NextLevel, EndRoom, Test
        }
        
        static int tileSize = 10;



        public static void CreateLevel(int levelChoice)
        {
            if (levelChoice > 1)
            {
                Walls.Clear();
                Windows.Clear();
                NextLevelSpots.Clear();
            }
            int tilesHigh = (Program.height - 2) / tileSize;
            int tilesWide = (Program.width - 2) / tileSize;
            Point origin = new Point(1, 1);
            Prefabs[] bluePrint = new Prefabs[tilesHigh * tilesWide];
            int index = 0;

            switch (levelChoice)
            {
                case 1:
                    for (int i = 0; i < bluePrint.Length; i++)
                    {
                        if (i == 0)
                            bluePrint[i] = Prefabs.StartRoom;
                        else if (i == 20)
                            bluePrint[i] = Prefabs.NextLevel;
                        else if (i == 3 || i == 7 || i == 15 || i == 18)
                            bluePrint[i] = Prefabs.FourWay;
                        else if (i == 8 || i == 12)
                            bluePrint[i] = Prefabs.T_H_S;
                        else if (i == 13)
                            bluePrint[i] = Prefabs.T_H_N;
                        else if (i == 6)
                            bluePrint[i] = Prefabs.Loop_W_S;
                        else if (i == 14)
                            bluePrint[i] = Prefabs.Loop_N_E;
                        else
                            bluePrint[i] = Prefabs.Hall_H;
                    }
                    break;
                case 2:
                    for (int i = 0; i < bluePrint.Length; i++)
                    {
                        if (i == 0)
                            bluePrint[i] = Prefabs.LevelStart;
                        else if (i == 1 || i == 3 || i == 6)
                            bluePrint[i] = Prefabs.Loop_W_S;
                        else if (i == 5 || i == 20)
                            bluePrint[i] = Prefabs.Hall_H;
                        else if (i == 8 || i == 12 || i == 19)
                            bluePrint[i] = Prefabs.FourWay;
                        else if (i > 8 && i < 12)
                            bluePrint[i] = Prefabs.Hall_V;
                        else if (i == 13)
                            bluePrint[i] = Prefabs.NextLevel;
                        else if (i == 14 || i == 15 || i == 17)
                            bluePrint[i] = Prefabs.Loop_N_E;
                        else if (i == 16 || i == 18)
                            bluePrint[i] = Prefabs.T_H_N;
                        else
                            bluePrint[i] = Prefabs.T_H_S;
                    }
                    break;
                case 3://final level                    
                    for (int i = 0; i < bluePrint.Length; i++)
                    {   
                        if (i < 2 || i == 5 || i == 13)
                            bluePrint[i] = Prefabs.FourWay;
                        else if (i == 7)
                            bluePrint[i] = Prefabs.Loop_N_E;
                        else if (i == 2 || i == 8 || i == 12 || i == 16)
                            bluePrint[i] = Prefabs.Hall_H;
                        else if (i == 9 || i == 18 || i == 20)
                            bluePrint[i] = Prefabs.T_H_N;
                        else if (i == 10)
                            bluePrint[i] = Prefabs.EndRoom;
                        else if (i == 11)
                            bluePrint[i] = Prefabs.Hall_V;
                        else if (i == 14)
                            bluePrint[i] = Prefabs.LevelStart;
                        else
                            bluePrint[i] = Prefabs.T_H_S;
                    }
                    break;
                default:
                    bluePrint[0] = Prefabs.Test;
                    break;
            }

            for (int y = 0; y < tilesHigh; y++)
            {
                for (int x = 0; x < tilesWide; x++)
                {
                    TilePicker tilePicker = (bluePrint[index]) switch
                    {
                        Prefabs.FourWay => FourWay,
                        Prefabs.Hall_H => Hall_H,
                        Prefabs.Hall_V => Hall_V,
                        Prefabs.T_H_S => T_H_S,
                        Prefabs.T_H_N => T_H_N,
                        Prefabs.Loop_W_S => Loop_W_S,
                        Prefabs.Loop_N_E => Loop_N_E,
                        Prefabs.Test => TestRoom,
                        Prefabs.StartRoom => StartRoom,
                        Prefabs.LevelStart => LevelStart,
                        Prefabs.NextLevel => NextLevel,
                        Prefabs.EndRoom => EndRoom,
                        _ => FourWay,
                    };
                    tilePicker(new Point(origin.X + (x * tileSize), origin.Y + (y * tileSize)));
                    index++;
                }
            }
        }

        static void Hall_H(Point origin)
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
        static void Hall_V(Point origin)
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
        static void FourWay(Point origin)
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
        static void T_H_S(Point origin)
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
        static void T_H_N(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 9 && (x == 0 || x == 9))
                        Walls.Add(new Point(origin.X + x, origin.Y + y));

                    if (y == 8)
                    {
                        if (x < 2 || x > 7)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                        else
                            Windows.Add(new Point(origin.X + x, origin.Y + y));
                    }

                    if (y < 4)
                    {
                        if (x < 2 || x > 6)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                }
            }
        }
        static void Loop_W_S(Point origin)
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
        static void Loop_N_E(Point origin)
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
        static void StartRoom(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 9)
                        Walls.Add(new Point(origin.X + x, origin.Y + y));

                    if (y == 0)
                    {
                        if (x == 2 || x == 3 || x == 6 || x == 7 || x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 1 || y == 8)
                    {
                        if (x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 2 || y == 7)
                    {
                        if (x == 0 || x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 3 || y == 6)
                    {
                        if (x == 0)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                }
            }
        }
        static void LevelStart(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 0 || y == 9)
                    {
                        Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 1 || y == 8)
                    {
                        if (x < 2 || x == 4 || x > 7)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 2 || y == 7)
                    {
                        if (x == 0 || x == 2 || x == 7 || x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                }
            }
        }
        static void NextLevel(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 0)
                    {
                        if (x < 3 || x > 6)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 1 || y == 2)
                    {
                        if (x == 0 || x > 6)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 3 || y == 6)
                    {
                        if (x == 6)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));

                        if (x == 7)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));

                        if (x > 7)
                            NextLevelSpots.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 4 || y == 5)
                    {
                        if (x > 7)
                            NextLevelSpots.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 7)
                    {
                        if (x < 6 || x > 7)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y > 7)
                    {
                        if (x == 0 || x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                }
            }
        }
        static void EndRoom(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 0)
                    {
                        if (x < 3 || x > 6)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 1)
                    {
                        if (x == 0)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));

                        if (x == 1)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 2)
                    {
                        if (x == 0)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));

                        if (x == 2)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 3)
                    {
                        if (x == 3 || x == 8)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));

                        if (x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 4)
                    {
                        if (x == 4 || x == 8)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 5)
                    {
                        if (x == 8)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 6)
                    {
                        if (x == 9)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 7)
                    {
                        if (x == 0)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));

                        if (x == 9)
                            NextLevelSpots.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 8)
                    {
                        if (x == 0)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));

                        if (x > 2 && x < 6)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));

                        if (x > 7)
                            NextLevelSpots.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 9)
                    {
                        if (x == 0 || x == 3 || x == 6)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));

                        if (x > 6)
                            NextLevelSpots.Add(new Point(origin.X + x, origin.Y + y));
                    }
                }
            }
        }

        static void TestRoom(Point origin)
        {
            for (int y = 0; y < tileSize; y++)
            {
                for (int x = 0; x < tileSize; x++)
                {
                    if (y == 0)
                    {
                        if (x < 3)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 1)
                    {
                        if (x == 0 || x == 2)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 3)
                    {
                        if (x == 3 || x == 5)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                        else if (x == 4)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 4)
                    {
                        if (x == 3)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));

                        if (x == 5)
                            Windows.Add(new Point(origin.X + x, origin.Y + y));
                    }
                    if (y == 5 && x == 5)
                        Windows.Add(new Point(origin.X + x, origin.Y + y));

                    if (y == 6)
                    {
                        if ( x > 2 && x < 6)
                            Walls.Add(new Point(origin.X + x, origin.Y + y));
                    }
                }
            }
        }
    }
}
