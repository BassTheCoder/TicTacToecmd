using System;
using System.Text.RegularExpressions;

namespace TicTacToeOcmd
{
    class Program
    {
        // Klasyczna plansza 3x3
        static int[,] GameBoard = new int[3, 3];

        // Funkcja poberajaca wartosc int od uzytkownika
        static int GetInt(string Text, int Min = 1, int Max = 3)
        {
            while (true)
            {
                try
                {
                    Console.Write(Text);
                    int number = int.Parse(Console.ReadLine());
                    if (number < Min || number > Max)
                    {
                        throw new ArgumentOutOfRangeException("Your number is off the limits.");
                    }
                    return number;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error. " + e.Message);
                }
            }
        }

        static string GetString(string msg)
        {
            string str;

            Console.Write(msg);
            str = Console.ReadLine();
            
            return str;
        }



        // Funkcja, która narysuje planszę do gry i wstawi O albo X zależnie, czy gracz coś w jakieś pole wstawi. odświeża się co turę (w funkcji MainGame)
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
                            switch (GameBoard[x / 2, y / 2])
                            {
                                case -1:
                                    Console.Write("O"); break;
                                case +1:
                                    Console.Write("X"); break;
                                default:
                                    Console.Write(" "); break;
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

        // Sprawdzenie czy wybrane pole jest już zajęte
        static bool IsTileFull(int X, int Y)
        {
            if (GameBoard[X, Y] == 0)
                return false;
            return true;
        }

        // Sprawdzenie czy któryś gracz spełnił warunki wygranej
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

        // Funkcja czyszcząca planszę
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

        // Funkcja sprawdzająca czy plansza jeszcze nie jest całkowicie zapełniona
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

        static void MainGame()
        {
            Console.Clear();
            ClearBoard();

            string Player1 = GetString("Player 1 name: ");
            string Player2 = GetString("Player 2 name: ");

            int CurrentPlayer = 1;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("You choose a tile to put your figure in by specifying its X and Y coordinates.");
                Console.WriteLine("The top left tile's coordinates are [1,1], and the bottom right one's [3,3]");

                DrawBoard();

                if (WinQ(CurrentPlayer))
                {

                    switch (CurrentPlayer)
                    {
                        case -1:
                            Console.Write("\n" + Player1); break;
                        case 1:
                            Console.Write("\n" + Player2); break;
                    }

                    Console.Write(" wins!");
                    Console.WriteLine();
                    break;
                }
                else if (!FreeTile())
                {
                    Console.WriteLine("\nIt's a tie!");
                    break;
                }

                CurrentPlayer *= -1;

                Console.Write("\nNow player: ");

                switch (CurrentPlayer)
                {
                    case -1:
                        Console.WriteLine(Player1); break;
                    case 1:
                        Console.WriteLine(Player2); break;
                }
                Console.WriteLine();

                while (true)
                {

                    int X = GetInt("Give X coordinates [1-3]: ", 1, 3);
                    int Y = GetInt("Give Y coordinates [1-3]: ", 1, 3);
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


        static void Main(string[] args)
        {
            while (true)
            {

                Console.Clear();
                Console.WriteLine("\n======= TIC TAC TOE =======\n");
                Console.WriteLine(" 1. New Game");
                Console.WriteLine(" 2. Player Statistics");
                Console.WriteLine(" 3. Exit");

                int menuOption = GetInt("\nChoose an option: ");

                switch (menuOption)
                {
                    case 1:
                        MainGame();
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }

                Console.WriteLine("\nPress any button to exit.");
                Console.ReadKey();
            }
        }
    }
}
