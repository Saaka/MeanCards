using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DTO.Games;
using MeanCards.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface ISkipRoundHandler
    {
        Task<SkipRoundResult> Handle(SkipRound request);
    }

    public class SkipRoundHandler : ISkipRoundHandler
    {
        private readonly IRequestValidator<SkipRound> validator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IPlayersRepository playersRepository;
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;
        private readonly INextGameRoundOwnerProvider nextGameRoundOwnerProvider;
        private readonly IQuestionCardsRepository questionCardsRepository;
        private readonly IPlayerCardsRepository playerCardsRepository;
        private readonly IAnswerCardsRepository answerCardsRepository;

        public SkipRoundHandler(
            IRequestValidator<SkipRound> validator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGameRoundsRepository gameRoundsRepository,
            IPlayersRepository playersRepository,
            IGameCheckpointUpdater gameCheckpointUpdater,
            INextGameRoundOwnerProvider nextGameRoundOwnerProvider,
            IQuestionCardsRepository questionCardsRepository,
            IPlayerCardsRepository playerCardsRepository,
            IAnswerCardsRepository answerCardsRepository)
        {
            this.validator = validator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gameRoundsRepository = gameRoundsRepository;
            this.playersRepository = playersRepository;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
            this.nextGameRoundOwnerProvider = nextGameRoundOwnerProvider;
            this.questionCardsRepository = questionCardsRepository;
            this.playerCardsRepository = playerCardsRepository;
            this.answerCardsRepository = answerCardsRepository;
        }

        public async Task<SkipRoundResult> Handle(SkipRound request)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var validatorResult = await validator.Validate(request);
                if (!validatorResult.IsSuccessful)
                    return new SkipRoundResult(validatorResult.Error);

                var skipped = await gameRoundsRepository
                    .SkipRound(request.GameRoundId);
                if (!skipped)
                    return new SkipRoundResult(GameErrors.GameRoundCouldNotBeSkipped);

                var round = await gameRoundsRepository
                    .GetGameRound(request.GameId, request.GameRoundId);

                var nextOwnerResult = await nextGameRoundOwnerProvider.GetNextOwner(request.GameId, round.OwnerPlayerId);
                if (!nextOwnerResult.IsSuccessful)
                    return new SkipRoundResult(GameErrors.CouldNotFindNextRoundOwner);

                var questionCard = await questionCardsRepository
                    .GetRandomQuestionCardForGame(request.GameId);
                if (questionCard == null)
                    return new SkipRoundResult(GameErrors.NoQuestionCardsAvailable);

                var newRound = await CreateRound(request.GameId, nextOwnerResult.PlayerId, questionCard.QuestionCardId, ++round.Number);

                await FillPlayerCards(request.GameId);

                var checkpoint = await gameCheckpointUpdater.Update(request.GameId, nameof(SkipRound));
                transaction.CommitTransaction();

                return new SkipRoundResult();
            }
        }

        private async Task FillPlayerCards(int gameId)
        {
            var players = await playerCardsRepository.GetPlayersCardsInfo(gameId);

            foreach (var player in players)
            {
                var missingCardCount = GameConstants.StartingPlayerCardsCount - player.PlayerCardsCount;

                if (missingCardCount > 0)
                    await CreatePlayerAnswerCards(gameId, player.PlayerId, missingCardCount);
            }
        }

        private async Task<int> CreatePlayerAnswerCards(int gameId, int playerId, int count)
        {
            var cards = await answerCardsRepository.GetRandomAnswerCardsForGame(gameId, count);

            var playerCards = cards.Select(c => new CreatePlayerCardModel
            {
                PlayerId = playerId,
                AnswerCardId = c.AnswerCardId
            }).ToList();

            return await playerCardsRepository.CreatePlayerCards(playerCards);
        }

        private async Task<GameRoundModel> CreateRound(int gameId, int playerId, int questionCardId, int roundNumber)
        {
            var gameRound = await gameRoundsRepository.CreateGameRound(new CreateGameRoundModel
            {
                GameId = gameId,
                OwnerPlayerId = playerId,
                QuestionCardId = questionCardId,
                RoundNumber = roundNumber
            });

            return gameRound;
        }
    }
}
