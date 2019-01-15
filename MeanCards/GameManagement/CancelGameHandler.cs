using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Validators;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface ICancelGameHandler
    {
        Task<CancelGameResult> Handle(CancelGame request);
    }

    public class CancelGameHandler : ICancelGameHandler
    {
        private readonly IRequestValidator<CancelGame> validator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        private readonly IGamesRepository gamesRepository;
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;

        public CancelGameHandler(
            IRequestValidator<CancelGame> requestValidator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGamesRepository gamesRepository,
            IGameCheckpointUpdater gameCheckpointUpdater)
        {
            this.validator = requestValidator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gamesRepository = gamesRepository;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
        }

        public async Task<CancelGameResult> Handle(CancelGame request)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var validatorResult = await validator.Validate(request);
                if (!validatorResult.IsSuccessful)
                    return new CancelGameResult(validatorResult.Error);

                var ended = await gamesRepository.CancelGame(request.GameId);
                if (!ended)
                    return new CancelGameResult(GameErrors.GameCouldNotBeCancelled);

                var checkpoint = await gameCheckpointUpdater.Update(request.GameId, nameof(SkipRound));
                transaction.CommitTransaction();

                return new CancelGameResult();
            }
        }
    }
}
