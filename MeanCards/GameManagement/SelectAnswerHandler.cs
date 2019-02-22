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
    public interface ISelectAnswerHandler : IRequestHandler<SelectAnswer, SelectAnswerResult>
    { }

    public class SelectAnswerHandler : ISelectAnswerHandler
    {
        private readonly IRequestValidator<SelectAnswer> requestValidator;
        private readonly IRepositoryTransactionsFactory repositoryTransactionsFactory;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IGamesRepository gamesRepository;
        private readonly IPlayerAnswersRepository playerAnswersRepository;
        private readonly IPlayerCardsCreator playerCardsCreator;
        private readonly IGameRoundCreator gameRoundCreator;
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;

        public SelectAnswerHandler(
            IRequestValidator<SelectAnswer> requestValidator,
            IRepositoryTransactionsFactory repositoryTransactionsFactory,
            IGameRoundsRepository gameRoundsRepository,
            IGamesRepository gamesRepository,
            IPlayerAnswersRepository playerAnswersRepository,
            IPlayerCardsCreator playerCardsCreator,
            IGameRoundCreator gameRoundCreator,
            IGameCheckpointUpdater gameCheckpointUpdater)
        {
            this.requestValidator = requestValidator;
            this.repositoryTransactionsFactory = repositoryTransactionsFactory;
            this.gameRoundsRepository = gameRoundsRepository;
            this.gamesRepository = gamesRepository;
            this.playerAnswersRepository = playerAnswersRepository;
            this.playerCardsCreator = playerCardsCreator;
            this.gameRoundCreator = gameRoundCreator;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
        }

        public async Task<SelectAnswerResult> Handle(SelectAnswer request, CancellationToken cancellationToken)
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

                var updateWinnerResult = await gameRoundsRepository.SelectRoundWinner(request.GameRoundId, pointsAddedResult.PlayerPoints.PlayerId);
                if (!updateWinnerResult)
                    return new SelectAnswerResult(GameErrors.RoundWinnerSelectionFailed);

                var game = await gamesRepository.GetGameById(request.GameId);
                if (pointsAddedResult.PlayerPoints.Points < game.PointsLimit)
                {
                    var createNewRoundResult = await gameRoundCreator
                        .CreateRound(request.GameId, request.GameRoundId);
                    if (!createNewRoundResult.IsSuccessful)
                        return new SelectAnswerResult(createNewRoundResult.Error);

                    var createCardsResult = await playerCardsCreator.CreateCards(request.GameId);
                    if (!createCardsResult.IsSuccessful)
                        return new SelectAnswerResult(createCardsResult.Error);
                }
                else
                {
                    var endGameResult = await gamesRepository
                        .EndGame(request.GameId, pointsAddedResult.PlayerPoints.UserId);
                    if (!endGameResult)
                        return new SelectAnswerResult(GameErrors.GameCouldNotBeEnded);
                }

                var checkpoint = await gameCheckpointUpdater.Update(request.GameId, nameof(SelectAnswer));
                transaction.CommitTransaction();

                return new SelectAnswerResult();
            }
        }
    }
}
