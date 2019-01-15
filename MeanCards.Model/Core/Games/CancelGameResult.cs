namespace MeanCards.Model.Core.Games
{
    public class CancelGameResult : BaseResult
    {
        public CancelGameResult()
        {
        }

        public CancelGameResult(string error) : base(error)
        {
        }
    }
}
