using System;

namespace TicTacToeOcmd
{
    class Program
    {
        static void Main(string[] args)
        {
            StatsManager.ReadPlayersList();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n======= TIC TAC TOE =======\n");
                Console.WriteLine(" 1. New Game");
                Console.WriteLine(" 2. Player Statistics");
                Console.WriteLine(" 3. Exit");

                int menuOption = Validator.GetInt("\nChoose an option: ");
                switch (menuOption)
                {
                    case 1:
                        MainGame.Game();
                        break;
                    case 2:
                        StatsManager.DisplayPlayers();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine("\nPress any button to return to main menu.");
                Console.ReadKey();
            }
        }
    }
}