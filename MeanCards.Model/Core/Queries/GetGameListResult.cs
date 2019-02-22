using MeanCards.ViewModel.Game.Models;
using System.Collections.Generic;

namespace MeanCards.Model.Core.Queries
{
    public class GetGameListResult : BaseResult
    {
        public GetGameListResult()
        {
        }

        public GetGameListResult(string error) : base(error)
        {
        }

        public List<GameListItem> List { get; set; }
    }
}
