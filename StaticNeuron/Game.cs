using System;
using System.Collections.Generic;
using System.Text;

namespace StaticNeuron
{
    public enum Pieces { Empty, Wall, Window, Player, Vision }

    class Game
    {
        public Pieces[,] screen;

        public Game(int height, int width)
        {
            screen = new Pieces[height, width];
            Render rend = new Render();
            rend.DrawScreen(screen, height, width);
        }
    }
}
