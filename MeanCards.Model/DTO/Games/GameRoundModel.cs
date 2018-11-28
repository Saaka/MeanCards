namespace MeanCards.Model.DTO.Games
{
    public class GameRoundModel
    {
        public int GameRoundId { get; set; }
        public int RoundNumber { get; set; }
        public int GameId { get; set; }
        public int QuestionCardId { get; set; }
        public int RoundOwnerId { get; set; }
        public int? RoundWinnerId { get; set; }
    }
}
