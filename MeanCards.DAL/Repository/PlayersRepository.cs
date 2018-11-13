using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Threading.Tasks;

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
                IsActive = true,
                Points = 0
            };

            context.Players.Add(newPlayer);
            await context.SaveChangesAsync();

            return newPlayer.PlayerId;
        }
    }
}
