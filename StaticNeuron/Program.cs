using System;

namespace StaticNeuron
{
    class Program
    {
        public static int height = 30;
        public static int width = 120;
        static void Main()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n\n     ▄▄▄▄▄      ▄▄▄▄▀ ██     ▄▄▄▄▀ ▄█ ▄█▄       ▄   ▄███▄     ▄   █▄▄▄▄ ████▄    ▄   ");
            Console.WriteLine("    █     ▀▄ ▀▀▀ █    █ █ ▀▀▀ █    ██ █▀ ▀▄      █  █▀   ▀     █  █  ▄▀ █   █     █  ");
            Console.WriteLine("  ▄  ▀▀▀▀▄       █    █▄▄█    █    ██ █   ▀  ██   █ ██▄▄    █   █ █▀▀▌  █   █ ██   █ ");
            Console.WriteLine("   ▀▄▄▄▄▀       █     █  █   █     ▐█ █▄  ▄▀ █ █  █ █▄   ▄▀ █   █ █  █  ▀████ █ █  █ ");
            Console.WriteLine("               ▀         █  ▀       ▐ ▀███▀  █  █ █ ▀███▀   █▄ ▄█   █         █  █ █ ");
            Console.WriteLine("                        █                    █   ██          ▀▀▀   ▀          █   ██ ");
            Console.WriteLine("                       ▀                                                             \n\n\n\n\n");
            Console.WriteLine("                             Press Any Key To Continue                               ");
            Console.ReadKey();
            Game game = new Game();
            game.Play();
        }
    }
}
