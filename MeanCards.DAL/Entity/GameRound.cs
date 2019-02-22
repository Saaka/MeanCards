using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DAL.Entity
{
    public class GameRound
    {
        public GameRound()
        {
            PlayerAnswers = new List<PlayerAnswer>();
        }

        [Key]
        public int GameRoundId { get; set; }
        public int GameId { get; set; }
        public string Code { get; set; }
        public byte Status { get; set; }
        public int Number { get; set; }
        public int QuestionCardId { get; set; }
        public int OwnerPlayerId { get; set; }
        public bool IsActive { get; set; }
        public int? WinnerPlayerId { get; set; }
        public DateTime CreateDate { get; set; }

        public Game Game { get; set; }
        public QuestionCard QuestionCard { get; set; }
        public Player RoundOwner { get; set; }
        public Player RoundWinner { get; set; }

        public List<PlayerAnswer> PlayerAnswers { get; set; }
    }
}
