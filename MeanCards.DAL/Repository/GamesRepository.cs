using MeanCards.Common.Enum;
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

        public async Task<int> CreateGame(CreateGameModel model)
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
            };

            context.Games.Add(newGame);
            await context.SaveChangesAsync();

            return newGame.GameId;
        }

        public async Task<GameModel> GetGameById(int gameId)
        {
            var query = from game in context.Games
                        where game.GameId == gameId
                        select new GameModel
                        {
                            GameId = game.GameId,
                            Code = game.Code,
                            Status = game.Status,
                            LanguageId = game.LanguageId,
                            Name = game.Name,
                            OwnerId = game.OwnerId,
                            ShowAdultContent = game.ShowAdultContent
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<GameModel> GetGameByCode(string code)
        {
            var query = from game in context.Games
                        where game.Code == code
                        select new GameModel
                        {
                            GameId = game.GameId,
                            Code = game.Code,
                            Status = game.Status,
                            LanguageId = game.LanguageId,
                            Name = game.Name,
                            OwnerId = game.OwnerId,
                            ShowAdultContent = game.ShowAdultContent
                        };

            return await query.FirstOrDefaultAsync();
        }
    }
}
