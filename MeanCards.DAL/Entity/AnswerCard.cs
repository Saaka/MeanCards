using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DAL.Entity
{
    public class AnswerCard
    {
        public AnswerCard()
        {
            PlayerAnswers = new List<PlayerAnswer>();
            PlayerCards = new List<PlayerCard>();
            SecondaryPlayerAnswers = new List<PlayerAnswer>();
        }

        [Key]
        public int AnswerCardId { get; set; }
        public int LanguageId { get; set; }
        [StringLength(256)]
        public string Text { get; set; }
        public bool IsAdultContent { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public Language Language { get; set; }
        public List<PlayerAnswer>  PlayerAnswers { get; set; }
        public List<PlayerCard>  PlayerCards { get; set; }
        public List<PlayerAnswer>  SecondaryPlayerAnswers { get; set; }
    }
}
