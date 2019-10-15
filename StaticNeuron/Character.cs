using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace StaticNeuron
{
    class Character
    {
        public Point Position { get; private set; }

        public Character()
        {
            Position = new Point(3, 3);
        }
    }
}
