using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games.Base;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.ValidationRules
{
    public interface IRoundOwnerRule
    {
        Task<ValidatorResult> Validate<T>(T request)
            where T : IGameRoundRequest, IPlayerRequest;
    }

    public class RoundOwnerRule : IRoundOwnerRule
    {
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IPlayersRepository playersRepository;

        public RoundOwnerRule(
            IGameRoundsRepository gameRoundsRepository,
            IPlayersRepository playersRepository)
        {
            this.gameRoundsRepository = gameRoundsRepository;
            this.playersRepository = playersRepository;
        }

        public async Task<ValidatorResult> Validate<T>(T request) where T : IGameRoundRequest, IPlayerRequest
        {
            var round = await gameRoundsRepository.GetGameRound(request.GameId, request.GameRoundId);
            var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);

            if (round.OwnerPlayerId != player.PlayerId)
                return new ValidatorResult(ValidatorErrors.Games.InvalidUserAction);

            return new ValidatorResult();
        }
    }
}
