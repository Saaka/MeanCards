using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class JoinGameValidator : IRequestValidator<JoinGame>
    {
        private readonly IGamesRepository gamesRepository;
        private readonly IUsersRepository usersRepository;

        public JoinGameValidator(
            IGamesRepository gamesRepository,
            IUsersRepository usersRepository)
        {
            this.gamesRepository = gamesRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<ValidatorResult> Validate(JoinGame request)
        {
            if (request.GameId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameIdRequired);
            if (request.UserId == 0)
                return new ValidatorResult(ValidatorErrors.Games.UserIdRequired);

            if (!await gamesRepository.ActiveGameExists(request.GameId))
                return new ValidatorResult(ValidatorErrors.Games.GameNotFoundOrInactive);
            if (!await usersRepository.ActiveUserExists(request.UserId))
                return new ValidatorResult(ValidatorErrors.Users.UserIdNotFound);
            if (await gamesRepository.IsUserInGame(request.GameId, request.UserId))
                return new ValidatorResult(ValidatorErrors.Games.UserAlreadyJoined);

            return new ValidatorResult();
        }
    }
}
