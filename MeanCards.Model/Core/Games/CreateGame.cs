namespace MeanCards.Model.Core.Games
{
    public class CreateGame
    {
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public int OwnerId { get; set; }
        public bool ShowAdultContent { get; set; }
    }
}
