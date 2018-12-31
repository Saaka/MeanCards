using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games.Base;
using System;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.Base
{
    public class GameRequestValidator : IRequestValidator<IGameRequest>
    {
        private readonly IGamesRepository gamesRepository;

        public GameRequestValidator(
            IGamesRepository gamesRepository)
        {
            this.gamesRepository = gamesRepository;
        }

        public async Task<ValidatorResult> Validate(IGameRequest request)
        {
            if (request.GameId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameIdRequired);

            var game = await gamesRepository.GetGameById(request.GameId);
            if (game == null || !game.IsActive)
                return new ValidatorResult(ValidatorErrors.Games.GameNotFoundOrInactive);

            return new ValidatorResult();
        }
    }
}
