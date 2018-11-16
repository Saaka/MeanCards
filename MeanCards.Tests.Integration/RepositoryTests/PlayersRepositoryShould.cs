using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Creation;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class PlayersRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreatePlayer()
        {
            var languageId = await CreateDefaultLanguage();
            var userId = await CreateDefaultUser();
            var gameId = await CreateDefaultGame(languageId, userId);

            var playersRepository = Fixture.GetService<IPlayersRepository>();
            var createPlayer = new CreatePlayerModel
            {
                GameId = gameId,
                UserId = userId
            };

            var playerId = await playersRepository.CreatePlayer(createPlayer);

            var player = await playersRepository.GetPlayerById(playerId);

            Assert.NotNull(player);
            Assert.Equal(userId, player.UserId);
            Assert.Equal(gameId, player.GameId);
            Assert.Equal(0, player.Points);
            Assert.True(player.IsActive);
        }
    }
}
