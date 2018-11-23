namespace MeanCards.Model.DTO.Players
{
    public class PlayerAnswerModel
    {
        public int PlayerAnswerId { get; set; }
        public int GameRoundId { get; set; }
        public int PlayerId { get; set; }
        public bool IsSelectedAnswer { get; set; }
        public int AnswerCardId { get; set; }
        public int? SecondaryAnswerCardId { get; set; }
    }
}
