using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class JoinGameValidator : IRequestValidator<JoinGame>
    {
        private readonly IGamesRepository gamesRepository;
        private readonly IPlayersRepository playersRepository;
        private readonly IUsersRepository usersRepository;

        public JoinGameValidator(
            IGamesRepository gamesRepository,
            IPlayersRepository playersRepository,
            IUsersRepository usersRepository)
        {
            this.gamesRepository = gamesRepository;
            this.playersRepository = playersRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<ValidatorResult> Validate(JoinGame request)
        {
            if (request.GameId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameIdRequired);
            if (request.UserId == 0)
                return new ValidatorResult(ValidatorErrors.Games.UserIdRequired);

            if (!await usersRepository.ActiveUserExists(request.UserId))
                return new ValidatorResult(ValidatorErrors.Users.UserIdNotFound);

            var game = await gamesRepository.GetGameById(request.GameId);
            if (game == null || game.Status != Common.Enums.GameStatusEnum.InProgress)
                return new ValidatorResult(ValidatorErrors.Games.GameNotFoundOrInactive);

            var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);
            if (player != null && player.IsActive)
                return new ValidatorResult(ValidatorErrors.Games.UserAlreadyJoined);

            return new ValidatorResult();
        }
    }
}
