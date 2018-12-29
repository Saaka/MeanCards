namespace MeanCards.Model.Core.Games
{
    public class CreateGame
    {
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public int UserId { get; set; }
        public bool ShowAdultContent { get; set; }
        public int PointsLimit { get; set; }
    }
}
