using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Validators.Games.ValidationRules;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class SelectAnswerValidator : IRequestValidator<SelectAnswer>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;
        private readonly IRoundOwnerRule roundOwnerRule;
        private readonly IPlayerAnswersRepository playerAnswersRepository;

        public SelectAnswerValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator,
            IRoundOwnerRule roundOwnerRule,
            IPlayerAnswersRepository playerAnswersRepository)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
            this.roundOwnerRule = roundOwnerRule;
            this.playerAnswersRepository = playerAnswersRepository;
        }

        public async Task<ValidatorResult> Validate(SelectAnswer request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);

            var isOwnerResult = await roundOwnerRule.Validate(request);
            if (!isOwnerResult.IsSuccessful)
                return new ValidatorResult(isOwnerResult.Error);

            var answerExists = await playerAnswersRepository.IsAnswerSubmitted(request.PlayerAnswerId, request.GameRoundId);
            if (!answerExists)
                return new ValidatorResult(ValidatorErrors.Games.PlayerAnswerDoesNotExists);

            return new ValidatorResult();
        }
    }
}
