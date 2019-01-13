using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.Games;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.GameManagement.CoreServices
{
    public interface INextGameRoundOwnerProvider
    {
        Task<GetNextRoundOwnerResult> GetNextOwner(int gameId, int lastRoundOwnerPlayerId);
    }

    public class NextGameRoundOwnerProvider : INextGameRoundOwnerProvider
    {
        private readonly IPlayersRepository playersRepository;

        public NextGameRoundOwnerProvider(IPlayersRepository playersRepository)
        {
            this.playersRepository = playersRepository;
        }

        public async Task<GetNextRoundOwnerResult> GetNextOwner(int gameId, int lastRoundOwnerPlayerId)
        {
            var players = await playersRepository.GetAllPlayers(gameId);

            var lastOwner = players.FirstOrDefault(x => x.PlayerId == lastRoundOwnerPlayerId);

            var nextOwner = players
                .Where(x => x.Number > lastOwner.Number)
                .OrderBy(x => x.Number)
                .FirstOrDefault();

            if (nextOwner == null)
                nextOwner = players
                    .OrderBy(x => x.Number)
                    .FirstOrDefault();

            if (nextOwner == null)
                return new GetNextRoundOwnerResult();

            return new GetNextRoundOwnerResult(nextOwner.PlayerId);
        }
    }

    public class GetNextRoundOwnerResult
    {
        public GetNextRoundOwnerResult()
        {
            IsSuccessful = false;
        }
        public GetNextRoundOwnerResult(int playerId)
        {
            IsSuccessful = true;
            PlayerId = playerId;
        }

        public bool IsSuccessful { get; set; }
        public int PlayerId { get; set; }
    }
}
