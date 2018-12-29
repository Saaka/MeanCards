using MeanCards.Model.Core.Games.Base;

namespace MeanCards.Model.Core.Games
{
    public class StartGameRound : IGameRequest, IGameRoundRequest, IUserRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int GameRoundId { get; set; }
    }
}
