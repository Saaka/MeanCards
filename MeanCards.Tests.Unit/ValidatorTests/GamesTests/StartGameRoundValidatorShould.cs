using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
using MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks;
using MeanCards.Validators.Games;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests
{
    public class StartGameRoundValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create(
                status: Common.Enums.GameRoundStatusEnum.Pending).Object;
            var gameRepo = GamesRepositoryMock.Create().Object;

            var validator = new StartGameRoundValidator(baseMock.Object, playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            baseMock.Verify(x => x.Validate(request));
        }

        [Fact]
        public async Task ReturnSuccessForGameOwner()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create(
                isRoundOwner: false,
                status: Common.Enums.GameRoundStatusEnum.Pending).Object;
            var gameRepo = GamesRepositoryMock.Create().Object;

            var validator = new StartGameRoundValidator(baseMock.Object, playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task ReturnSuccessForGameRoundOwner()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create(
                status: Common.Enums.GameRoundStatusEnum.Pending).Object;
            var gameRepo = GamesRepositoryMock.Create(
                isGameOwner: false).Object;

            var validator = new StartGameRoundValidator(baseMock.Object, playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task ReturnFailureForInvalidUserAndPlayer()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create(
                isRoundOwner: false,
                status: Common.Enums.GameRoundStatusEnum.Pending).Object;
            var gameRepo = GamesRepositoryMock.Create( 
                isGameOwner: false).Object;

            var validator = new StartGameRoundValidator(baseMock.Object, playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                UserId = 1,
                GameRoundId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidUserAction, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidRoundStatus()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create(
                status: Common.Enums.GameRoundStatusEnum.Finished).Object;
            var gameRepo = GamesRepositoryMock.Create().Object;

            var validator = new StartGameRoundValidator(baseMock.Object, playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidGameRoundStatus, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForNotEnoughPlayers()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = PlayersRepositoryMock.Create(
                roundHasEnoughPlayers: false).Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create(
                status: Common.Enums.GameRoundStatusEnum.Pending).Object;
            var gameRepo = GamesRepositoryMock.Create().Object;

            var validator = new StartGameRoundValidator(baseMock.Object, playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.NotEnoughPlayers, result.Error);
        }

        private const int RoundOwnerId = 1;
    }
}
