namespace MeanCards.Commands
{
    public abstract class BaseResult
    {
        public BaseResult()
        {
            IsSuccessful = true;
        }

        public BaseResult(string error)
        {
            IsSuccessful = false;
            Error = error;
        }
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
    }
}
