using MeanCards.ViewModel.Game.Models.GameViewModels;
using System.Collections.Generic;

namespace MeanCards.ViewModel.Game
{
    public class GetGameResponse
    {
        public GameData Game { get; set; }
        public List<PlayerData> Players { get; set; }
    }
}
