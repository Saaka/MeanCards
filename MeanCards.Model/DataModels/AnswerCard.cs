using System;
using System.Collections.Generic;

namespace MeanCards.Model.DataModels
{
    public class AnswerCard
    {
        public int AnswerCardId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public bool IsAdultContent { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public Language Language { get; set; }
        public List<PlayerAnswer>  PlayerAnswers { get; set; }
        public List<PlayerAnswer>  SecondaryPlayerAnswers { get; set; }
    }
}
