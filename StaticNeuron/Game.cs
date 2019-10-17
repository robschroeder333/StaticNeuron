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
        public Level level;
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Game(int width, int height)
        {
            Height = height;
            Width = width;
            screen = new Pieces[width, height];
            rend = new Render();
            player = new Character(height, width, screen);
            level = new Level(width, height);
            level.CreateLevel(1);

        }

        public void Play()
        {
            //screen[player.Position.X, player.Position.Y] = Pieces.Player;
            //foreach (Point vision in player.Vision)
            //{
            //    if (vision.X != -1)
            //        screen[vision.X, vision.Y] = Pieces.Vision;
            //}
            //foreach (Point wall in level.Walls)
            //{
            //        screen[wall.X, wall.Y] = Pieces.Wall;
            //}
            //rend.DrawScreen(screen, Width, Height);

            do {
                screen = new Pieces[Width, Height];
                
                foreach (Point wall in level.Walls)
                {
                    screen[wall.X, wall.Y] = Pieces.Wall;
                }
                if (player.Turns > 0)
                {
                    if (Console.KeyAvailable != true)
                    {
                        player.Move(Console.ReadKey().Key);
                        screen[player.Position.X, player.Position.Y] = Pieces.Player;
                        foreach (Point vision in player.Vision)
                        {
                            if (vision.X != -1)
                                screen[vision.X, vision.Y] = Pieces.Vision;
                        }
                        rend.DrawScreen(screen, Width, Height);
                    }
                }

            } while (true); 

        }
    }
}
