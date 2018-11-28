using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DAL.Entity
{
    public class Player
    {
        public Player()
        {
            OwnedGameRounds = new List<GameRound>();
            WonRounds = new List<GameRound>();
            PlayerAnswers = new List<PlayerAnswer>();
            PlayerCards = new List<PlayerCard>();
        }
        [Key]
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Number { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }

        public Game Game { get; set; }
        public List<GameRound> OwnedGameRounds { get; set; }
        public List<GameRound> WonRounds { get; set; }
        public List<PlayerAnswer> PlayerAnswers { get; set; }
        public List<PlayerCard> PlayerCards { get; set; }
    }
}
