namespace MeanCards.Model.DAL
{
    public class RepositoryResult<T>
        where T : class
    {
        public RepositoryResult()
        {
            IsSuccessful = true;
        }

        public RepositoryResult(string error)
        {
            IsSuccessful = false;
            Error = error;
        }

        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public T Model { get; set; }
    }
}
