using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Validators;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface IEndSubmissionsHandler
    {
        Task<EndSubmissionsResult> Handle(EndSubmissions request);
    }

    public class EndSubmissionsHandler : IEndSubmissionsHandler
    {
        private readonly IRequestValidator<EndSubmissions> requestValidator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;

        public EndSubmissionsHandler(
            IRequestValidator<EndSubmissions> requestValidator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGameRoundsRepository gameRoundsRepository,
            IGameCheckpointUpdater gameCheckpointUpdater)
        {
            this.requestValidator = requestValidator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gameRoundsRepository = gameRoundsRepository;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
        }

        public async Task<EndSubmissionsResult> Handle(EndSubmissions request)
        {
            using (var transaction = repositoryTransactionsFactory.CreateTransaction())
            {
                var validationResult = await requestValidator.Validate(request);
                if (!validationResult.IsSuccessful)
                    return new EndSubmissionsResult(validationResult.Error);

                var updated = await gameRoundsRepository
                    .UpdateGameRoundStatus(request.GameRoundId, Common.Enums.GameRoundStatusEnum.Selection);
                if (!updated)
                    return new EndSubmissionsResult(GameErrors.CouldNotEndAnswersSubmissions);

                var checkpoint = await gameCheckpointUpdater.Update(request.GameId, nameof(EndSubmissions));
                transaction.CommitTransaction();

                return new EndSubmissionsResult();
            }
        }
    }
}
