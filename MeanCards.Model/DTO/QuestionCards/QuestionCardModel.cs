namespace MeanCards.Model.DTO.QuestionCards
{
    public class QuestionCardModel
    {
        public int QuestionCardId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public bool IsAdultContent { get; set; }
        public byte NumberOfAnswers { get; set; }
    }
}
