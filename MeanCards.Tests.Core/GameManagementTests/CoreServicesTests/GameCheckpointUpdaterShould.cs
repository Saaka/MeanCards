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
            var checkpointRepository = Fixture.GetService<IGameCheckpointRepository>();

            var oldCheckpoint = await checkpointRepository.GetCurrentCheckpoint(game.GameId);

            var checkpoint = await updater.Update(game.GameId, "Test1");
            checkpoint = await updater.Update(game.GameId, "Test2");

            var newCheckpoint = await checkpointRepository.GetCurrentCheckpoint(game.GameId);

            Assert.NotEqual(oldCheckpoint, checkpoint);
            Assert.Equal(checkpoint, newCheckpoint);
        }

        [Fact]
        public async Task GetListOfAllCheckpoints()
        {
            var game = await Fixture.CreateGame(
                additionalPlayersCount: 0);
            var updater = Fixture.GetService<IGameCheckpointUpdater>();
            var checkpointRepository = Fixture.GetService<IGameCheckpointRepository>();
            
            await updater.Update(game.GameId, "Test1");
            await updater.Update(game.GameId, "Test2");
            await updater.Update(game.GameId, "Test3");

            var checkpoints = await checkpointRepository.GetCheckpointsForGame(game.GameId);

            Assert.Equal(4, checkpoints.Count);
        }
    }
}
