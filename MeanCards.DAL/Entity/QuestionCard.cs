using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DAL.Entity
{
    public class QuestionCard
    {
        public QuestionCard()
        {
            GameRounds = new List<GameRound>();
        }
        [Key]
        public int QuestionCardId { get; set; }
        public int LanguageId { get; set; }
        [StringLength(256)]
        [Required]
        public string Text { get; set; }
        public bool IsAdultContent { get; set; }
        public byte NumberOfAnswers { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public Language Language { get; set; }
        public List<GameRound> GameRounds { get; set; }
    }
}
