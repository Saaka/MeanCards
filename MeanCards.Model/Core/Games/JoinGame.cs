using MeanCards.Model.Core.Games.Base;
using MediatR;

namespace MeanCards.Model.Core.Games
{
    public class JoinGame : IRequest<JoinGameResult>, IGameRequest, IUserRequest
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
    }
}
