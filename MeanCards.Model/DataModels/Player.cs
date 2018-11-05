using System.Collections.Generic;

namespace MeanCards.Model.DataModels
{
    public class Player
    {
        public Player()
        {
            OwnedGameRounds = new List<GameRound>();
            WonRounds = new List<GameRound>();
            PlayerAnswers = new List<PlayerAnswer>();
        }
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }

        public Game Game { get; set; }
        public List<GameRound> OwnedGameRounds { get; set; }
        public List<GameRound> WonRounds { get; set; }
        public List<PlayerAnswer> PlayerAnswers { get; set; }
    }
}
