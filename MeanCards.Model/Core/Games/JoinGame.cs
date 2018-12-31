using MeanCards.Model.Core.Games.Base;

namespace MeanCards.Model.Core.Games
{
    public class JoinGame : IBaseRequest, IGameRequest, IUserRequest
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
    }
}
