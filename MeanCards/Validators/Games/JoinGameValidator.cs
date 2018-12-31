using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class JoinGameValidator : IRequestValidator<JoinGame>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;
        private readonly IGamesRepository gamesRepository;
        private readonly IPlayersRepository playersRepository;

        public JoinGameValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator,
            IGamesRepository gamesRepository,
            IPlayersRepository playersRepository)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
            this.gamesRepository = gamesRepository;
            this.playersRepository = playersRepository;
        }

        public async Task<ValidatorResult> Validate(JoinGame request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);

            var gameStatus = await gamesRepository.GetGameStatus(request.GameId);
            if (gameStatus != Common.Enums.GameStatusEnum.InProgress)
                return new ValidatorResult(ValidatorErrors.Games.GameNotFoundOrInactive);

            var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);
            if (player != null && player.IsActive)
                return new ValidatorResult(ValidatorErrors.Games.UserAlreadyJoined);

            return new ValidatorResult();
        }
    }
}
