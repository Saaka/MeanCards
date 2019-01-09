using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Validators.Games.ValidationRules;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class StartGameRoundValidator : IRequestValidator<StartGameRound>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;
        private readonly IPlayersRepository playersRepository;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IGameOrRoundOwnerRule gameOrRoundOwnerRule;

        public StartGameRoundValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator,
            IPlayersRepository playersRepository,
            IGameRoundsRepository gameRoundsRepository,
            IGameOrRoundOwnerRule gameOrRoundOwnerRule)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
            this.playersRepository = playersRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.gameOrRoundOwnerRule = gameOrRoundOwnerRule;
        }

        public async Task<ValidatorResult> Validate(StartGameRound request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);
            
            var round = await gameRoundsRepository.GetGameRound(request.GameId, request.GameRoundId);
            if (round.Status != Common.Enums.GameRoundStatusEnum.Pending)
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);

            var isOwnerResult = await gameOrRoundOwnerRule.Validate(request);
            if (!isOwnerResult.IsSuccessful)
                return new ValidatorResult(isOwnerResult.Error);

            var playerCount = await playersRepository.GetActivePlayersCount(request.GameId);
            if (playerCount < GameConstants.MinimumPlayersCount)
                return new ValidatorResult(ValidatorErrors.Games.NotEnoughPlayers);

            return new ValidatorResult();
        }
    }
}
