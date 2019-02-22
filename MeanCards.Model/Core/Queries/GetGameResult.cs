namespace MeanCards.Model.Core.Queries
{
    public class GetGameResult : BaseResult
    {
        public GetGameResult()
        {
        }

        public GetGameResult(string error) : base(error)
        {
        }

        public string Name { get; set; }
        public string Code { get; set; }
    }
}
