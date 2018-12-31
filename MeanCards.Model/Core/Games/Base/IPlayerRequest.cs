namespace MeanCards.Model.Core.Games.Base
{
    public interface IPlayerRequest
    {
        int UserId { get; }
        int GameId { get; set; }
    }
}
