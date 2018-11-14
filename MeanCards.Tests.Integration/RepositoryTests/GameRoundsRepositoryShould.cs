using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Creation;
using MeanCards.Tests.Integration.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class GameRoundsRepositoryShould : IDisposable
    {
        private readonly DALServiceCollectionFixture Fixture;

        public GameRoundsRepositoryShould()
        {
            Fixture = new DALServiceCollectionFixture();
        }

        [Fact]
        public async Task CreateGameRound()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await CreateGame(languageId, userId);
            var playerId = await CreatePlayer(userId, gameId);
            var questionCardId = await CreateQuestionCard(languageId);

            var repository = Fixture.GetService<IGameRoundsRepository>();
            var createRound = new CreateGameRoundModel
            {
                GameId = gameId,
                RoundOwnerId = playerId,
                QuestionCardId = questionCardId
            };

            var gameRoundId = await repository.CreateGameRound(createRound);
            var gameRound = await repository.GetCurrentGameRound(gameId);

            Assert.NotNull(gameRound);
            Assert.Equal(playerId, gameRound.RoundOwnerId);
            Assert.Equal(gameId, gameRound.GameId);
            Assert.Equal(questionCardId, gameRound.QuestionCardId);
            Assert.True(gameRound.IsActive);
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

        private async Task<int> CreatePlayer(int userId, int gameId)
        {
            var playersRepository = Fixture.GetService<IPlayersRepository>();

            return await playersRepository.CreatePlayer(new CreatePlayerModel
            {
                UserId = userId,
                GameId = gameId
            });
        }

        private async Task<int> CreateQuestionCard(int languageId)
        {
            var repository = Fixture.GetService<IQuestionCardsRepository>();

            return await repository.CreateQuestionCard(new CreateQuestionCardModel
            {
                LanguageId = languageId,
                NumberOfAnswers = 1,
                Text = "Test",
                IsAdultContent = false
            });
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}
