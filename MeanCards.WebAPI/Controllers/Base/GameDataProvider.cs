using MeanCards.Model.DTO.Games;
using MeanCards.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Controllers.Base
{
    public interface IGameDataProvider
    {
        Task<GameSimpleModel> GetGame(string gameCode);
        Task<GameRoundSimpleModel> GetGameRound(string gameRoundCode);
    }

    public class GameDataProvider : IGameDataProvider
    {
        private readonly IGameDataService gameDataService;
        private readonly IGameRoundDataService gameRoundDataService;

        public GameDataProvider(
            IGameDataService gameDataService,
            IGameRoundDataService gameRoundDataService)
        {
            this.gameDataService = gameDataService;
            this.gameRoundDataService = gameRoundDataService;
        }

        public async Task<GameSimpleModel> GetGame(string gameCode)
        {
            return await gameDataService.GetGameData(gameCode);
        }

        public async Task<GameRoundSimpleModel> GetGameRound(string gameRoundCode)
        {
            return await gameRoundDataService.GetGameRoundData(gameRoundCode);
        }
    }
}
