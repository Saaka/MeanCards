using MeanCards.Model.Core.Games.Base;

namespace MeanCards.Model.Core.Games
{
    public class SelectAnswer : IBaseRequest, IGameRequest, IGameRoundRequest, IUserRequest, IPlayerRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int GameRoundId { get; set; }
        public int PlayerAnswerId { get; set; }
    }
}
