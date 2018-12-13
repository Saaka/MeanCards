using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class StartGameRoundValidator : IRequestValidator<StartGameRound>
    {
        private readonly IPlayersRepository playersRepository;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IGamesRepository gamesRepository;

        public StartGameRoundValidator(
            IPlayersRepository playersRepository,
            IGameRoundsRepository gameRoundsRepository,
            IGamesRepository gamesRepository)
        {
            this.playersRepository = playersRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.gamesRepository = gamesRepository;
        }

        public async Task<ValidatorResult> Validate(StartGameRound request)
        {
            if (request.UserId == 0)
                return new ValidatorResult(ValidatorErrors.Games.UserIdRequired);
            if (request.GameRoundId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameRoundIdRequired);

            if (!await playersRepository.IsUserLinkedWithPlayer(request.UserId, request.GameId))
                return new ValidatorResult(ValidatorErrors.Players.UserNotLinkedWithPlayer);
            if (!await gameRoundsRepository.IsRoundInGame(request.GameId, request.GameRoundId))
                return new ValidatorResult(ValidatorErrors.Games.RoundNotLinkedWithGame);

            if (!await gameRoundsRepository.IsGameRoundPending(request.GameRoundId))
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);
            if (!(await gameRoundsRepository.IsGameRoundOwner(request.GameRoundId, request.UserId)
                || await gamesRepository.IsGameOwner(request.GameRoundId, request.UserId)))
                return new ValidatorResult(ValidatorErrors.Games.UserCantStartRound);

            return new ValidatorResult();
        }
    }
}
