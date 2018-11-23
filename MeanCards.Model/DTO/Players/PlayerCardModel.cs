namespace MeanCards.Model.DTO.Players
{
    public class PlayerCardModel
    {
        public int PlayerCardId { get; set; }
        public int PlayerId { get; set; }
        public int AnswerCardId { get; set; }
        public bool IsUsed { get; set; }
    }
}
