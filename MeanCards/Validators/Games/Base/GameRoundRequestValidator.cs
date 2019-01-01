using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games.Base;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.Base
{
    public class GameRoundRequestValidator : IRequestValidator<IGameRoundRequest>
    {
        private readonly IGameRoundsRepository gameRoundsRepository;

        public GameRoundRequestValidator(
            IGameRoundsRepository gameRoundsRepository)
        {
            this.gameRoundsRepository = gameRoundsRepository;
        }

        public async Task<ValidatorResult> Validate(IGameRoundRequest request)
        {
            if (request.GameRoundId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameRoundIdRequired);

            var round = await gameRoundsRepository.GetGameRound(request.GameId, request.GameRoundId);
            if (round == null)
                return new ValidatorResult(ValidatorErrors.Games.RoundNotLinkedWithGame);
            if (!round.IsActive)
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);

            return new ValidatorResult();
        }
    }
}
