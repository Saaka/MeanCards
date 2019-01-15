using MeanCards.Model.Core.Games.Base;

namespace MeanCards.Model.Core.Games
{
    public class CancelGame : IBaseRequest, IGameRequest, IUserRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}
