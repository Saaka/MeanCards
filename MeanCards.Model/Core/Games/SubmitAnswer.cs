namespace MeanCards.Model.Core.Games
{
    public class SubmitAnswer
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int GameRoundId { get; set; }
        public int PlayerCardId { get; set; }
    }
}
