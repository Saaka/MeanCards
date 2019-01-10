namespace MeanCards.Model.Core.Games.Base
{
    public interface IGameRoundRequest : IGameRequest
    {
        int GameRoundId { get; }
    }
}
