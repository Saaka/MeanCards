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
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
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
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundOwner: false);
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
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
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
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundOwner: false);
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
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundPending: false);
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
            var playersRepo = CreatePlayersRepoMock(
                hasEnoughPlayers: false);
            var gameRoundRepo = CreateGameRoundRepoMock();
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

        private IGameRoundsRepository CreateGameRoundRepoMock(
            bool isRoundOwner = true,
            bool isRoundPending = true,
            bool isRoundInGame = true)
        {
            var mock = new Mock<IGameRoundsRepository>();
            mock.Setup(m => m.GetGameRound(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                if (!isRoundInGame)
                    return Task.FromResult<GameRoundModel>(null);

                return Task.FromResult(new GameRoundModel
                {
                    Status = isRoundPending ? Common.Enums.GameRoundStatusEnum.Pending : Common.Enums.GameRoundStatusEnum.Finished,
                    OwnerPlayerId = isRoundOwner ? RoundOwnerId : int.MaxValue
                });
            });

            return mock.Object;
        }

        private IPlayersRepository CreatePlayersRepoMock(
            bool isUserLinkedWithPlayer = true,
            bool hasEnoughPlayers = true)
        {
            var mock = new Mock<IPlayersRepository>();
            mock.Setup(m => m.GetPlayerByUserId(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                if (!isUserLinkedWithPlayer)
                    return Task.FromResult<PlayerModel>(null);

                return Task.FromResult(new PlayerModel
                {
                    PlayerId = RoundOwnerId
                });
            });

            mock.Setup(m => m.GetActivePlayersCount(It.IsAny<int>())).Returns(() =>
            {
                if (!hasEnoughPlayers)
                    return Task.FromResult<int>(1);

                return Task.FromResult<int>(GameConstants.MinimumPlayersCount);
            });

            return mock.Object;
        }
    }
}
