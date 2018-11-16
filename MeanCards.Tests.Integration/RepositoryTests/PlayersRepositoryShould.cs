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
            var languageId = await Fixture.CreateDefaultLanguage();
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await CreateGame(languageId, userId);

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

        private async Task<int> CreateGame(int languageId, int userId)
        {
            var gamesRepository = Fixture.GetService<IGamesRepository>();
            var createModel = new CreateGameModel
            {
                GameCode = "gamecode1",
                LanguageId = languageId,
                OwnerId = userId,
                Name = "Test game",
                ShowAdultContent = true
            };

            var gameId = await gamesRepository.CreateGame(createModel);
            return gameId;
        }
    }
}
