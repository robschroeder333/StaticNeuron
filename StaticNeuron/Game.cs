using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace StaticNeuron
{
    public enum Pieces { Empty, Wall, Window, Player, Enemy, NextLevel, Vision, Fire, Torch}

    class Game
    {
        public static Pieces[,] screen;
        public static Pieces[,] invisibleScreen;
        public static int currentLevel = 1;
        public static int CurrentLevel 
        {
            get
            {
                return currentLevel;
            }
            set
            {
                currentLevel = value;
                Console.Clear();
                levelChanged = true;
                screen = new Pieces[Program.width, Program.height];
                invisibleScreen = new Pieces[Program.width, Program.height];
                Level.CreateLevel(CurrentLevel);
            }
        }
        public Character player;
        public Character monster;
        public Fire fire;
        static bool levelChanged = false;

        public Game()
        {
            screen = new Pieces[Program.width, Program.height];
            invisibleScreen = new Pieces[Program.width, Program.height];
            player = new Character(1, 5, false);
            monster = new Character(1, 4);
            fire = new Fire(3, 5, 5);
            CurrentLevel = 1;
        }

        public void Step()
        {
            do {
                screen = new Pieces[Program.width, Program.height];
                invisibleScreen = new Pieces[Program.width, Program.height];
                LevelManager();

                if (player.Actions > 0)
                {
                    if (Console.KeyAvailable != true)
                    {
                        player.Move(Console.ReadKey().Key);
                        screen[player.Position.X, player.Position.Y] = Pieces.Player;
                        monster.NPCMove();
                        screen[monster.Position.X, monster.Position.Y] = Pieces.Enemy;
                        invisibleScreen[player.Position.X, player.Position.Y] = Pieces.Player;
                        invisibleScreen[fire.Position.X, fire.Position.Y] = Pieces.Torch;
                        

                        foreach (Point vision in fire.Vision)
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

            } while (player.LightLevel >= 0); 

        }

        void LevelManager()
        {
            if (levelChanged)
            {
                switch (CurrentLevel)
                {
                    case 1:
                    case 2:
                        player.Position = new Point(3, 5);//3,5
                        levelChanged = false;
                        break;
                    case 3:
                        player.Position = new Point(2, 25);
                        levelChanged = false;
                        break;
                    default:
                        break;
                }
            }


            foreach (Point wall in Level.Walls)
            {
                screen[wall.X, wall.Y] = Pieces.Wall;
            }
            foreach (Point window in Level.Windows)
            {
                screen[window.X, window.Y] = Pieces.Window;
            }
            foreach (Point spots in Level.NextLevelSpots)
            {
                screen[spots.X, spots.Y] = Pieces.NextLevel;
            }

        }
    }
}
