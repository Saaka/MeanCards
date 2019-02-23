using MeanCards.ViewModel.Game.Models;

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

        public GameData Game { get; set; }
    }
}
