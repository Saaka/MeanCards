namespace MeanCards.Validators
{
    public class ValidatorResult
    {
        public ValidatorResult()
        {
            IsSuccessful = true;
        }

        public ValidatorResult(string error)
        {
            Error = error;
        }
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
    }
}
