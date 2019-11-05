using System.Xml;
using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace StaticNeuron
{
    class Program
    {
        //offset by 2 to track border easily
        public static int height = 32;
        public static int width = 72;
        public static int[] colors = { 52,88,124,160,1,196};
        
        public static string[] quotes = {"Overconfidence is a slow and insidious killer", 
        "Do you think God stays in heaven because he too lives in fear of what he's created?",
        "We make our own monsters, then fear them for what they show us about ourselves",
        "It’s a funny thing, ambition. It can take one to sublime heights or harrowing depths. And sometimes they are one and the same.",
        "Don't wish it were easier, wish you were better."};
        public static Random rnd = new Random();
        public static bool isWindows;
        static void Main()
        {
            isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            Opening();
            Game game = new Game();
            game.Step();
            Death();
        }

        static void Opening()
        {            
            Console.Clear();
            Console.WriteLine("\n\n\n\n\n");
            Console.WriteLine("     ▄▄▄▄▄      ▄▄▄▄▀ ██     ▄▄▄▄▀ ▄█ ▄█▄       ▄   ▄███▄     ▄   █▄▄▄▄ ████▄    ▄   ");
            Console.WriteLine("    █     ▀▄ ▀▀▀ █    █ █ ▀▀▀ █    ██ █▀ ▀▄      █  █▀   ▀     █  █  ▄▀ █   █     █  ");
            Console.WriteLine("  ▄  ▀▀▀▀▄       █    █▄▄█    █    ██ █   ▀  ██   █ ██▄▄    █   █ █▀▀▌  █   █ ██   █ ");
            Console.WriteLine("   ▀▄▄▄▄▀       █     █  █   █     ▐█ █▄  ▄▀ █ █  █ █▄   ▄▀ █   █ █  █  ▀████ █ █  █ ");
            Console.WriteLine("               ▀         █  ▀       ▐ ▀███▀  █  █ █ ▀███▀   █▄ ▄█   █         █  █ █ ");
            Console.WriteLine("                        █                    █   ██          ▀▀▀   ▀          █   ██ ");
            Console.WriteLine("                       ▀                                                             ");
            Console.WriteLine("\n\n\n\n\n");
            Console.WriteLine("                             Press Any Key To Continue                               ");
            Console.ReadKey();
        }

        static void Death()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 6; i++)
            {
                int color = colors[i];
                Console.SetCursorPosition(0, i + 5);
                Console.WriteLine($"\u001b[38:5:{color}m     ▄▄▄▄▄      ▄▄▄▄▀ ██     ▄▄▄▄▀ ▄█ ▄█▄       ▄   ▄███▄     ▄   █▄▄▄▄ ████▄    ▄   ");
                Console.WriteLine("    █     ▀▄ ▀▀▀ █    █ █ ▀▀▀ █    ██ █▀ ▀▄      █  █▀   ▀     █  █  ▄▀ █   █     █  ");
                Console.WriteLine("  ▄  ▀▀▀▀▄       █    █▄▄█    █    ██ █   ▀  ██   █ ██▄▄    █   █ █▀▀▌  █   █ ██   █ ");
                Console.WriteLine("   ▀▄▄▄▄▀       █     █  █   █     ▐█ █▄  ▄▀ █ █  █ █▄   ▄▀ █   █ █  █  ▀████ █ █  █ ");
                Console.WriteLine("               ▀         █  ▀       ▐ ▀███▀  █  █ █ ▀███▀   █▄ ▄█   █         █  █ █ ");
                Console.WriteLine("                        █                    █   ██          ▀▀▀   ▀          █   ██ ");
                Console.WriteLine("                       ▀                                                             ");
                Console.WriteLine("\n\n\n\n\n");
                Thread.Sleep(200 + (i * 80 - (i * 15)));
            }
                Console.WriteLine($"{quotes[rnd.Next(0,quotes.Length - 1)]}");
        }
        static void ColorTest()
        {
            Console.BackgroundColor = ConsoleColor.White;

            //Backgrounds
            Console.Write("\u001b[40m black \u001b[0m");//black
            Console.Write("\u001b[40;1m bright black \u001b[0m");//bright black
            Console.Write("\u001b[41m red \u001b[0m");//red
            Console.Write("\u001b[41;1m bright red \u001b[0m");//bright red
            Console.Write("\u001b[42m green \u001b[0m");//green
            Console.Write("\u001b[42;1m bright green \u001b[0m");//bright green
            Console.Write("\u001b[43m yellow \u001b[0m");//yellow
            Console.Write("\u001b[43;1m bright yellow \u001b[0m\n");//bright yellow
            Console.Write("\u001b[44m blue \u001b[0m");//blue
            Console.Write("\u001b[44;1m bright blue \u001b[0m");//bright blue
            Console.Write("\u001b[45m magenta \u001b[0m");//magenta
            Console.Write("\u001b[45;1m bright magenta \u001b[0m");//bright magenta
            Console.Write("\u001b[46m cyan \u001b[0m");//cyan
            Console.Write("\u001b[46;1m bright cyan \u001b[0m");//bright cyan
            Console.Write("\u001b[47m\u001b[30m white\u001b[0m");//white (with black foreground)
            Console.Write("\u001b[47;1m bright white\u001b[0m\n\n");//bright white

            Console.BackgroundColor = ConsoleColor.White;
            
            //Foregrounds
            Console.Write("\u001b[30m black \u001b[0m");//black
            Console.Write("\u001b[30;1m bright black \u001b[0m");//bright black
            Console.Write("\u001b[31m red \u001b[0m");//red
            Console.Write("\u001b[31;1m bright red \u001b[0m");//bright red
            Console.Write("\u001b[32m green \u001b[0m");//green
            Console.Write("\u001b[32;1m bright green \u001b[0m");//bright green
            Console.Write("\u001b[33m yellow \u001b[0m");//yellow
            Console.Write("\u001b[33;1m bright yellow \u001b[0m\n");//bright yellow
            Console.Write("\u001b[34m blue \u001b[0m");//blue
            Console.Write("\u001b[34;1m bright blue \u001b[0m");//bright blue
            Console.Write("\u001b[35m magenta \u001b[0m");//magenta
            Console.Write("\u001b[35;1m bright magenta \u001b[0m");//bright magenta
            Console.Write("\u001b[36m cyan \u001b[0m");//cyan
            Console.Write("\u001b[36;1m bright cyan \u001b[0m");//bright cyan
            Console.Write("\u001b[37m white\u001b[0m");//white (same color as foreground)
            Console.Write("\u001b[37;1m bright white\u001b[0m\n\n");//bright white
        }
    }
}
