using MeanCards.Common.Enum;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.DAL.Repository
{
    public class GamesRepository : IGamesRepository
    {
        private readonly AppDbContext context;

        public GamesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateGame(CreateGameModel model)
        {
            var newGame = new Game
            {
                IsActive = true,
                GameCode = model.GameCode,
                LanguageId = model.LanguageId,
                Name = model.Name,
                OwnerId = model.OwnerId,
                ShowAdultContent = model.ShowAdultContent,
                GameStatus = (byte)GameStatusEnum.Created,
                CreateDate = DateTime.UtcNow,
            };

            context.Games.Add(newGame);
            await context.SaveChangesAsync();

            return newGame.GameId;
        }

        public async Task<Game> GetGameById(int gameId)
        {
            var query = from game in context.Games
                        where game.GameId == gameId
                        select game;

            return await query.FirstOrDefaultAsync();
        }
    }
}
