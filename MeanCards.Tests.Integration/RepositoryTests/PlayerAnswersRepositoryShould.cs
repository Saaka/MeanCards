using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.AnswerCards;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DAL.Creation.QuestionCards;
using MeanCards.Model.DTO.Games;
using MeanCards.Tests.Integration.BaseTests;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class PlayerAnswersRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreatePlayerAnswer()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await Fixture.CreateDefaultGame(languageId, userId);
            var playerId = await Fixture.CreateDefaultPlayer(userId, gameId);
            var questionCardId = await CreateQuestionCard(languageId);
            var gameRound = await CreateGameRound(gameId, playerId, questionCardId);
            var answerCardId = await CreateAnswerCard(languageId);

            var repository = Fixture.GetService<IPlayerAnswersRepository>();
            var createAnswer = new CreatePlayerAnswerModel
            {
                AnswerCardId = answerCardId, 
                GameRoundId = gameRound.GameRoundId,
                PlayerId = playerId
            };

            var playerAnswerId = await repository.CreatePlayerAnswer(createAnswer);

            var answers = await repository.GetAllPlayerAnswers(gameRound.GameRoundId);

            Assert.Single(answers);

            var answer = answers.First();
            Assert.Equal(answerCardId, answer.AnswerCardId);
            Assert.Equal(gameRound.GameRoundId, answer.GameRoundId);
            Assert.False(answer.IsSelectedAnswer);
            Assert.Equal(playerId, answer.PlayerId);
            Assert.Null(answer.SecondaryAnswerCardId);
            TestHelper.AssertNumberOfFields<CreatePlayerAnswerModel>(4);
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

        private async Task<int> CreateAnswerCard(int languageId)
        {
            var repository = Fixture.GetService<IAnswerCardsRepository>();

            return await repository.CreateAnswerCard(new CreateAnswerCardModel
            {
                LanguageId = languageId,
                Text = "Test",
                IsAdultContent = false
            });
        }

        private async Task<GameRoundModel> CreateGameRound(int gameId, int playerId, int questionCardId, int roundNumber = 1)
        {
            var repository = Fixture.GetService<IGameRoundsRepository>();
            var createRound = new CreateGameRoundModel
            {
                GameId = gameId,
                RoundOwnerId = playerId,
                QuestionCardId = questionCardId,
                RoundNumber = roundNumber
            };

            return await repository.CreateGameRound(createRound);
        }
    }
}
