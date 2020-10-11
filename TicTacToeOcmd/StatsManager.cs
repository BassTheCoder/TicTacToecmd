using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text.Json;

namespace TicTacToeOcmd
{
    class StatsManager
    {
        public static List<Player> playersList = new List<Player>();
        private static string filePath = System.IO.Directory.GetCurrentDirectory();
        private static string fileName = "gamedata.json";

        public static void SaveToFile()
        {
            using StreamWriter streamWriter = new StreamWriter(filePath + "\\" + fileName);

            string json = JsonSerializer.Serialize(playersList);
            streamWriter.Write(json);
            streamWriter.Flush();
        }

        public static void ReadPlayersList()
        {
            try
            {
                using StreamReader streamReader = new StreamReader(filePath + "\\" + fileName);
                string json = streamReader.ReadToEnd();
                playersList = JsonSerializer.Deserialize<List<Player>>(json);
            }
            catch (Exception)
            {
                Console.WriteLine("List of players not found. No data has been loaded. \nPress any key to continue.");
                Console.ReadKey();
            }
        }

        public static Player FindOrCreatePlayer(string name)
        {
            foreach (Player player in playersList)
            {
                if (player.Name == name)
                    return player;
            }
            Player newPlayer = new Player { Name = name, Stats = new Stats() };
            playersList.Add(newPlayer);
            return newPlayer;
        }

        public static void DisplayPlayers()
        {
            Console.Clear();
            Console.WriteLine("Players:\n");
            foreach (Player player in playersList)
            {
                Console.Write("\n" + player.Name + "- Games played: " + player.Stats.GamesPlayed + " || Wins: " + player.Stats.Wins + " || Losses: " + player.Stats.Losses);
                if (player.Stats.GamesPlayed > 0)
                {
                    Console.WriteLine("|| Win %: " + player.Stats.WinPercentage + "%");
                }
                else
                {
                    Console.WriteLine("|| Player hasn't played yet, no Win% data." );
                }
            }
        }
    }
}
