using MeanCards.Model.Core;
using MeanCards.Model.Core.Games.Base;
using MediatR;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.BaseTests
{
    public class BaseGameRequestsValidatorShould : IClassFixture<BaseGameRequestsValidatorSut>
    {
        private readonly BaseGameRequestsValidatorSut validatorSut;
        public BaseGameRequestsValidatorShould(BaseGameRequestsValidatorSut validatorSut)
        {
            this.validatorSut = validatorSut;
        }

        [Fact]
        public async Task ShouldCallGameRequestValidator()
        {
            var request = new BaseRequestConcrete();

            await validatorSut.Validator.Validate(request);

            validatorSut.GameRequestValidatorMock.Verify(x => x.Validate(request));
        }

        [Fact]
        public async Task ShouldCallGameRoundRequestValidator()
        {
            var request = new BaseRequestConcrete();

            await validatorSut.Validator.Validate(request);

            validatorSut.GameRoundRequestValidatorMock.Verify(x => x.Validate(request));
        }

        [Fact]
        public async Task ShouldCallUserRequestValidator()
        {
            var request = new BaseRequestConcrete();

            await validatorSut.Validator.Validate(request);

            validatorSut.UserRequestValidatorMock.Verify(x => x.Validate(request));
        }

        [Fact]
        public async Task ShouldCallPlayerRequestValidator()
        {
            var request = new BaseRequestConcrete();

            await validatorSut.Validator.Validate(request);

            validatorSut.PlayerRequestValidatorMock.Verify(x => x.Validate(request));
        }

        private class BaseRequestConcrete : IBaseRequest, IGameRequest, IGameRoundRequest, IUserRequest, IPlayerRequest
        {
            public int UserId { get; set; }

            public int GameId { get; set; }

            public int GameRoundId { get; set; }
        }
    }
}
