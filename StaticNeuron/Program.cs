using System;

namespace StaticNeuron
{
    class Program
    {
        public static int height = 30;
        public static int width = 120;
        static void Main()
        {
            Game game = new Game();
            game.Play();
        }
    }
}
