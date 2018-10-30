namespace MeanCards.Model.DataModels
{
    public class Player
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }

        public Game Game { get; set; }
    }
}
