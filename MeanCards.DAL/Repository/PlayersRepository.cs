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

        public async Task<int> CreatePlayer(CreatePlayerModel model)
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

            return newPlayer.PlayerId;
        }

        public async Task<PlayerModel> GetPlayerById(int playerId)
        {
            var query = from player in context.Players
                        where player.PlayerId == playerId
                        select new PlayerModel
                        {
                            PlayerId = player.PlayerId,
                            GameId = player.GameId,
                            Points = player.Points,
                            UserId = player.UserId,
                            Number = player.Number
                        };

            return await query.FirstOrDefaultAsync();
        }
    }
}
