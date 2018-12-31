namespace MeanCards.Model.Core.Games.Base
{
    public interface IGameRoundRequest
    {
        int GameRoundId { get; }
        int GameId { get; }
    }
}
