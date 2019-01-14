using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Validators;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface IEndGameHandler
    {
        Task<EndGameResult> Handle(EndGame request);
    }

    public class EndGameHandler : IEndGameHandler
    {
        private readonly IRequestValidator<EndGame> validator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;

        public EndGameHandler(
            IRequestValidator<EndGame> requestValidator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGameCheckpointUpdater gameCheckpointUpdater)
        {
            this.validator = requestValidator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
        }

        public async Task<EndGameResult> Handle(EndGame request)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var validatorResult = await validator.Validate(request);
                if (!validatorResult.IsSuccessful)
                    return new EndGameResult(validatorResult.Error);


                var checkpoint = await gameCheckpointUpdater.Update(request.GameId, nameof(SkipRound));
                transaction.CommitTransaction();

                return new EndGameResult();
            }
        }
    }
}
