using MeanCards.Common.Constants;
using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
using MeanCards.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface ICreateGameHandler
    {
        Task<CreateGameResult> Handle(CreateGame request);
    }

    public class CreateGameHandler : ICreateGameHandler
    {
        private readonly IRequestValidator<CreateGame> requestValidator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        private readonly IGamesRepository gamesRepository;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IPlayersRepository playersRepository;
        private readonly IPlayerCardsRepository playerCardsRepository;
        private readonly IQuestionCardsRepository questionCardsRepository;
        private readonly IAnswerCardsRepository answerCardsRepository;
        private readonly ICodeGenerator codeGenerator;

        public CreateGameHandler(
            IRequestValidator<CreateGame> requestValidator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGamesRepository gamesRepository,
            IGameRoundsRepository gameRoundsRepository,
            IPlayersRepository playersRepository,
            IPlayerCardsRepository playerCardsRepository,
            IQuestionCardsRepository questionCardsRepository,
            IAnswerCardsRepository answerCardsRepository,
            ICodeGenerator codeGenerator)
        {
            this.requestValidator = requestValidator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gamesRepository = gamesRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.playersRepository = playersRepository;
            this.playerCardsRepository = playerCardsRepository;
            this.questionCardsRepository = questionCardsRepository;
            this.answerCardsRepository = answerCardsRepository;
            this.codeGenerator = codeGenerator;
        }

        public async Task<CreateGameResult> Handle(CreateGame request)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var validationResult = await requestValidator.Validate(request);
                if (!validationResult.IsSuccessful)
                    return new CreateGameResult(validationResult.Error);

                var gameCode = codeGenerator.Generate();

                var game = await CreateGameModel(request, gameCode);
                var player = await CreatePlayerModel(game.GameId, request.OwnerId);

                var questionCard = await questionCardsRepository
                    .GetRandomQuestionCardForGame(game.GameId);
                if (questionCard == null)
                    return new CreateGameResult(GameErrors.NoQuestionCardsAvailable);

                var gameRound = await CreateFirstRound(
                    game.GameId, 
                    player.PlayerId, 
                    questionCard.QuestionCardId);

                var cardCount = await CreatePlayerAnswerCards(game, player, GameConstants.StartingCardsCount);
                if (cardCount != GameConstants.StartingCardsCount)
                    return new CreateGameResult(GameErrors.NotEnoughAnswerCards);

                transaction.CommitTransaction();

                return new CreateGameResult
                {
                    GameId = game.GameId,
                    PlayerId = player.PlayerId,
                    Code = gameCode
                };
            }
        }

        private async Task<int> CreatePlayerAnswerCards(GameModel game, PlayerModel player, int count)
        {
            var cards = await answerCardsRepository.GetRandomAnswerCardsForGame(game.GameId, count);

            var playerCards = cards.Select(c => new CreatePlayerCardModel
            {
                PlayerId = player.PlayerId,
                AnswerCardId = c.AnswerCardId
            }).ToList();

            return await playerCardsRepository.CreatePlayerCards(playerCards);
        }

        private async Task<GameRoundModel> CreateFirstRound(int gameId, int playerId, int questionCardId)
        {
            var gameRound = await gameRoundsRepository.CreateGameRound(new CreateGameRoundModel
            {
                GameId = gameId,
                RoundOwnerId = playerId,
                QuestionCardId = questionCardId,
                RoundNumber = 1
            });

            return gameRound;
        }

        private async Task<GameModel> CreateGameModel(CreateGame request, string gameCode)
        {
            var game = await gamesRepository.CreateGame(new CreateGameModel
            {
                Code = gameCode,
                LanguageId = request.LanguageId,
                Name = request.Name,
                OwnerId = request.OwnerId,
                ShowAdultContent = request.ShowAdultContent,
                PointsLimit = request.PointsLimit > 0 ? request.PointsLimit : GameConstants.DefaultPointsLimit
            });
            return game;
        }

        private async Task<PlayerModel> CreatePlayerModel(int gameId, int userId)
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
