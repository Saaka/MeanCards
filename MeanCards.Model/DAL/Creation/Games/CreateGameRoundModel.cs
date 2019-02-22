namespace MeanCards.Model.DAL.Creation.Games
{
    public class CreateGameRoundModel
    {
        public int GameId { get; set; }
        public string RoundCode { get; set; }
        public int QuestionCardId { get; set; }
        public int OwnerPlayerId { get; set; }
        public int RoundNumber { get; set; }
    }
}
