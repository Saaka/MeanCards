using MeanCards.Common.Constants;
using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
using MeanCards.Validators;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface ICreateGameHandler : IRequestHandler<CreateGame, CreateGameResult>
    { }

    public class CreateGameHandler : ICreateGameHandler
    {
        private readonly IRequestValidator<CreateGame> requestValidator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        private readonly IGameRoundCreator gameRoundCreator;
        private readonly IPlayerCardsCreator playerCardsCreator;
        private readonly IGamesRepository gamesRepository;
        private readonly IPlayersRepository playersRepository;
        private readonly ICodeGenerator codeGenerator;
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;

        public CreateGameHandler(
            IRequestValidator<CreateGame> requestValidator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGameRoundCreator gameRoundCreator,
            IPlayerCardsCreator playerCardsCreator,
            IGamesRepository gamesRepository,
            IPlayersRepository playersRepository,
            ICodeGenerator codeGenerator,
            IGameCheckpointUpdater gameCheckpointUpdater)
        {
            this.requestValidator = requestValidator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gameRoundCreator = gameRoundCreator;
            this.playerCardsCreator = playerCardsCreator;
            this.gamesRepository = gamesRepository;
            this.playersRepository = playersRepository;
            this.codeGenerator = codeGenerator;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
        }

        public async Task<CreateGameResult> Handle(CreateGame request, CancellationToken cancellationToken)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var validationResult = await requestValidator.Validate(request);
                if (!validationResult.IsSuccessful)
                    return new CreateGameResult(validationResult.Error);

                var gameCode = codeGenerator.Generate();

                var game = await CreateGame(request, gameCode);

                var player = await CreatePlayer(game.GameId, request.UserId);

                var createRoundResult = await gameRoundCreator
                    .CreateFirstRound(game.GameId, player.PlayerId);
                if (!createRoundResult.IsSuccessful)
                    return new CreateGameResult(createRoundResult.Error);

                var createCardsResult = await playerCardsCreator.CreateCardsForPlayer(game.GameId, player.PlayerId);
                if (!createCardsResult.IsSuccessful)
                    return new CreateGameResult(createCardsResult.Error);

                var checkpoint = await gameCheckpointUpdater.Update(game.GameId, nameof(CreateGame));

                transaction.CommitTransaction();

                return new CreateGameResult
                {
                    GameId = game.GameId,
                    PlayerId = player.PlayerId,
                    Code = gameCode,
                    Checkpoint = checkpoint
                };
            }
        }

        private async Task<GameModel> CreateGame(CreateGame request, string gameCode)
        {
            var game = await gamesRepository.CreateGame(new CreateGameModel
            {
                Code = gameCode,
                LanguageId = request.LanguageId,
                Name = request.Name,
                OwnerId = request.UserId,
                ShowAdultContent = request.ShowAdultContent,
                PointsLimit = request.PointsLimit > 0 ? request.PointsLimit : GameConstants.DefaultPointsLimit
            });
            return game;
        }

        private async Task<PlayerModel> CreatePlayer(int gameId, int userId)
        {
            var player = await playersRepository.CreatePlayer(new CreatePlayerModel
            {
                GameId = gameId,
                UserId = userId,
                Number = 1
            });

            return player;
        }
    }
}
