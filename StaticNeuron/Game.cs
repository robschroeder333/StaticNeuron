using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace StaticNeuron
{
    public enum Pieces { Empty, Wall, Window, Player, Vision }

    class Game
    {
        public Pieces[,] screen;
        public Render rend;
        public Character player;
        int height;
        int width;
        public Game(int height, int width)
        {
            this.height = height;
            this.width = width;
            screen = new Pieces[height, width];
            rend = new Render();
            player = new Character();
        }

        public void Play()
        {
            screen[player.Position.X, player.Position.Y] = Pieces.Player;
            foreach (Point vision in player.Vision)
            {
                screen[vision.X, vision.Y] = Pieces.Vision;
            }
            rend.DrawScreen(screen, height, width);
        }
    }
}
