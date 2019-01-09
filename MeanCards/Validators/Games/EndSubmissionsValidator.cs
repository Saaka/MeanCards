using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Validators.Games.ValidationRules;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class EndSubmissionsValidator : IRequestValidator<EndSubmissions>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IPlayerAnswersRepository playerAnswersRepository;
        private readonly IGameOrRoundOwnerRule gameOrRoundOwnerRule;

        public EndSubmissionsValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator,
            IGameRoundsRepository gameRoundsRepository,
            IPlayerAnswersRepository playerAnswersRepository,
            IGameOrRoundOwnerRule gameOrRoundOwnerRule)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
            this.gameRoundsRepository = gameRoundsRepository;
            this.playerAnswersRepository = playerAnswersRepository;
            this.gameOrRoundOwnerRule = gameOrRoundOwnerRule;
        }

        public async Task<ValidatorResult> Validate(EndSubmissions request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);

            var round = await gameRoundsRepository.GetGameRound(request.GameId, request.GameRoundId);
            if (round.Status != Common.Enums.GameRoundStatusEnum.InProgress)
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);

            var isOwnerResult = await gameOrRoundOwnerRule.Validate(request);
            if (!isOwnerResult.IsSuccessful)
                return new ValidatorResult(isOwnerResult.Error);

            var numberOfAnswers = await playerAnswersRepository.GetNumberOfAnswers(request.GameRoundId);
            if (numberOfAnswers < GameConstants.MinimumAnswersCount)
                return new ValidatorResult(ValidatorErrors.Games.NotEnoughSubmittedAnswers);

            return new ValidatorResult();
        }
    }
}
