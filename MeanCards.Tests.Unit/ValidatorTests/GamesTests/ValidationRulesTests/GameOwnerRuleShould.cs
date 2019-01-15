using MeanCards.Common.Constants;
using MeanCards.Model.Core;
using MeanCards.Model.Core.Games.Base;
using MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks;
using MeanCards.Validators.Games.ValidationRules;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.ValidationRulesTests
{
    public class GameOwnerRuleShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var gameRepoMock = GamesRepositoryMock.Create();

            var rule = new GameOwnerRule(gameRepoMock.Object);

            var request = new RequestConcrete
            {
                GameId = 1,
                UserId = 1
            };

            var result = await rule.Validate(request);

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task ReturnFailureForUserOtherThanOwner()
        {
            var gameRepoMock = GamesRepositoryMock.Create(
                isGameOwner: false);

            var rule = new GameOwnerRule(gameRepoMock.Object);

            var request = new RequestConcrete
            {
                GameId = 1,
                UserId = 1
            };

            var result = await rule.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidUserAction, result.Error);
        }

        private class RequestConcrete : IGameRequest, IUserRequest
        {
            public int GameId { get; set; }

            public int UserId { get; set; }
        }
    }
}
