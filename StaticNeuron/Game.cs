﻿using System;
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
                Lights.Clear();
                Level.CreateLevel(CurrentLevel);

            }
        }
        public Character player;
        static List<Fire> Lights;
        static bool levelChanged = false;       

        public Game()
        {
            screen = new Pieces[Program.width, Program.height];
            invisibleScreen = new Pieces[Program.width, Program.height];
            player = new Character(1, 5);
            Lights = new List<Fire>();
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
                        invisibleScreen[player.Position.X, player.Position.Y] = Pieces.Player;
                        if (Level.Enemies.Capacity > 0 && Level.Enemies.Count > 0)
                        {
                            int index = 0;
                            int indexToBeRemoved = -1;
                            foreach (Enemy enemy in Level.Enemies)
                            {
                                enemy.Move();
                                if ((player.Position == enemy.Position || player.Position == enemy.PreviousPosition))
                                {
                                    player.LightLevel--;
                                    indexToBeRemoved = index;
                                    if (player.LightLevel > 0)
                                        player.SetVision();
                                    continue;
                                }
                                else 
                                {
                                    index ++;
                                    screen[enemy.Position.X, enemy.Position.Y] = Pieces.Enemy;
                                }
                            }
                                if (indexToBeRemoved > -1)
                                Level.Enemies.RemoveAt(indexToBeRemoved);
                                screen[player.Position.X, player.Position.Y] = Pieces.Empty;
                        }
                        foreach (Fire light in Lights)
                        {
                            foreach (Point vision in light.Vision)
                            {
                                if (vision.X != -1)
                                {
                                    invisibleScreen[vision.X, vision.Y] = (screen[vision.X, vision.Y]) switch
                                    {
                                        Pieces.Empty => Pieces.Vision,
                                        Pieces.Wall => Pieces.Wall,
                                        Pieces.Player => Pieces.Player,
                                        Pieces.Window => Pieces.Window,
                                        Pieces.Enemy => Pieces.Enemy,
                                        Pieces.NextLevel => Pieces.NextLevel,
                                        Pieces.Vision => Pieces.Vision,
                                        Pieces.Fire => Pieces.Fire,
                                        Pieces.Torch => Pieces.Torch,
                                        _ => Pieces.Vision,
                                    };
                                }
                            }
                        }

                        foreach (Point vision in player.Vision)
                        {
                            if (vision.X != -1)
                            {
                                invisibleScreen[vision.X, vision.Y] = (screen[vision.X, vision.Y]) switch
                                {
                                    Pieces.Empty => Pieces.Vision,
                                    Pieces.Wall => Pieces.Wall,
                                    Pieces.Player => Pieces.Player,
                                    Pieces.Window => Pieces.Window,
                                    Pieces.Enemy => Pieces.Enemy,
                                    Pieces.NextLevel => Pieces.NextLevel,
                                    Pieces.Vision => Pieces.Vision,
                                    Pieces.Fire => Pieces.Fire,
                                    Pieces.Torch => Pieces.Torch,
                                    _ => Pieces.Vision,
                                };
                            }
                        }

                        Render.DrawScreen();
                    }
                }

            } while (player.LightLevel != -1);

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
            foreach (Point spot in Level.NextLevelSpots)
            {
                screen[spot.X, spot.Y] = Pieces.NextLevel;
            }

            if (Lights.Capacity == 0 || Lights.Count == 0)
            {
                foreach (Point fire in Level.Fire)
                {
                    Lights.Add(new Fire(fire.X, fire.Y, 5));
                    screen[fire.X, fire.Y] = Pieces.Fire;
                }

                foreach (Point torch in Level.Torches)
                {
                    Lights.Add(new Fire(torch.X, torch.Y, 3));
                    screen[torch.X, torch.Y] = Pieces.Torch;
                }
            }
            else
            {
                foreach (Point fire in Level.Fire)
                {
                    screen[fire.X, fire.Y] = Pieces.Fire;
                }

                foreach (Point torch in Level.Torches)
                {
                    screen[torch.X, torch.Y] = Pieces.Torch;
                }

            }
        }
    }
}
