using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeanCards.DAL.Entity
{
    public class Language
    {
        public Language()
        {
            Games = new List<Game>();
            AnswerCards = new List<AnswerCard>();
            QuestionCards = new List<QuestionCard>();
        }
        [Key]
        public int LanguageId { get; set; }
        [StringLength(8)]
        public string Code { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public List<Game> Games { get; set; }
        public List<AnswerCard> AnswerCards { get; set; }
        public List<QuestionCard> QuestionCards { get; set; }
    }
}
