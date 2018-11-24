namespace MeanCards.Model.DTO.Games
{
    public class GameModel
    {
        public int GameId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public byte Status { get; set; }
        public int LanguageId { get; set; }
        public int OwnerId { get; set; }
        public bool ShowAdultContent { get; set; }
    }
}
