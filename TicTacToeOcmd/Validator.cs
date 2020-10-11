using System;
using System.Text.RegularExpressions;

namespace TicTacToeOcmd
{
    class Validator
    {
        public static bool ValidateInput(string input)
        {
            string pattern = "^[a-zA-Z0-9]+$";
            Regex regex = new Regex(pattern);            
            return regex.IsMatch(input);
        }

        public static int GetInt(string Text, int Min = 1, int Max = 3)
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
    }
}
