using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Tests.Integration.BaseTests;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class PlayersRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreatePlayer()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await Fixture.CreateDefaultGame(languageId, userId);

            var playersRepository = Fixture.GetService<IPlayersRepository>();
            var createPlayer = new CreatePlayerModel
            {
                GameId = gameId,
                UserId = userId,
                Number = 1
            };

            var player = await playersRepository.CreatePlayer(createPlayer);

            Assert.NotNull(player);
            Assert.Equal(userId, player.UserId);
            Assert.Equal(gameId, player.GameId);
            Assert.Equal(1, player.Number);
            Assert.Equal(0, player.Points);
            Assert.Equal(3, TestHelper.GetNumberOfProperties<CreatePlayerModel>());
        }
    }
}
