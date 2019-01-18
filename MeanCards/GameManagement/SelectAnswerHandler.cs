using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Validators;
using System;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface ISelectAnswerHandler
    {
        Task<SelectAnswerResult> Handle(SelectAnswer request);
    }

    public class SelectAnswerHandler : ISelectAnswerHandler
    {
        private readonly IRequestValidator<SelectAnswer> requestValidator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IPlayerAnswersRepository playerAnswersRepository;
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;

        public SelectAnswerHandler(
            IRequestValidator<SelectAnswer> requestValidator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGameRoundsRepository gameRoundsRepository,
            IPlayerAnswersRepository playerAnswersRepository,
            IGameCheckpointUpdater gameCheckpointUpdater)
        {
            this.requestValidator = requestValidator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gameRoundsRepository = gameRoundsRepository;
            this.playerAnswersRepository = playerAnswersRepository;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
        }

        public async Task<SelectAnswerResult> Handle(SelectAnswer request)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var validationResult = await requestValidator.Validate(request);
                if (!validationResult.IsSuccessful)
                    return new SelectAnswerResult(validationResult.Error);

                var answerSelected = await playerAnswersRepository.MarkAnswerAsSelected(request.PlayerAnswerId);
                if (!answerSelected)
                    return new SelectAnswerResult(GameErrors.SelectAnswerFailed);

                var pointsAddedResult = await playerAnswersRepository.AddPointForAnswer(request.PlayerAnswerId, request.GameRoundId);
                if (!pointsAddedResult.IsSuccessful)
                    return new SelectAnswerResult(GameErrors.CouldNotAddPointToPlayer);



                var checkpoint = await gameCheckpointUpdater.Update(request.GameId, nameof(SelectAnswer));
                transaction.CommitTransaction();

                return new SelectAnswerResult();
            }
        }
    }
}
