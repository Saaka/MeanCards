using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DAL.Creation.Players;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface ICreateGameHandler
    {
        Task<CreateGameResult> Handle(CreateGame request);
    }

    public class CreateGameHandler : ICreateGameHandler
    {
        protected readonly IGamesRepository gamesRepository;
        protected readonly IGameRoundsRepository gameRoundsRepository;
        protected readonly IPlayersRepository playersRepository;
        protected readonly ICodeGenerator codeGenerator;

        public CreateGameHandler(IGamesRepository gamesRepository,
            IGameRoundsRepository gameRoundsRepository,
            IPlayersRepository playersRepository,
            ICodeGenerator codeGenerator)
        {
            this.gamesRepository = gamesRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.playersRepository = playersRepository;
            this.codeGenerator = codeGenerator;
        }

        public async Task<CreateGameResult> Handle(CreateGame request)
        {
            var gameCode = codeGenerator.Generate();

            int gameId = await CreateGameModel(request, gameCode);
            var playerId = await CreatePlayerModel(gameId, request.OwnerId);

            return new CreateGameResult
            {
                GameId = gameId,
                Code = gameCode
            };
        }

        private async Task<int> CreateGameModel(CreateGame request, string gameCode)
        {
            var gameId = await gamesRepository.CreateGame(new CreateGameModel
            {
                Code = gameCode,
                LanguageId = request.LanguageId,
                Name = request.Name,
                OwnerId = request.OwnerId,
                ShowAdultContent = request.ShowAdultContent
            });
            return gameId;
        }

        private async Task<int> CreatePlayerModel(int gameId, int userId)
        {
            var playerId = await playersRepository.CreatePlayer(new CreatePlayerModel
            {
                GameId = gameId,
                UserId = userId
            });

            return playerId;
        }
    }
}
