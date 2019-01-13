using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Validators;
using System;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface ISubmitAnswerHandler
    {
        Task<SubmitAnswerResult> Handle(SubmitAnswer request);
    }

    public class SubmitAnswerHandler : ISubmitAnswerHandler
    {
        private readonly IRequestValidator<SubmitAnswer> requestValidator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        private readonly IPlayerAnswersRepository playerAnswerRepository;
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;
        private readonly IPlayersRepository playersRepository;
        private readonly IPlayerCardsRepository playerCardsRepository;

        public SubmitAnswerHandler(
            IRequestValidator<SubmitAnswer> requestValidator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IPlayerAnswersRepository playerAnswerRepository,
            IGameCheckpointUpdater gameCheckpointUpdater,
            IPlayersRepository playersRepository,
            IPlayerCardsRepository playerCardsRepository)
        {
            this.requestValidator = requestValidator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.playerAnswerRepository = playerAnswerRepository;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
            this.playersRepository = playersRepository;
            this.playerCardsRepository = playerCardsRepository;
        }

        public async Task<SubmitAnswerResult> Handle(SubmitAnswer request)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var validatorResult = await requestValidator.Validate(request);
                if (!validatorResult.IsSuccessful)
                    return new SubmitAnswerResult(validatorResult.Error);

                var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);
                int playerAnswerId = await SubmitAnswer(request, player);
                if (playerAnswerId == 0)
                    return new SubmitAnswerResult(GameErrors.SubmitAnswerFailed);

                await gameCheckpointUpdater.Update(request.GameId, nameof(Model.Core.Games.SubmitAnswer));

                transaction.CommitTransaction();

                return new SubmitAnswerResult();
            }
        }

        private async Task<int> SubmitAnswer(SubmitAnswer request, Model.DTO.Players.PlayerModel player)
        {
            var submitModel = new CreatePlayerAnswerModel
            {
                GameRoundId = request.GameRoundId,
                PlayerId = player.PlayerId,
                AnswerCardId = await GetAnswerCardId(request.PlayerCardId),
                SecondaryAnswerCardId = await GetSecondaryAnswerCardId(request.SecondPlayerCardId)
            };
            var answerId =  await playerAnswerRepository.CreatePlayerAnswer(submitModel);

            await MarkCardAsUsed(request.PlayerCardId);
            await MarkCardAsUsed(request.SecondPlayerCardId);

            return answerId;
        }

        private async Task MarkCardAsUsed(int? playerCardId)
        {
            if (!playerCardId.HasValue)
                return;

            await playerCardsRepository.MarkCardAsUsed(playerCardId.Value);   
        }

        private async Task<int> GetAnswerCardId(int playerCardId)
        {
            return await playerCardsRepository.GetAnswerCardIdForPlayerCard(playerCardId);
        }

        private async Task<int?> GetSecondaryAnswerCardId(int? playerCardId)
        {
            if (!playerCardId.HasValue)
                return null;

            return await playerCardsRepository.GetAnswerCardIdForPlayerCard(playerCardId.Value);
        }
    }
}
