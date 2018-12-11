using MeanCards.DAL.Interfaces.Repository;
using MeanCards.GameManagement.CoreServices;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests.CoreServicesTests
{
    public class GameCheckpointUpdaterShould : BaseCoreTests
    {
        [Fact]
        public async Task UpdateGameCheckpoint()
        {
            var game = await Fixture.CreateGame();

            var updater = Fixture.GetService<IGameCheckpointUpdater>();

            var checkpoint = await updater.Update(game.GameId);

            Assert.NotEqual(game.Checkpoint, checkpoint);

            var gamesRepository = Fixture.GetService<IGamesRepository>();

            var updatedGame = await gamesRepository.GetGameById(game.GameId);

            Assert.Equal(updatedGame.Checkpoint, checkpoint);
        }
    }
}
