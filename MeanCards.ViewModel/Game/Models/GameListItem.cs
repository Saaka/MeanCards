using System;

namespace MeanCards.ViewModel.Game.Models
{
    public class GameListItem
    {
        public string GameCode { get; set; }
        public string Name { get; set; }
        public int OwnerCode { get; set; }
        public string Owner { get; set; }
        public string OwnerEmail { get; set; }
        public bool AdultContent { get; set; }
        public string LanguageCode { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
