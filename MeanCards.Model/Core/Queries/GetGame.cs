using MeanCards.Model.Core.Games.Base;
using MediatR;

namespace MeanCards.Model.Core.Queries
{
    public class GetGame : IRequest<GetGameResult>, IPlayerRequest
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
    }
}
