namespace MeanCards.Model.DAL.Creation.Players
{
    public class CreatePlayerAnswerModel
    {
        public int GameRoundId { get; set; }
        public int PlayerId { get; set; }
        public int AnswerCardId { get; set; }
        public int? SecondaryAnswerCardId { get; set; }
    }
}
