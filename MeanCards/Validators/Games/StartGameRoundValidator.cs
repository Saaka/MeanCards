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
            if (request.GameId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameIdRequired);
            if (request.GameRoundId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameRoundIdRequired);

            var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);
            if (player == null)
                return new ValidatorResult(ValidatorErrors.Players.UserNotLinkedWithPlayer);

            var round = await gameRoundsRepository.GetCurrentGameRound(request.GameId);
            if (round == null || round.Status != Common.Enums.GameRoundStatusEnum.Pending)
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);
                        
            var game = await gamesRepository.GetGameById(request.GameId);
            if(!(game.OwnerId == request.UserId || round.OwnerPlayerId == player.PlayerId))
                return new ValidatorResult(ValidatorErrors.Games.UserCantStartRound);
            
            return new ValidatorResult();
        }
    }
}
