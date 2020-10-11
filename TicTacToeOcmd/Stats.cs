namespace TicTacToeOcmd
{
    class Stats
    {
        public int GamesPlayed { get => Wins + Losses + Ties; }
        public int WinPercentage { get => (Wins/GamesPlayed)*100; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }

    }
}
