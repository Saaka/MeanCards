namespace MeanCards.Commands.Games
{
    public class CreateGameResult : BaseResult
    {
        public CreateGameResult()
        {
        }

        public CreateGameResult(string error) : base(error)
        {
        }

        public int GameId { get; set; }
        public string Code { get; set; }
    }
}
