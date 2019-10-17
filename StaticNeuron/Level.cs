using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace StaticNeuron
{
    class Level
    {
        public List<Point> Walls { get; set; }

        public Level()
        {
            Walls = new List<Point>();
            Walls.Add(new Point(20, 20));
        }

        public void CreateLevel(int levelChoice)
        {
            Walls.Clear();
            switch (levelChoice)
            {
                case 1:
                    Walls.Add(new Point(20, 20));
                    Walls.Add(new Point(21, 20));
                    Walls.Add(new Point(22, 20));
                    Walls.Add(new Point(22, 21));
                    Walls.Add(new Point(22, 22));
                    Walls.Add(new Point(5, 5));
                    Walls.Add(new Point(40, 10));
                    Walls.Add(new Point(40, 11));
                    Walls.Add(new Point(40, 13));
                    Walls.Add(new Point(40, 14));
                    break;
                default:
                    break;
            }
        }
    }
}
