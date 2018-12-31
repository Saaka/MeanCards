using MeanCards.Model.Core;
using MeanCards.Model.Core.Games.Base;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.BaseTests
{
    public class BaseGameRequestsValidatorShould
    {
        [Fact]
        public void Test()
        {

        }

        private class BaseRequestConcrete : IBaseRequest, IGameRequest, IGameRoundRequest, IUserRequest, IPlayerRequest
        {
            public int UserId { get; set; }

            public int GameId { get; set; }

            public int GameRoundId { get; set; }
        }
    }
}
