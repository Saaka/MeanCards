using MeanCards.Common.Constants;
using MeanCards.Common.Enums;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Validators.Games.ValidationRules;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class SkipRoundValidator : IRequestValidator<SkipRound>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IGameOrRoundOwnerRule gameOrRoundOwnerRule;

        public SkipRoundValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator,
            IGameRoundsRepository gameRoundsRepository,
            IGameOrRoundOwnerRule gameOrRoundOwnerRule)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
            this.gameRoundsRepository = gameRoundsRepository;
            this.gameOrRoundOwnerRule = gameOrRoundOwnerRule;
        }

        public async Task<ValidatorResult> Validate(SkipRound request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);

            var round = await gameRoundsRepository.GetGameRound(request.GameId, request.GameRoundId);
            if (!statuses.Contains(round.Status))
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);

            var isOwnerResult = await gameOrRoundOwnerRule.Validate(request);
            if (!isOwnerResult.IsSuccessful)
                return new ValidatorResult(isOwnerResult.Error);

            return new ValidatorResult();
        }

        GameRoundStatusEnum[] statuses = new GameRoundStatusEnum[]
        {
            GameRoundStatusEnum.Pending,
            GameRoundStatusEnum.InProgress,
            GameRoundStatusEnum.Selection
        };
    }
}
