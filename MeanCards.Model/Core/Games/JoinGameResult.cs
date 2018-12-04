namespace MeanCards.Model.Core.Games
{
    public class JoinGameResult : BaseResult
    {
        public JoinGameResult()
        {
        }

        public JoinGameResult(string error) : base(error)
        {
        }

        public int PlayerId { get; set; }
    }
}
