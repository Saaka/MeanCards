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
        public int QuestionCardId { get; set; }
        public int RoundOwnerId { get; set; }
        public bool IsActive { get; set; }
        public int? RoundWinnerId { get; set; }
        public DateTime CreateDate { get; set; }

        public Game Game { get; set; }
        public QuestionCard QuestionCard { get; set; }
        public Player RoundOwner { get; set; }
        public Player RoundWinner { get; set; }

        public List<PlayerAnswer> PlayerAnswers { get; set; }
    }
}
