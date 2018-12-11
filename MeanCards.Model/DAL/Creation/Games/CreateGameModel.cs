namespace MeanCards.Model.DAL.Creation.Games
{
    public class CreateGameModel
    {
        public string Code { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public bool ShowAdultContent { get; set; }
        public int PointsLimit { get; set; }
        public string Checkpoint { get; set; }
    }
}
