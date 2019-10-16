using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace StaticNeuron
{
    class Level
    {
        List<Point> level;

        public Level(int width, int height)
        {
            level = new List<Point>();
            level.Add(new Point(20, 20));
        }
    }
}
