using System;
using System.Collections.Generic;
using System.Text;

namespace MeanCards.Model.DTO.Games
{
    public class GameRoundSimpleModel
    {
        public int GameRoundId { get; set; }
        public string GameRoundCode { get; set; }
        public int GameId { get; set; }
    }
}
