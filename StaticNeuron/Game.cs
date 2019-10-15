using System;
using System.Collections.Generic;
using System.Text;

namespace StaticNeuron
{
    enum Pieces { Empty, Wall, Window, Player, Vision}

    class Game
    {
        Pieces[,] screen;

        public Game(int height, int width)
        {
            screen = new Pieces[height, width];
        }
    }
}
