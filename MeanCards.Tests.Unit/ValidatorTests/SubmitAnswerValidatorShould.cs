using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Validators.Games;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests
{
    public class SubmitAnswerValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock();

            var validator = new SubmitAnswerValidator(playersRepo, gameRoundRepo, cardsRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task ReturnFailureForMissingGameRoundId()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock();

            var validator = new SubmitAnswerValidator(playersRepo, gameRoundRepo, cardsRepo);

            var request = new SubmitAnswer
            {
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameRoundIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingGameId()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock();

            var validator = new SubmitAnswerValidator(playersRepo, gameRoundRepo, cardsRepo);

            var request = new SubmitAnswer
            {
                UserId = 1,
                GameRoundId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingUserId()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock();

            var validator = new SubmitAnswerValidator(playersRepo, gameRoundRepo, cardsRepo);

            var request = new SubmitAnswer
            {
                GameId = 1,
                GameRoundId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.UserIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingPlayerCardId()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock();

            var validator = new SubmitAnswerValidator(playersRepo, gameRoundRepo, cardsRepo);

            var request = new SubmitAnswer
            {
                GameId = 1,
                GameRoundId = 1,
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.PlayerCardIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidUserAndPlayerCombination()
        {
            var playersRepo = CreatePlayersRepoMock(
                isUserLinkedWithPlayer: false);
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock();

            var validator = new SubmitAnswerValidator(playersRepo, gameRoundRepo, cardsRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Players.UserNotLinkedWithPlayer, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidRoundStatus()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundInProgress: false);
            var cardsRepo = CreatePlayerCardsRepoMock();

            var validator = new SubmitAnswerValidator(playersRepo, gameRoundRepo, cardsRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidGameRoundStatus, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidGameAndRoundCombination()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundInGame: false);
            var cardsRepo = CreatePlayerCardsRepoMock();

            var validator = new SubmitAnswerValidator(playersRepo, gameRoundRepo, cardsRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.RoundNotLinkedWithGame, result.Error);
        }


        [Fact]
        public async Task ReturnFailureForInvalidCardAndPlayerCombination()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock(
                isCardLinkedWithUser: false);

            var validator = new SubmitAnswerValidator(playersRepo, gameRoundRepo, cardsRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.CardNotLinkedWithPlayer, result.Error);
        }

        private IGameRoundsRepository CreateGameRoundRepoMock(
            bool isRoundOwner = true,
            bool isRoundInProgress = true,
            bool isRoundInGame = true)
        {
            var mock = new Mock<IGameRoundsRepository>();
            mock.Setup(x => x.IsGameRoundOwner(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(isRoundOwner));
            mock.Setup(x => x.IsGameRoundInProgress(It.IsAny<int>())).Returns(Task.FromResult(isRoundInProgress));
            mock.Setup(x => x.IsRoundInGame(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(isRoundInGame));

            return mock.Object;
        }

        private IPlayersRepository CreatePlayersRepoMock(bool isUserLinkedWithPlayer = true)
        {
            var mock = new Mock<IPlayersRepository>();
            mock.Setup(x => x.IsUserLinkedWithPlayer(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(isUserLinkedWithPlayer));

            return mock.Object;
        }

        private IPlayerCardsRepository CreatePlayerCardsRepoMock(bool isCardLinkedWithUser = true)
        {
            var mock = new Mock<IPlayerCardsRepository>();
            mock.Setup(x => x.IsCardLinkedWithUser(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(isCardLinkedWithUser));

            return mock.Object;
        }
    }
}
