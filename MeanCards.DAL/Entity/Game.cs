using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DAL.Entity
{
    public class Game
    {
        public Game()
        {
            GameRounds = new List<GameRound>();
            Players = new List<Player>();
            GameCheckpoints = new List<GameCheckpoint>();
        }

        [Key]
        public int GameId { get; set; }
        [StringLength(32)]
        [Required]
        public string Code { get; set; }
        [StringLength(128)]
        [Required]
        public string Name { get; set; }
        public byte Status { get; set; }
        public int LanguageId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        public bool ShowAdultContent { get; set; }
        public bool IsActive { get; set; }
        public int PointsLimit { get; set; }
        public int? WinnerId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Language Language { get; set; }
        public virtual User Owner { get; set; }
        public virtual User Winner { get; set; }
        public virtual List<GameRound> GameRounds { get; set; }
        public virtual List<Player> Players { get; set; }
        public virtual List<GameCheckpoint> GameCheckpoints { get; set; }
    }
}
