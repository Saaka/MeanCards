using System;

namespace MeanCards.Model.DTO.Games
{
    public class GameCheckpointModel
    {
        public int GameCheckpointId { get; set; }
        public int GameId { get; set; }
        public string Code { get; set; }
        public string OperationType { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
