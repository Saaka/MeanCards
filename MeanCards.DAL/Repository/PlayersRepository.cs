using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using MeanCards.Model.DTO.Players;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using MeanCards.Model.DAL.Creation.Players;

namespace MeanCards.DAL.Repository
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly AppDbContext context;

        public PlayersRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<PlayerModel> CreatePlayer(CreatePlayerModel model)
        {
            var newPlayer = new Player
            {
                GameId = model.GameId,
                UserId = model.UserId,
                Number = model.Number,
                IsActive = true,
                Points = 0
            };

            context.Players.Add(newPlayer);
            await context.SaveChangesAsync();

            return MapToModel(newPlayer);
        }

        public async Task<PlayerModel> GetPlayerById(int playerId)
        {
            var query = from p in context.Players
                        where p.PlayerId == playerId
                        select p;

            var player = await query.FirstOrDefaultAsync();
            if (player == null) return null;

            return MapToModel(player);
        }

        public async Task<PlayerModel> GetPlayerByUserId(int userId, int gameId)
        {
            var query = from p in context.Players
                        where p.UserId == userId
                            && p.GameId == gameId
                            && p.IsActive
                        select p;

            var player = await query.FirstOrDefaultAsync();
            if (player == null) return null;

            return MapToModel(player);
        }

        public async Task<int> GetMaxPlayerNumberForGame(int gameId)
        {
            var query = from player in context.Players
                        where player.GameId == gameId
                        orderby player.Number descending
                        select player.Number;

            return await query.FirstOrDefaultAsync();
        }

        private PlayerModel MapToModel(Player player)
        {
            return new PlayerModel
            {
                PlayerId = player.PlayerId,
                GameId = player.GameId,
                Number = player.Number,
                Points = player.Points,
                UserId = player.UserId,
                IsActive = player.IsActive
            };
        }

        public async Task<int> GetActivePlayersCount(int gameId)
        {
            var query = from player in context.Players
                        where player.GameId == gameId
                                && player.IsActive
                        select player.PlayerId;

            return await query.CountAsync();
        }
    }
}
