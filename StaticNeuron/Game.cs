using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace StaticNeuron
{
    public enum Pieces { Empty, Wall, Window, Player, Enemy, NextLevel, Vision, Fire }

    class Game
    {
        public static Pieces[,] screen;
        public static Pieces[,] invisibleScreen;
        public Character player;
        public Character monster;
        public Level level;
        public Game()
        {
            screen = new Pieces[Program.width, Program.height];
            invisibleScreen = new Pieces[Program.width, Program.height];
            player = new Character(1, 5, false);
            monster = new Character(3, 6);
            level = new Level(1);

        }

        public void Step()
        {
            do {
                screen = new Pieces[Program.width, Program.height];
                invisibleScreen = new Pieces[Program.width, Program.height];
                foreach (Point wall in level.Walls)
                {
                    screen[wall.X, wall.Y] = Pieces.Wall;
                }
                foreach (Point window in level.Windows)
                {
                    screen[window.X, window.Y] = Pieces.Window;
                }

                if (player.Actions > 0)
                {
                    if (Console.KeyAvailable != true)
                    {
                        player.Move(Console.ReadKey().Key);
                        screen[player.Position.X, player.Position.Y] = Pieces.Player;
                        monster.NPCMove();
                        screen[monster.Position.X, monster.Position.Y] = Pieces.Player;
                        invisibleScreen[player.Position.X, player.Position.Y] = Pieces.Player;
                        foreach (Point vision in player.Vision)
                        {
                            if (vision.X != -1)
                            {
                                switch (screen[vision.X, vision.Y])
                                {
                                    case Pieces.Empty:
                                        invisibleScreen[vision.X, vision.Y] = Pieces.Vision;
                                    break;
                                    case Pieces.Wall:
                                        invisibleScreen[vision.X, vision.Y] = Pieces.Wall;
                                    break;
                                    case Pieces.Player:
                                        invisibleScreen[vision.X, vision.Y] = Pieces.Player;
                                    break;
                                    case Pieces.Window:
                                        invisibleScreen[vision.X, vision.Y] = Pieces.Window;
                                    break;
                                    default:
                                        invisibleScreen[vision.X, vision.Y] = Pieces.Vision;
                                    break;
                                }
                            }

                        }
                        
                        Render.DrawScreen();
                    }
                }

            } while (true); 

        }
    }
}
