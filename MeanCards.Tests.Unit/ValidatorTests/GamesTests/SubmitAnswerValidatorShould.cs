using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
using MeanCards.Model.DTO.QuestionCards;
using MeanCards.Validators.Games;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests
{
    public class SubmitAnswerValidatorShould
    {

        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock();
            var questionCardRepo = CreateQuestionCardRepoMock();

            var validator = new SubmitAnswerValidator(baseMock.Object, playersRepo, gameRoundRepo, cardsRepo, questionCardRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = RoundId,
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            baseMock.Verify(x => x.Validate(request));
        }

        [Fact]
        public async Task ReturnFailureForMissingPlayerCardId()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock();
            var questionCardRepo = CreateQuestionCardRepoMock();

            var validator = new SubmitAnswerValidator(baseMock.Object, playersRepo, gameRoundRepo, cardsRepo, questionCardRepo);

            var request = new SubmitAnswer
            {
                GameId = 1,
                GameRoundId = RoundId,
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.PlayerCardIdRequired, result.Error);
        }
        
        [Fact]
        public async Task ReturnFailureForInvalidRoundStatus()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundInProgressStatus: false);
            var cardsRepo = CreatePlayerCardsRepoMock();
            var questionCardRepo = CreateQuestionCardRepoMock();

            var validator = new SubmitAnswerValidator(baseMock.Object, playersRepo, gameRoundRepo, cardsRepo, questionCardRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = RoundId,
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidGameRoundStatus, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidCardAndPlayerCombination()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock(
                isCardLinkedWithUser: false);
            var questionCardRepo = CreateQuestionCardRepoMock();

            var validator = new SubmitAnswerValidator(baseMock.Object, playersRepo, gameRoundRepo, cardsRepo, questionCardRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = RoundId,
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.CardNotLinkedWithPlayer, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingSecondPlayerCard()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var cardsRepo = CreatePlayerCardsRepoMock();
            var questionCardRepo = CreateQuestionCardRepoMock(
                isMultiChoiceQuestion: true);

            var validator = new SubmitAnswerValidator(baseMock.Object, playersRepo, gameRoundRepo, cardsRepo, questionCardRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = RoundId,
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.SecondPlayerCardIdRequired, result.Error);
        }

        private const int CardOwnerId = 1;
        private const int RoundOwnerId = 1;
        private const int RoundId = 1;

        private IGameRoundsRepository CreateGameRoundRepoMock(
            bool isRoundOwner = true,
            bool isRoundInProgressStatus = true,
            bool isRoundInGame = true)
        {
            var mock = new Mock<IGameRoundsRepository>();
            mock.Setup(m => m.GetCurrentGameRound(It.IsAny<int>())).Returns(() =>
            {
                if (!isRoundInGame)
                    return Task.FromResult<GameRoundModel>(null);

                return Task.FromResult(new GameRoundModel
                {
                    Status = isRoundInProgressStatus ? Common.Enums.GameRoundStatusEnum.InProgress : Common.Enums.GameRoundStatusEnum.Finished,
                    OwnerPlayerId = isRoundOwner ? RoundOwnerId : int.MaxValue,
                    GameRoundId = RoundId
                });
            });

            return mock.Object;
        }

        private IPlayersRepository CreatePlayersRepoMock(bool isUserLinkedWithPlayer = true)
        {
            var mock = new Mock<IPlayersRepository>();
            mock.Setup(m => m.GetPlayerByUserId(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                if (!isUserLinkedWithPlayer)
                    return Task.FromResult<PlayerModel>(null);

                return Task.FromResult(new PlayerModel
                {
                    PlayerId = CardOwnerId
                });
            });

            return mock.Object;
        }

        private IPlayerCardsRepository CreatePlayerCardsRepoMock(bool isCardLinkedWithUser = true)
        {
            var mock = new Mock<IPlayerCardsRepository>();
            mock.Setup(m => m.GetPlayerCard(It.IsAny<int>())).Returns(() =>
           {
               return Task.FromResult(new PlayerCardModel
               {
                   PlayerId = isCardLinkedWithUser ? CardOwnerId : int.MaxValue
               });
           });

            return mock.Object;
        }

        private IQuestionCardsRepository CreateQuestionCardRepoMock(bool isMultiChoiceQuestion = false)
        {
            var mock = new Mock<IQuestionCardsRepository>();
            mock.Setup(m => m.GetActiveQuestionCardForRound(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult(new QuestionCardModel
                {
                    NumberOfAnswers = isMultiChoiceQuestion ? (byte)2 : (byte)1
                });
            });

            return mock.Object;
        }
    }
}
