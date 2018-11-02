using System;

namespace MeanCards.Model.DataModels
{
    public class Game
    {
        public int GameId { get; set; }
        public byte GameStatus { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public bool ShowAdultContent { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public Language Language { get; set; }
        public User Owner { get; set; }
    }
}
