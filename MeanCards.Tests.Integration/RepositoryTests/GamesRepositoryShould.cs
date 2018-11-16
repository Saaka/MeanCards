using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Creation;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class GamesRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreateGame()
        {
            var languageId = await CreateDefaultLanguage();
            var userId = await CreateDefaultUser();

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

        [Fact]
        public async Task ThrowForDuplicatedCode()
        {
            var languageId = await CreateDefaultLanguage();
            var userId = await CreateDefaultUser();

            var gamesRepository = Fixture.GetService<IGamesRepository>();
            var createModel = new CreateGameModel
            {
                GameCode = "gamecode1",
                LanguageId = languageId,
                OwnerId = userId,
                Name = "Test game",
                ShowAdultContent = true
            };


            Func<Task> createTwoGames = () => Task.Run(async () =>
            {
                var id = await gamesRepository.CreateGame(createModel);
                id = await gamesRepository.CreateGame(createModel);
            });

            var ex = await Assert.ThrowsAnyAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(createTwoGames);
            Assert.NotNull(ex);
        }
    }
}
