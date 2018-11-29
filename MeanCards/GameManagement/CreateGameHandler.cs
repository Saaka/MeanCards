using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
using System;
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
        protected readonly IQuestionCardsRepository questionCardsRepository;
        protected readonly ICodeGenerator codeGenerator;

        public CreateGameHandler(IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGamesRepository gamesRepository,
            IGameRoundsRepository gameRoundsRepository,
            IPlayersRepository playersRepository,
            IQuestionCardsRepository questionCardsRepository,
            ICodeGenerator codeGenerator)
        {
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gamesRepository = gamesRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.playersRepository = playersRepository;
            this.questionCardsRepository = questionCardsRepository;
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

                transaction.CommitTransaction();

                return new CreateGameResult
                {
                    GameId = game.GameId,
                    Code = gameCode
                };
            }
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
