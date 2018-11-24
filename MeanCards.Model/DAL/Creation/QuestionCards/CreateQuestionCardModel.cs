namespace MeanCards.Model.DAL.Creation.QuestionCards
{
    public class CreateQuestionCardModel
    {
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public bool IsAdultContent { get; set; }
        public byte NumberOfAnswers { get; set; }
    }
}
