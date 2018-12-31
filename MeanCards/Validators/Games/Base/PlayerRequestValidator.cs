using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games.Base;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.Base
{
    public class PlayerRequestValidator : IRequestValidator<IPlayerRequest>
    {
        private readonly IPlayersRepository playersRepository;

        public PlayerRequestValidator(
            IPlayersRepository playersRepository)
        {
            this.playersRepository = playersRepository;
        }

        public async Task<ValidatorResult> Validate(IPlayerRequest request)
        {
            if (request.UserId == 0)
                return new ValidatorResult(ValidatorErrors.Games.UserIdRequired);

            var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);
            if (player == null || !player.IsActive)
                return new ValidatorResult(ValidatorErrors.Players.UserNotLinkedWithPlayer);

            return new ValidatorResult();
        }
    }
}
