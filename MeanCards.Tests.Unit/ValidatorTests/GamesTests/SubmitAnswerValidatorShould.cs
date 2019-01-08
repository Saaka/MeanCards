﻿using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
using MeanCards.Model.DTO.QuestionCards;
using MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks;
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
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create().Object;
            var cardsRepo = CreatePlayerCardsRepoMock();
            var questionCardRepo = CreateQuestionCardRepoMock();

            var validator = new SubmitAnswerValidator(baseMock.Object, playersRepo, gameRoundRepo, cardsRepo, questionCardRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = MockConstants.RoundId,
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
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create().Object;
            var cardsRepo = CreatePlayerCardsRepoMock();
            var questionCardRepo = CreateQuestionCardRepoMock();

            var validator = new SubmitAnswerValidator(baseMock.Object, playersRepo, gameRoundRepo, cardsRepo, questionCardRepo);

            var request = new SubmitAnswer
            {
                GameId = 1,
                GameRoundId = MockConstants.RoundId,
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
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create(
                isRoundInProgressStatus: false).Object;
            var cardsRepo = CreatePlayerCardsRepoMock();
            var questionCardRepo = CreateQuestionCardRepoMock();

            var validator = new SubmitAnswerValidator(baseMock.Object, playersRepo, gameRoundRepo, cardsRepo, questionCardRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = MockConstants.RoundId,
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
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create().Object;
            var cardsRepo = CreatePlayerCardsRepoMock(
                isCardLinkedWithUser: false);
            var questionCardRepo = CreateQuestionCardRepoMock();

            var validator = new SubmitAnswerValidator(baseMock.Object, playersRepo, gameRoundRepo, cardsRepo, questionCardRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = MockConstants.RoundId,
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
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create().Object;
            var cardsRepo = CreatePlayerCardsRepoMock();
            var questionCardRepo = CreateQuestionCardRepoMock(
                isMultiChoiceQuestion: true);

            var validator = new SubmitAnswerValidator(baseMock.Object, playersRepo, gameRoundRepo, cardsRepo, questionCardRepo);

            var request = new SubmitAnswer
            {
                GameRoundId = MockConstants.RoundId,
                UserId = 1,
                GameId = 1,
                PlayerCardId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.SecondPlayerCardIdRequired, result.Error);
        }
        
        private IPlayerCardsRepository CreatePlayerCardsRepoMock(bool isCardLinkedWithUser = true)
        {
            var mock = new Mock<IPlayerCardsRepository>();
            mock.Setup(m => m.GetPlayerCard(It.IsAny<int>())).Returns(() =>
           {
               return Task.FromResult(new PlayerCardModel
               {
                   PlayerId = isCardLinkedWithUser ? MockConstants.CardOwnerId : int.MaxValue
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
