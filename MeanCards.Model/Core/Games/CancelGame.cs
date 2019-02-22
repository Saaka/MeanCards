using MeanCards.Model.Core.Games.Base;
using MediatR;

namespace MeanCards.Model.Core.Games
{
    public class CancelGame : IRequest<CancelGameResult>, IGameRequest, IUserRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}
