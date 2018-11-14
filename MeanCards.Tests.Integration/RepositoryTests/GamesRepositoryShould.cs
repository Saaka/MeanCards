using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Creation;
using MeanCards.Tests.Integration.Config;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class GamesRepositoryShould : IDisposable
    {
        private readonly DALServiceCollectionFixture Fixture;

        public GamesRepositoryShould()
        {
            Fixture = new DALServiceCollectionFixture();
        }

        [Fact]
        public async Task CreateGame()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var userId = await Fixture.CreateDefaultUser();

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

            var game = await gamesRepository.GetGameById(gameId);

            Assert.NotNull(game);
            Assert.True(game.IsActive);
            Assert.True(game.ShowAdultContent);
            Assert.Equal(languageId, game.LanguageId);
            Assert.Equal(userId, game.OwnerId);
            Assert.Equal("Test game", game.Name);
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}
