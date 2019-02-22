using MeanCards.Model.Core.Games.Base;
using MediatR;

namespace MeanCards.Model.Core.Games
{
    public class SubmitAnswer : IRequest<SubmitAnswerResult>, IGameRequest, IGameRoundRequest, IUserRequest, IPlayerRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int GameRoundId { get; set; }
        public int PlayerCardId { get; set; }
        public int? SecondPlayerCardId { get; set; }
    }
}
