using MeanCards.Model.Core.Games.Base;

namespace MeanCards.Model.Core.Games
{
    public class EndGame : IBaseRequest, IGameRequest, IUserRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}
