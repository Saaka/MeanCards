using System.Collections.Generic;

namespace MeanCards.DataModel.Entity
{
    public class Language
    {
        public Language()
        {
            Games = new List<Game>();
            AnswerCards = new List<AnswerCard>();
            QuestionCards = new List<QuestionCard>();
        }
        public int LanguageId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public List<Game> Games { get; set; }
        public List<AnswerCard> AnswerCards { get; set; }
        public List<QuestionCard> QuestionCards { get; set; }
    }
}
