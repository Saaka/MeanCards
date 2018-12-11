using MeanCards.Common.Enums;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using MeanCards.Model.DTO.Games;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using MeanCards.Model.DAL.Creation.Games;

namespace MeanCards.DAL.Repository
{
    public class GamesRepository : IGamesRepository
    {
        private readonly AppDbContext context;

        public GamesRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<GameModel> CreateGame(CreateGameModel model)
        {
            var newGame = new Game
            {
                IsActive = true,
                Code = model.Code,
                LanguageId = model.LanguageId,
                Name = model.Name,
                OwnerId = model.OwnerId,
                ShowAdultContent = model.ShowAdultContent,
                Status = (byte)GameStatusEnum.Created,
                CreateDate = DateTime.UtcNow,
                PointsLimit = model.PointsLimit,
                Checkpoint = model.Checkpoint
            };

            context.Games.Add(newGame);
            await context.SaveChangesAsync();

            return MapToModel(newGame);
        }

        public async Task<GameModel> GetGameById(int gameId)
        {
            var query = from game in context.Games
                        where game.GameId == gameId
                        select game;

            var result = await query.FirstOrDefaultAsync();

            return MapToModel(result);
        }

        public async Task<GameModel> GetGameByCode(string code)
        {
            var query = from game in context.Games
                        where game.Code == code
                        select game;

            var result = await query.FirstOrDefaultAsync();

            return MapToModel(result);
        }

        public async Task<bool> ActiveGameExists(int gameId)
        {
            var exists = await context.Games
                .AnyAsync(x => x.GameId == gameId && x.IsActive);

            return exists;
        }

        public async Task<bool> IsUserInGame(int gameId, int userId)
        {
            var exists = await context.Players
                .AnyAsync(x => x.GameId == gameId && x.UserId == userId && x.IsActive);

            return exists;
        }

        private GameModel MapToModel(Game game)
        {
            if (game == null)
                return null;

            return new GameModel
            {
                Code = game.Code,
                GameId = game.GameId,
                LanguageId = game.LanguageId,
                Name = game.Name,
                OwnerId = game.OwnerId,
                ShowAdultContent = game.ShowAdultContent,
                Status = (GameStatusEnum)game.Status,
                PointsLimit = game.PointsLimit,
                Checkpoint = game.Checkpoint
            };
        }
    }
}
