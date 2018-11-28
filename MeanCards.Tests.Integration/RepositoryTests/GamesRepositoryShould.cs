using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Tests.Integration.BaseTests;
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
            var languageId = await Fixture.CreateDefaultLanguage();
            var userId = await Fixture.CreateDefaultUser();

            var gamesRepository = Fixture.GetService<IGamesRepository>();
            var createModel = new CreateGameModel
            {
                Code = "gamecode1",
                LanguageId = languageId,
                OwnerId = userId,
                Name = "Test game",
                ShowAdultContent = true
            };

            var gameId = await gamesRepository.CreateGame(createModel);

            var game = await gamesRepository.GetGameById(gameId);

            Assert.NotNull(game);
            Assert.True(game.ShowAdultContent);
            Assert.Equal(languageId, game.LanguageId);
            Assert.Equal(userId, game.OwnerId);
            Assert.Equal("Test game", game.Name);
            Assert.Equal(5, TestHelper.GetNumberOfProperties<CreateGameModel>());
        }

        [Fact]
        public async Task ThrowForDuplicatedCode()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var userId = await Fixture.CreateDefaultUser();

            var gamesRepository = Fixture.GetService<IGamesRepository>();
            var createModel = new CreateGameModel
            {
                Code = "gamecode1",
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
