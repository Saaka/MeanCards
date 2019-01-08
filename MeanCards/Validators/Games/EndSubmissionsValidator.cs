using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class EndSubmissionsValidator : IRequestValidator<EndSubmissions>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;
        private readonly IPlayersRepository playersRepository;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IGamesRepository gamesRepository;
        private readonly IPlayerAnswersRepository playerAnswersRepository;

        public EndSubmissionsValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator,
            IPlayersRepository playersRepository,
            IGameRoundsRepository gameRoundsRepository,
            IGamesRepository gamesRepository,
            IPlayerAnswersRepository playerAnswersRepository)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
            this.playersRepository = playersRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.gamesRepository = gamesRepository;
            this.playerAnswersRepository = playerAnswersRepository;
        }

        public async Task<ValidatorResult> Validate(EndSubmissions request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);

            var round = await gameRoundsRepository.GetGameRound(request.GameId, request.GameRoundId);
            if (round.Status != Common.Enums.GameRoundStatusEnum.InProgress)
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);

            var game = await gamesRepository.GetGameById(request.GameId);
            var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);
            if (game.OwnerId != request.UserId && round.OwnerPlayerId != player.PlayerId)
                return new ValidatorResult(ValidatorErrors.Games.InvalidUserAction);

            var numberOfAnswers = await playerAnswersRepository.GetNumberOfAnswers(request.GameRoundId);
            if (numberOfAnswers < GameConstants.MinimumAnswersCount)
                return new ValidatorResult(ValidatorErrors.Games.NotEnoughSubmittedAnswers);

            return new ValidatorResult();
        }
    }
}
