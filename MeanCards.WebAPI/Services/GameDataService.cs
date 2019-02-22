using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.Games;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Services
{
    public interface IGameDataService
    {
        Task<GameSimpleModel> GetGameData(string gameCode);
    }
    public class GameDataService : IGameDataService
    {
        private readonly IMemoryCache memoryCache;
        private readonly IGamesRepository gamesRepository;

        private const string GameDataContextPrefix = "G_CTX_";

        public GameDataService(
             IMemoryCache memoryCache,
             IGamesRepository gamesRepository)
        {
            this.memoryCache = memoryCache;
            this.gamesRepository = gamesRepository;
        }

        public async Task<GameSimpleModel> GetGameData(string gameCode)
        {
            string key = $"{GameDataContextPrefix}{gameCode}";

            var gameData = await memoryCache.GetOrCreateAsync(key, async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(24);

                var result = await gamesRepository.GetGameByCode(gameCode);
                if (result == null)
                    throw new ArgumentException(ValidatorErrors.Games.GameNotFoundOrInactive);

                return result;
            });

            return gameData;
        }
    }
}
