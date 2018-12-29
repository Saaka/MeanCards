using MeanCards.Model.Core.Games.Base;

namespace MeanCards.Model.Core.Games
{
    public class SubmitAnswer : IGameRequest, IGameRoundRequest, IUserRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int GameRoundId { get; set; }
        public int PlayerCardId { get; set; }
        public int? SecondPlayerCardId { get; set; }
    }
}
