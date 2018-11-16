using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Creation;
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
            var gameId = await CreateGame(languageId, userId);
            var playerId = await CreatePlayer(userId, gameId);
            var questionCardId = await CreateQuestionCard(languageId);
            var gameRoundId = await CreateGameRound(gameId, playerId, questionCardId);
            var answerCardId = await CreateAnswerCard(languageId);

            var repository = Fixture.GetService<IPlayerAnswersRepository>();
            var createAnswer = new CreatePlayerAnswerModel
            {
                AnswerCardId = answerCardId, 
                GameRoundId = gameRoundId,
                PlayerId = playerId
            };

            var playerAnswerId = await repository.CreatePlayerAnswer(createAnswer);

            var answers = await repository.GetAllPlayerAnswers(gameRoundId);

            Assert.Single(answers);

            var answer = answers.First();
            Assert.Equal(answerCardId, answer.AnswerCardId);
            Assert.Equal(gameRoundId, answer.GameRoundId);
            Assert.False(answer.IsSelectedAnswer);
            Assert.Equal(playerId, answer.PlayerId);
            Assert.Null(answer.SecondaryAnswerCardId);
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

        private async Task<int> CreateGameRound(int gameId, int playerId, int questionCardId)
        {
            var repository = Fixture.GetService<IGameRoundsRepository>();
            var createRound = new CreateGameRoundModel
            {
                GameId = gameId,
                RoundOwnerId = playerId,
                QuestionCardId = questionCardId
            };

            return await repository.CreateGameRound(createRound);
        }
    }
}
