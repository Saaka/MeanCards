namespace MeanCards.Model.Core.Games.Base
{
    public interface IPlayerRequest : IGameRequest
    {
        int UserId { get; }
    }
}
