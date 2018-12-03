using MeanCards.Common.Constants;
using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
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
        protected readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        protected readonly IGamesRepository gamesRepository;
        protected readonly IGameRoundsRepository gameRoundsRepository;
        protected readonly IPlayersRepository playersRepository;
        protected readonly IPlayerCardsRepository playerCardsRepository;
        protected readonly IQuestionCardsRepository questionCardsRepository;
        protected readonly IAnswerCardsRepository answerCardsRepository;
        protected readonly ICodeGenerator codeGenerator;

        public CreateGameHandler(IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGamesRepository gamesRepository,
            IGameRoundsRepository gameRoundsRepository,
            IPlayersRepository playersRepository,
            IPlayerCardsRepository playerCardsRepository,
            IQuestionCardsRepository questionCardsRepository,
            IAnswerCardsRepository answerCardsRepository,
            ICodeGenerator codeGenerator)
        {
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
                var gameCode = codeGenerator.Generate();

                var game = await CreateGameModel(request, gameCode);
                var player = await CreatePlayerModel(game.GameId, request.OwnerId);
                var gameRound = await CreateFirstRound(game, player);
                await CreatePlayerAnswerCards(game, player);

                transaction.CommitTransaction();

                return new CreateGameResult
                {
                    GameId = game.GameId,
                    Code = gameCode
                };
            }
        }

        private async Task CreatePlayerAnswerCards(GameModel game, PlayerModel player)
        {
            var cards = await answerCardsRepository.GetRandomAnswerCardsForGame(game.GameId, GameConstants.StartingCardsCount);

            var playerCards = cards.Select(c => new CreatePlayerCardModel
            {
                PlayerId = player.PlayerId,
                AnswerCardId = c.AnswerCardId
            }).ToList();

            await playerCardsRepository.CreatePlayerCards(playerCards);
        }

        private async Task<GameRoundModel> CreateFirstRound(GameModel game, PlayerModel player)
        {
            var questionCard = await questionCardsRepository.GetRandomQuestionCardForGame(game.GameId);

            var gameRound = await gameRoundsRepository.CreateGameRound(new CreateGameRoundModel
            {
                GameId = game.GameId,
                RoundOwnerId = player.PlayerId,
                QuestionCardId = questionCard.QuestionCardId,
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
                ShowAdultContent = request.ShowAdultContent
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
