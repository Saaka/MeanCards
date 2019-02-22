using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.Games;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Services
{
    public interface IGameRoundDataService
    {
        Task<GameRoundSimpleModel> GetGameRoundData(string gameRoundCode);
    }
    public class GameRoundDataService : IGameRoundDataService
    {
        private readonly IMemoryCache memoryCache;
        private readonly IGameRoundsRepository gameRoundsRepository;

        private const string GameRoundDataContextPrefix = "GR_CTX_";

        public GameRoundDataService(
             IMemoryCache memoryCache,
             IGameRoundsRepository gamesRoundsRepository)
        {
            this.memoryCache = memoryCache;
            this.gameRoundsRepository = gamesRoundsRepository;
        }

        public async Task<GameRoundSimpleModel> GetGameRoundData(string gameRoundCode)
        {
            string key = $"{GameRoundDataContextPrefix}{gameRoundCode}";

            var gameRoundData = await memoryCache.GetOrCreateAsync(key, async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(24);

                var result = await gameRoundsRepository.GetGameRoundByCode(gameRoundCode);
                if (result == null)
                    throw new ArgumentException(ValidatorErrors.Games.GameRoundNotFoundOrInactive);

                return result;
            });

            return gameRoundData;
        }
    }
}
