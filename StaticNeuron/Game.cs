using System.Threading;
using System;
using System.Collections.Generic;
using System.Drawing;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Smf;

namespace StaticNeuron
{
    public enum Pieces { Empty, Wall, Window, Player, Enemy, NextLevel, Vision, Fire, Torch}

    class Game
    {
        public static Pieces[,] screen;
        public static Pieces[,] invisibleScreen;
        public static int currentLevel;
        public static int CurrentLevel 
        {
            get
            {
                return currentLevel;
            }
            set
            {
                currentLevel = value;
                Transition();
            }
        }
        public Character player;
        static List<Fire> Lights;
        static bool levelChanged = false;    

         public static string[] humanTorch = {
             @"     
                    '    _____
                   '    |     |
                    )'  |     :
               O   (*.  |-    |
             / | \ /    |     :
              / \    ___|_____|___",
           @"        
                   '     _____
                    ;'  |     |
                   '    |     :
               O   '+'  |-    |
             / | \ /    |     :
              / \    ___|_____|___", 
                   
           @"         
                    '    _____
                    : ' |     |
                   ;  , |     :
               O    +,  |-    |
             / | \ /    |     :
              / \    ___|_____|___",
           @"     
                    ;    _____
                   '    |     |
                   ' '  |     :
               O   .*)  |-    |
             / | \ /    |     :
              / \    ___|_____|___",
           @"     
                    '    _____
                   ' .  |     |
                    .   |     :
               O   '+;  |-    |
             / | \ /    |     :
              / \    ___|_____|___",
              @"
                         _____
                        |     |\
                        |  o  | \
                        | <|\_|  |
                        |  |\ |  |
                     ___|_/_|_| o|_
                               \ |
                                \|",
                    @"  
                         _____
                        | LVL |
                        |  2  :
                        |-    |
                        |     :
                     ___|_____|___",
                     @" 
                         _____
                        | LVL |
                        |  3  :
                        |-    |
                        |     :
                     ___|_____|___"

         };

        


        static Playback playback;
        static OutputDevice outputDevice;

        public Game()
        {
            screen = new Pieces[Program.width, Program.height];
            invisibleScreen = new Pieces[Program.width, Program.height];
            player = new Character(1, 5);
            Lights = new List<Fire>();
            if (Program.isWindows)
                outputDevice = OutputDevice.GetById(0);
            
            CurrentLevel = 1;            
        }

        public void Step()
        {
            do {
                screen = new Pieces[Program.width, Program.height];
                invisibleScreen = new Pieces[Program.width, Program.height];
                LevelManager();
                while (Console.KeyAvailable) Console.ReadKey(true);
                if (Console.KeyAvailable != true)
                {
                    player.Move(Console.ReadKey().Key);
                    screen[player.Position.X, player.Position.Y] = Pieces.Player;
                    if (Level.Enemies.Capacity > 0 && Level.Enemies.Count > 0)
                    {
                        int index = 0;
                        int indexToBeRemoved = -1;
                        foreach (Enemy enemy in Level.Enemies)
                        {
                            if (player.Position == enemy.Position)
                            {
                                player.LightLevel--;
                                indexToBeRemoved = index;
                                player.SetVision();
                                continue;
                            }
                            else 
                            {
                                enemy.Move();
                                index ++;
                                screen[enemy.Position.X, enemy.Position.Y] = Pieces.Enemy;
                            }
                        }
                        if (player.LightLevel > 0)
                            player.SetVision();

                        if (indexToBeRemoved > -1)
                        {
                            Level.Enemies.RemoveAt(indexToBeRemoved);
                            screen[player.Position.X, player.Position.Y] = Pieces.Empty;
                            Console.Beep();
                                for (int i = 0; i < 6; i++)
                                {
                                    if (i % 2 == 0)
                                    {   Console.SetCursorPosition(player.Position.X,player.Position.Y);
                                        Console.Write(" ");
                                    }
                                    else 
                                    {
                                        Console.SetCursorPosition(player.Position.X,player.Position.Y);
                                        Console.Write("P");
                                    }
                                    
                                    Thread.Sleep(100);
                                }
                        }
                    }

                    if (Lights.Capacity > 0 && Lights.Count > 0)
                    {
                        int index = 0;
                        int indexToBeRemoved = -1;
                        foreach (Fire light in Lights)
                        {
                            if (light.Position.X == player.Position.X 
                                && light.Position.Y == player.Position.Y 
                                && player.LightLevel < 4 
                                && Fire.CanRefresh)
                            {
                                indexToBeRemoved = index;
                                player.LightLevel++;                               
                            }
                            else
                            {
                                light.SetVision();
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
                            index++;
                        }
                        if (indexToBeRemoved != -1)
                        {
                            Lights.RemoveAt(indexToBeRemoved);
                            Level.Torches.RemoveAt(indexToBeRemoved);
                            screen[player.Position.X, player.Position.Y] = Pieces.Empty;
                            invisibleScreen[player.Position.X, player.Position.Y] = Pieces.Empty;

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
                    invisibleScreen[player.Position.X, player.Position.Y] = Pieces.Player;

                    Render.DrawScreen();
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
                        player.Position = new Point(3, 4);//3,5
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
                foreach (Point torch in Level.Torches)
                {
                    Lights.Add(new Fire(torch.X, torch.Y, 3));
                    screen[torch.X, torch.Y] = Pieces.Torch;
                }

                foreach (Point fire in Level.Fire)
                {
                    Lights.Add(new Fire(fire.X, fire.Y, 5));
                    screen[fire.X, fire.Y] = Pieces.Fire;
                }
            }
            else
            {
                foreach (Point torch in Level.Torches)
                {
                    screen[torch.X, torch.Y] = Pieces.Torch;
                }

                foreach (Point fire in Level.Fire)
                {
                    screen[fire.X, fire.Y] = Pieces.Fire;
                }
            }
        }

        public static void DisplaySequence(int choice)
        {
            void Text(string input, int pause = 2500, bool clear = true, int x = 30, int y = 13)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(input);
                Thread.Sleep(pause);
                if (clear)
                    Console.Clear();
            }
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.CursorVisible = false;
                    Thread.Sleep(2500);
                    Text("I no longer remember the sun");
                    Text("The fire,");
                    Text("is all that I have");                                       
                    Text("It keeps me safe");                                       
                    Text("keeps them at bay");                                       
                    Text("Darkness is everywhere");                    
                    Text("but glimmers of light exist");                    
                    Text("Out there");                    
                    Text("With them..");                    
                    Text("Maybe...", 500, false);                    
                    Text("A way out?", 500, false, 5, 5);//other voice                    
                    Text("You have to get out!", 1000, true, 20, 25);//other voice                    
                    Text("This place feels endless...");
                    Text("I'm not sure how much longer I can take", 1000, false);
                    Text("I have to escape", 500, false, 40, 8);//other voice                    
                    Text("You must!", 500, false, 10, 10);//other voice                    
                    Text("Go!", 1500, true, 60, 20);//other voice   
                    
                    Text("I'm not sure how much longer I can take", 0, false);                   
                    Text("You must!", 0, false, 10, 10);//other voice                    
                    Text("Go!", 1000, true, 60, 20);//other voice   

                    Text("I'm not sure how much longer I can take", 0, false);                   
                    Text("Go!", 1000, true, 60, 20);//other voice  

                    Text("I'm not sure how much longer I can take", 1000, true);                   
                    break;
                case 2:                    
                case 3:
                    Console.Clear();
                    Thread.Sleep(2000);
                    Console.CursorVisible = false;
                    for (int i = 0; i < 10; i++)
                    {
                        Text(humanTorch[0], 120, false);
                        Text(humanTorch[1], 120, false);
                        Text(humanTorch[2], 120, false);
                        Text(humanTorch[3], 120, false);
                        Text(humanTorch[4], 120, false);
                    }
                    Text(humanTorch[5], 1000, true);
                    Text(humanTorch[choice == 2 ? 6 : 7], 1500, true);
                    break;
                case 4:
                    //animation of diving toward light
                    break;               
                default:
                    break;
            }
        }

        static void NewPlayback(string file)
        {            
            playback = MidiFile.Read(file).GetPlayback(outputDevice);
            playback.InterruptNotesOnStop = true;
            playback.Loop = true;
        }

        static void Transition()
        {

            string[] songs = CurrentLevel switch
            {
                1 => new string[2] { @"songs\thenightmare.mid", @"songs\darkplaces.mid" },
                2 => new string[2] { @"songs\thenightmare.mid", @"songs\DisneyHauntHouseTheme.mid" },
                3 => new string[2] { @"songs\thenightmare.mid", @"songs\creepy3.mid" },
                4 => new string[2] { @"songs\thenightmare.mid", @"songs\creepy3.mid" },//change both
                _ => new string[2] { "funeralmarch.mid", "funeralmarch.mid" },
            };
            if (currentLevel > 1 && Program.isWindows)
                playback.Stop();

            levelChanged = true;
            screen = new Pieces[Program.width, Program.height];
            invisibleScreen = new Pieces[Program.width, Program.height];
            Lights.Clear();
            Level.CreateLevel(CurrentLevel);
            if (Program.isWindows)
            {
                NewPlayback(songs[0]);
                playback.Start();            
            }
            DisplaySequence(currentLevel);
            if (Program.isWindows)
            {
                playback.Stop();
                NewPlayback(songs[1]);
                playback.Start();

            }
            Render.DrawScreen();

        }
    }
}
