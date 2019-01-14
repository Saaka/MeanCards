using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Validators.Games.ValidationRules;
using System;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class EndGameValidator : IRequestValidator<EndGame>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;
        private readonly IGameOwnerRule gameOwnerRule;
        private readonly IGamesRepository gamesRepository;

        public EndGameValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator,
            IGameOwnerRule gameOwnerRule,
            IGamesRepository gamesRepository)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
            this.gameOwnerRule = gameOwnerRule;
            this.gamesRepository = gamesRepository;
        }

        public async Task<ValidatorResult> Validate(EndGame request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);

            var isOwnerResult = await gameOwnerRule.Validate(request);
            if (!isOwnerResult.IsSuccessful)
                return new ValidatorResult(isOwnerResult.Error);

            var gameStatus = await gamesRepository.GetGameStatus(request.GameId);
            if (gameStatus != Common.Enums.GameStatusEnum.InProgress)
                return new ValidatorResult(ValidatorErrors.Games.GameNotFoundOrInactive);

            return new ValidatorResult();
        }
    }
}
