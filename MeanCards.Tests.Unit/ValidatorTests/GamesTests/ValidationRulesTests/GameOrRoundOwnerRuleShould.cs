using MeanCards.Common.Constants;
using MeanCards.Model.Core.Games.Base;
using MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks;
using MeanCards.Validators.Games.ValidationRules;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.ValidationRulesTests
{
    public class GameOrRoundOwnerRuleShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var gameRepoMock = GamesRepositoryMock.Create();
            var gameRoundRepoMock = GameRoundsRepositoryMock.Create();
            var playersRepoMock = PlayersRepositoryMock.Create();

            var rule = new GameOrRoundOwnerRule(gameRepoMock.Object, gameRoundRepoMock.Object, playersRepoMock.Object);

            var request = new RequestConcrete
            {
                GameId = 1,
                GameRoundId = 1,
                UserId = 1
            };

            var result = await rule.Validate(request);

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task ReturnSuccessForRoundOwner()
        {
            var gameRepoMock = GamesRepositoryMock.Create(
                isGameOwner: false);
            var gameRoundRepoMock = GameRoundsRepositoryMock.Create();
            var playersRepoMock = PlayersRepositoryMock.Create();

            var rule = new GameOrRoundOwnerRule(gameRepoMock.Object, gameRoundRepoMock.Object, playersRepoMock.Object);

            var request = new RequestConcrete
            {
                GameId = 1,
                GameRoundId = 1,
                UserId = 1
            };

            var result = await rule.Validate(request);

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task ReturnSuccessForGameOwner()
        {
            var gameRepoMock = GamesRepositoryMock.Create();
            var gameRoundRepoMock = GameRoundsRepositoryMock.Create(
                isRoundOwner: false);
            var playersRepoMock = PlayersRepositoryMock.Create();

            var rule = new GameOrRoundOwnerRule(gameRepoMock.Object, gameRoundRepoMock.Object, playersRepoMock.Object);

            var request = new RequestConcrete
            {
                GameId = 1,
                GameRoundId = 1,
                UserId = 1
            };

            var result = await rule.Validate(request);

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidUser()
        {
            var gameRepoMock = GamesRepositoryMock.Create(
                isGameOwner: false);
            var gameRoundRepoMock = GameRoundsRepositoryMock.Create(
                isRoundOwner: false);
            var playersRepoMock = PlayersRepositoryMock.Create();

            var rule = new GameOrRoundOwnerRule(gameRepoMock.Object, gameRoundRepoMock.Object, playersRepoMock.Object);

            var request = new RequestConcrete
            {
                GameId = 1,
                GameRoundId = 1,
                UserId = 1
            };

            var result = await rule.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidUserAction, result.Error);
        }

        class RequestConcrete : IGameRoundRequest, IPlayerRequest
        {
            public int UserId { get; set; }

            public int GameId { get; set; }

            public int GameRoundId { get; set; }
        }
    }
}
