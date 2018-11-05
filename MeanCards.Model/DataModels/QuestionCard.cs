using System;
using System.Collections.Generic;

namespace MeanCards.Model.DataModels
{
    public class QuestionCard
    {
        public QuestionCard()
        {
            GameRounds = new List<GameRound>();
        }
        public int QuestionCardId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public bool IsAdultContent { get; set; }
        public byte NumberOfAnswers { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public Language Language { get; set; }
        public List<GameRound> GameRounds { get; set; }
    }
}
