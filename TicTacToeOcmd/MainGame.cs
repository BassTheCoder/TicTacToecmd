using System;

namespace TicTacToeOcmd
{
    class MainGame
    {
        static int[,] GameBoard = new int[3, 3];
        static void DrawBoard()
        {
            Console.WriteLine();

            for (int y = 0; y < 5; y++)
            {
                if (y % 2 == 0)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (x % 2 == 0)
                        {
                            int state = GameBoard[x / 2, y / 2];
                            if (state == -1)
                            {
                                Console.Write("O");
                            }
                            if (state == 1)
                            {
                                Console.Write("X");
                            }
                            if (state == 0)
                            {
                                Console.Write(" ");
                            }
                        }
                        else
                        {
                            Console.Write("|");
                        }
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("-----");
                }
            }
        }

        static bool IsTileFull(int X, int Y)
        {
            if (GameBoard[X, Y] == 0)
                return false;
            return true;
        }

        static bool WinQ(int WhichPlayer)
        {
            for (int y = 0; y < 3; y++)
            {
                int Sum = 0;
                for (int x = 0; x < 3; x++)
                {
                    Sum += GameBoard[x, y];
                }
                if (Sum / 3 == WhichPlayer)
                {
                    return true;
                }
            }
            for (int x = 0; x < 3; x++)
            {
                int Sum = 0;
                for (int y = 0; y < 3; y++)
                {
                    Sum += GameBoard[x, y];
                }
                if (Sum / 3 == WhichPlayer)
                    return true;
            }
            if (GameBoard[0, 0] + GameBoard[1, 1] + GameBoard[2, 2] == WhichPlayer * 3)
            {
                return true;
            }
            else if (GameBoard[2, 0] + GameBoard[1, 1] + GameBoard[0, 2] == WhichPlayer * 3)
            {
                return true;
            }

            return false;
        }

        static void ClearBoard()
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    GameBoard[x, y] = 0;
                }
            }
        }

        static bool FreeTile()
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (GameBoard[x, y] == 0)
                        return true;
                }
            }
            return false;
        }

        public static void Game()
        {
            Console.Clear();
            ClearBoard();

            string playerName1 = "";
            string playerName2 = "";

            while (!Validator.ValidateInput(playerName1))
            {
                Console.WriteLine("\nPlayer 1 name [letters and numbers only]:");
                playerName1 = Console.ReadLine();
            }

            while (!Validator.ValidateInput(playerName2))
            {
                Console.WriteLine("\nPlayer 2 name [letters and numbers only]:");
                playerName2 = Console.ReadLine();
            }

            Player player1 = StatsManager.FindOrCreatePlayer(playerName1);
            Player player2 = StatsManager.FindOrCreatePlayer(playerName2);
            StatsManager.SaveToFile();

            int CurrentPlayer = 1;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("You choose a tile to put your figure in by specifying its X and Y coordinates.");
                Console.WriteLine("The top left tile's coordinates are [1,1], and the bottom right one's [3,3]");

                DrawBoard();

                if (WinQ(CurrentPlayer))
                {
                    if (CurrentPlayer == -1)
                    {
                        Console.Write("\n" + player1.Name);
                        player1.Stats.Wins++;
                        player2.Stats.Losses++;
                        StatsManager.SaveToFile();
                    }
                    else if (CurrentPlayer == 1)
                    {
                        Console.Write("\n" + player2.Name);
                        player2.Stats.Wins++;
                        player1.Stats.Losses++;
                        StatsManager.SaveToFile();
                    }
                    Console.Write(" wins!");
                    Console.WriteLine();
                    break;
                }
                else if (!FreeTile())
                {
                    Console.WriteLine("\nIt's a tie!");
                    player1.Stats.Ties++;
                    player2.Stats.Ties++;
                    break;
                }

                CurrentPlayer *= -1;

                Console.Write("\nNow player: ");

                if (CurrentPlayer == -1)
                    Console.WriteLine(player1.Name);
                else if (CurrentPlayer == 1)
                    Console.WriteLine(player2.Name);

                Console.WriteLine();

                while (true)
                {

                    int X = Validator.GetInt("Give X coordinates [1-3]: ", 1, 3);
                    int Y = Validator.GetInt("Give Y coordinates [1-3]: ", 1, 3);
                    X--;
                    Y--;

                    if (!IsTileFull(X, Y))
                    {
                        GameBoard[X, Y] = CurrentPlayer;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nThis tile is already taken.");
                    }
                }
            }
        }
    }
}
