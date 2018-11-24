using System.ComponentModel.DataAnnotations;

namespace MeanCards.DAL.Entity
{
    public class PlayerAnswer
    {
        [Key]
        public int PlayerAnswerId { get; set; }
        public int GameRoundId { get; set; }
        public int PlayerId { get; set; }
        public bool IsSelectedAnswer { get; set; }
        public int AnswerCardId { get; set; }
        public int? SecondaryAnswerCardId { get; set; }

        public GameRound GameRound { get; set; }
        public Player Player { get; set; }
        public AnswerCard AnswerCard { get; set; }
        public AnswerCard SecondaryAnswerCard { get; set; }
    }
}
