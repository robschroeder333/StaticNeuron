using System;
using System.Text;

namespace StaticNeuron
{
    public class Render
    {
        public void DrawScreen(Pieces[,] gameScreen, int width, int height)
        {
            //Console.Clear();
            StringBuilder screenAsString = new StringBuilder("", width * height);
            char currentCharacter = Convert.ToChar(32);
            for (int y = 0; y < height; y++)
            {

                for (int x = 0; x < width; x++)
                {
                    if ((x == 0 && y == 0) || (x == (width - 1) && y == (height - 1)))
                        currentCharacter = '/';
                    else if ((x == 0 && y == (height - 1)) || ((x == (width - 1)) && (y == 0)))
                        currentCharacter = '\\';
                    else if (x > 0 && x < (width - 1))
                        currentCharacter = '-';
                    else if (y > 0 && y < (height - 1))
                        currentCharacter = '|';
                    if (y > 0 && y < height - 1 && x != 0 && x < width - 1)
                    {

                        switch (gameScreen[x, y])
                        {
                            case (Pieces.Empty):
                                currentCharacter = Convert.ToChar(32);
                                break;
                            case (Pieces.Wall):
                                currentCharacter = 'X';
                                break;
                            case (Pieces.Window):
                                currentCharacter = 'O';
                                break;
                            case (Pieces.Player):
                                currentCharacter = 'P';
                                break;
                            case (Pieces.Vision):
                                currentCharacter = 'V';
                                break;

                        }
                    }
                    screenAsString.Append(new char[] { currentCharacter });


                }
                screenAsString.Append("\n");
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(screenAsString);

        }
    }
}

    
