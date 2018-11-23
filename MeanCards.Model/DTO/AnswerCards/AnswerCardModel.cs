namespace MeanCards.Model.DTO.AnswerCards
{
    public class AnswerCardModel
    {
        public int AnswerCardId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public bool IsAdultContent { get; set; }
    }
}
