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

        [Fact]
        public async Task GetMaxPlayerNumberForGame()
        {
            var playersRepository = Fixture.GetService<IPlayersRepository>();

            var languageId = await Fixture.CreateDefaultLanguage();
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await Fixture.CreateDefaultGame(languageId, userId);
            var playerId = await Fixture.CreateDefaultPlayer(gameId, userId);

            var number = await playersRepository.GetMaxPlayerNumberForGame(gameId);
            Assert.Equal(1, number);

            var secondUserId = await Fixture.CreateDefaultUser(email: "test2@test.com", userName: "Nowak");
            var secondPlayerId = await Fixture.CreateDefaultPlayer(gameId, secondUserId, ++number);
            
            number = await playersRepository.GetMaxPlayerNumberForGame(gameId);

            Assert.Equal(2, number);

            var thirdUserId = await Fixture.CreateDefaultUser(email: "test3@test.com", userName: "Smith");
            var thirdPlayerId = await Fixture.CreateDefaultPlayer(gameId, thirdUserId, ++number);

            number = await playersRepository.GetMaxPlayerNumberForGame(gameId);

            Assert.Equal(3, number);

        }
    }
}
