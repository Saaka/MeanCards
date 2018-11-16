using System.ComponentModel.DataAnnotations;

namespace MeanCards.DataModel.Entity
{
    public class PlayerCard
    {
        [Key]
        public int PlayerCardId { get; set; }
        public int PlayerId { get; set; }
        public int AnswerCardId { get; set; }
        public bool IsUsed { get; set; }

        public virtual Player Player { get; set; }
        public virtual AnswerCard AnswerCard { get; set; }
    }
}
