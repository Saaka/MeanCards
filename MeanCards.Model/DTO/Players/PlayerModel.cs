namespace MeanCards.Model.DTO.Players
{
    public class PlayerModel
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public int Number { get; set; }
        public int Points { get; set; }
    }
}
