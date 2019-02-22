using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Validators;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface ISkipRoundHandler : IRequestHandler<SkipRound, SkipRoundResult>
    { }

    public class SkipRoundHandler : ISkipRoundHandler
    {
        private readonly IRequestValidator<SkipRound> validator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        private readonly IGameRoundCreator gameRoundCreator;
        private readonly IPlayerCardsCreator playerCardsCreator;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;

        public SkipRoundHandler(
            IRequestValidator<SkipRound> validator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGameRoundCreator gameRoundCreator,
            IPlayerCardsCreator playerCardsCreator,
            IGameRoundsRepository gameRoundsRepository,
            IGameCheckpointUpdater gameCheckpointUpdater)
        {
            this.validator = validator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gameRoundCreator = gameRoundCreator;
            this.playerCardsCreator = playerCardsCreator;
            this.gameRoundsRepository = gameRoundsRepository;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
        }

        public async Task<SkipRoundResult> Handle(SkipRound request, CancellationToken cancellationToken)
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

                var createNewRoundResult = await gameRoundCreator
                    .CreateRound(request.GameId, request.GameRoundId);
                if (!createNewRoundResult.IsSuccessful)
                    return new SkipRoundResult(createNewRoundResult.Error);

                var createCardsResult = await playerCardsCreator
                    .CreateCards(request.GameId);
                if (!createCardsResult.IsSuccessful)
                    return new SkipRoundResult(createCardsResult.Error);

                var checkpoint = await gameCheckpointUpdater.Update(request.GameId, nameof(SkipRound));
                transaction.CommitTransaction();

                return new SkipRoundResult();
            }
        }
    }
}
