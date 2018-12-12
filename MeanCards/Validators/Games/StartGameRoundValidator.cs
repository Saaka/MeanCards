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
            if (request.PlayerId == 0)
                return new ValidatorResult(ValidatorErrors.Games.PlayerIdRequired);
            if (request.GameRoundId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameRoundIdRequired);

            if (!await playersRepository.IsUserLinkedWithPlayer(request.UserId, request.PlayerId))
                return new ValidatorResult(ValidatorErrors.Players.UserNotLinkedWithPlayer);
            if (!await gameRoundsRepository.IsGameRoundPending(request.GameRoundId))
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);
            if (!(await gameRoundsRepository.IsGameRoundOwner(request.GameRoundId, request.PlayerId)
                || await gamesRepository.IsGameOwner(request.GameRoundId, request.UserId)))
                return new ValidatorResult(ValidatorErrors.Games.UserCantStartRound);
            if (!await gameRoundsRepository.IsRoundInGame(request.GameId, request.GameRoundId))
                return new ValidatorResult(ValidatorErrors.Games.RoundNotLinkedWithGame);

            return new ValidatorResult();
        }
    }
}
