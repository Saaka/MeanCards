using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Validators;
using System;
using System.Collections.Generic;
using System.Text;
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
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;

        public SubmitAnswerHandler(
            IRequestValidator<SubmitAnswer> requestValidator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGameCheckpointUpdater gameCheckpointUpdater)
        {
            this.requestValidator = requestValidator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
        }

        public async Task<SubmitAnswerResult> Handle(SubmitAnswer request)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var validatorResult = await requestValidator.Validate(request);
                if (!validatorResult.IsSuccessful)
                    return new SubmitAnswerResult(validatorResult.Error);


                await gameCheckpointUpdater.Update(request.GameId, nameof(SubmitAnswer));

                transaction.CommitTransaction();

                return new SubmitAnswerResult();
            }
        }
    }
}
