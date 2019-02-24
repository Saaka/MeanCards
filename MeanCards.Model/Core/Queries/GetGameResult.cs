using MeanCards.ViewModel.Game.Models.GameViewModels;
using System.Collections.Generic;

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
        public List<PlayerData> Players { get; set; }
    }
}
