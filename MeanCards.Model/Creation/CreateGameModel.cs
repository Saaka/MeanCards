namespace MeanCards.Model.Creation
{
    public class CreateGameModel
    {
        public string GameCode { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public bool ShowAdultContent { get; set; }
    }
}
