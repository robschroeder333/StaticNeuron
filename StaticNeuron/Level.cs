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


            switch (levelChoice)
            {
                case 1:
                    
                    break;
                default: //testing
                    Hall_H(new Point(1, 1));
                    break;
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
    }
}
