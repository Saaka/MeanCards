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
            var gameId = await Fixture.CreateDefaultGame(languageId, userId);
            var playerId = await Fixture.CreateDefaultPlayer(gameId, userId);
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
    }
}
