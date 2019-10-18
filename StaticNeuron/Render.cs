using System;
using System.Text;

namespace StaticNeuron
{
    public static class Render
    {
        public static void DrawScreen()
        {
            StringBuilder screenAsString = new StringBuilder("", Program.width * Program.height);
            string currentCharacter = "";
            for (int y = 0; y < Program.height; y++)
            {

                for (int x = 0; x < Program.width; x++)
                {
                    if ((x == 0 && y == 0) || (x == (Program.width - 1) && y == (Program.height - 1)))
                        currentCharacter = "/";
                    else if ((x == 0 && y == (Program.height - 1)) || ((x == (Program.width - 1)) && (y == 0)))
                        currentCharacter = "\\";
                    else if (x > 0 && x < (Program.width - 1))
                        currentCharacter = "-";
                    else if (y > 0 && y < (Program.height - 1))
                        currentCharacter = "|";
                    if (y > 0 && y < Program.height - 1 && x != 0 && x < Program.width - 1)
                    {

                        switch (Game.invisibleScreen[x, y])
                        {
                            case (Pieces.Empty):
                                currentCharacter = " ";
                                break;
                            case (Pieces.Wall):
                                currentCharacter = "\u001b[33;1m█\u001b[0m";
                                break;
                            case (Pieces.Window):
                                currentCharacter = "O";
                                break;
                            case (Pieces.Player):
                                currentCharacter = "\u001b[44m\u001b[33;1m@\u001b[0m";
                                break;
                            case (Pieces.Vision):
                                currentCharacter = "\u001b[43m \u001b[0m";
                                break;

                        }
                    }
                    screenAsString.Append(currentCharacter);


                }
                screenAsString.Append("\n");
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(screenAsString);

        }
    }
}

    
