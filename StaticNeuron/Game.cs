using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace StaticNeuron
{
    public enum Pieces { Empty, Wall, Window, Player, Vision }

    class Game
    {
        public static Pieces[,] screen;
        public Render rend;
        public Character player;
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Game(int width, int height)
        {
            Height = height;
            Width = width;
            screen = new Pieces[width, height];
            rend = new Render();
            player = new Character(height, width, screen);
        }

        public void Play()
        {

            do {
                screen = new Pieces[Width, Height];
                screen[player.Position.X, player.Position.Y] = Pieces.Player;
            foreach (Point vision in player.Vision)
                {
                    if (vision.X != -1) 
                    screen[vision.X, vision.Y] = Pieces.Vision;
                }
                rend.DrawScreen(screen, Width, Height);
                player.Move();
                Console.WriteLine(Width);
                Console.WriteLine(Height);
                } while (true); 

        }
    }
}
