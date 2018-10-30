using System;

namespace MeanCards.Model.DataModels
{
    public class QuestionCard
    {
        public int QuestionCardId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public bool IsAdultContent { get; set; }
        public byte NumberOfAnswers { get; set; }
        public DateTime CreateDate { get; set; }

        public Language Language { get; set; }
    }
}
