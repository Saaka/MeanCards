using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games.Base;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.ValidationRules
{
    public interface IGameOrRoundOwnerRule
    {
        Task<ValidatorResult> Validate<T>(T request)
            where T : IGameRoundRequest, IPlayerRequest;
    }
    public class GameOrRoundOwnerRule : IGameOrRoundOwnerRule
    {
        private readonly IGamesRepository gamesRepository;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IPlayersRepository playersRepository;

        public GameOrRoundOwnerRule(
            IGamesRepository gamesRepository,
            IGameRoundsRepository gameRoundsRepository,
            IPlayersRepository playersRepository)
        {
            this.gamesRepository = gamesRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.playersRepository = playersRepository;
        }

        public async Task<ValidatorResult> Validate<T>(T request)
            where T: IGameRoundRequest, IPlayerRequest
        {
            var game = await gamesRepository.GetGameById(request.GameId);
            var round = await gameRoundsRepository.GetGameRound(request.GameId, request.GameRoundId);
            var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);

            if (game.OwnerId != request.UserId && round.OwnerPlayerId != player.PlayerId)
                return new ValidatorResult(ValidatorErrors.Games.InvalidUserAction);

            return new ValidatorResult();
        }
    }
}
