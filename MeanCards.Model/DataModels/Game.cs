using System;

namespace MeanCards.Model.DataModels
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int OwnerId { get; set; }
        public bool ShowAdultContent { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
