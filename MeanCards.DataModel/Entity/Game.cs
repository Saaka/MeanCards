using System;
using System.Collections.Generic;

namespace MeanCards.DataModel.Entity
{
    public class Game
    {
        public Game()
        {
            GameRounds = new List<GameRound>();
            Players = new List<Player>();
        }

        public int GameId { get; set; }
        public byte GameStatus { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public bool ShowAdultContent { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public Language Language { get; set; }
        public User Owner { get; set; }
        public List<GameRound> GameRounds { get; set; }
        public List<Player> Players { get; set; }
    }
}
