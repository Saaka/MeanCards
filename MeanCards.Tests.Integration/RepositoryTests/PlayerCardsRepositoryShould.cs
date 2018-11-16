using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Creation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class PlayerCardsRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreatePlayerCards()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await CreateGame(languageId, userId);
            var playerId = await CreatePlayer(gameId, userId);
            var answerCardId = await PopulateAnswerCards(languageId);

            var cardModels = new List<CreatePlayerCardModel>
            {
                new CreatePlayerCardModel { PlayerId = playerId, AnswerCardId = answerCardId},
                new CreatePlayerCardModel { PlayerId = playerId, AnswerCardId = answerCardId},
                new CreatePlayerCardModel { PlayerId = playerId, AnswerCardId = answerCardId},
                new CreatePlayerCardModel { PlayerId = playerId, AnswerCardId = answerCardId},
                new CreatePlayerCardModel { PlayerId = playerId, AnswerCardId = answerCardId},
            };

            var cardRepository = Fixture.GetService<IPlayerCardsRepository>();

            await cardRepository.CreatePlayerCards(cardModels);

            var cards = await cardRepository.GetUnusedPlayerCards(playerId);

            Assert.Equal(5, cards.Count);
        }

        private async Task<int> PopulateAnswerCards(int languageId)
        {
            var cardRepository = Fixture.GetService<IAnswerCardsRepository>();
            var answerCardId = await cardRepository.CreateAnswerCard(new CreateAnswerCardModel
            {
                IsAdultContent = true,
                LanguageId = languageId,
                Text = "Test1"
            });

            return answerCardId;
        }

        private async Task<int> CreatePlayer(int gameId, int userId)
        {
            var playersRepository = Fixture.GetService<IPlayersRepository>();
            var createPlayer = new CreatePlayerModel
            {
                GameId = gameId,
                UserId = userId
            };

            var playerId = await playersRepository.CreatePlayer(createPlayer);
            return playerId;
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
