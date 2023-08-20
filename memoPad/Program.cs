namespace memoPad
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] gameScreen = new string[20, 10];

            for (int row = 0; row < gameScreen.GetLength(0); row++)
            {
                for (int col = 0; col < gameScreen.GetLength(1); col++)
                {
                    if (col == 0 || row == gameScreen.GetLength(0) - 1 || row == 0 || col == gameScreen.GetLength(1) - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        gameScreen[row, col] = "■";
                        Console.Write(gameScreen[row, col]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        gameScreen[row, col] = "  ";
                        Console.Write(gameScreen[row, col]);
                        Console.ResetColor();
                    }
                }
                Console.Write("\n");
            }
        }
    }
}

