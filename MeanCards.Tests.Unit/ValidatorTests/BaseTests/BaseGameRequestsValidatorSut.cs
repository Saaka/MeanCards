using MeanCards.Model.Core;
using MeanCards.Model.Core.Games.Base;
using MeanCards.Validators;
using MeanCards.Validators.Games;
using Moq;
using System.Threading.Tasks;

namespace MeanCards.Tests.Unit.ValidatorTests.BaseTests
{
    public class BaseGameRequestsValidatorSut
    {
        public Mock<IRequestValidator<IGameRequest>> GameRequestValidatorMock { get; private set; }
        public Mock<IRequestValidator<IGameRoundRequest>> GameRoundRequestValidatorMock { get; private set; }
        public Mock<IRequestValidator<IUserRequest>> UserRequestValidatorMock { get; private set; }
        public Mock<IRequestValidator<IPlayerRequest>> PlayerRequestValidatorMock { get; private set; }

        public BaseGameRequestsValidator Validator { get; private set; }

        public BaseGameRequestsValidatorSut()
        {
            GameRequestValidatorMock = CreateGameRequestValidator();
            GameRoundRequestValidatorMock = CreateGameRoundRequestValidator();
            UserRequestValidatorMock = CreateUserRequestValidator();
            PlayerRequestValidatorMock = CreatePlayerRequestValidator();

            Validator = new BaseGameRequestsValidator(
                GameRequestValidatorMock.Object,
                GameRoundRequestValidatorMock.Object,
                UserRequestValidatorMock.Object,
                PlayerRequestValidatorMock.Object);
        }

        private Mock<IRequestValidator<IPlayerRequest>> CreatePlayerRequestValidator()
        {
            var mock = new Mock<IRequestValidator<IPlayerRequest>>();
            mock.Setup(x => x.Validate(It.IsAny<IPlayerRequest>()))
                .Returns(() =>
                {
                    return Task.FromResult(new ValidatorResult());
                })
                .Verifiable();

            return mock;
        }

        private Mock<IRequestValidator<IUserRequest>> CreateUserRequestValidator()
        {
            var mock = new Mock<IRequestValidator<IUserRequest>>();
            mock.Setup(x => x.Validate(It.IsAny<IUserRequest>()))
                .Returns(() =>
                {
                    return Task.FromResult(new ValidatorResult());
                })
                .Verifiable();

            return mock;
        }

        private Mock<IRequestValidator<IGameRoundRequest>> CreateGameRoundRequestValidator()
        {
            var mock = new Mock<IRequestValidator<IGameRoundRequest>>();
            mock.Setup(x => x.Validate(It.IsAny<IGameRoundRequest>()))
                .Returns(() =>
                {
                    return Task.FromResult(new ValidatorResult());
                })
                .Verifiable();

            return mock;
        }

        private Mock<IRequestValidator<IGameRequest>> CreateGameRequestValidator()
        {
            var mock = new Mock<IRequestValidator<IGameRequest>>();
            mock.Setup(x => x.Validate(It.IsAny<IGameRequest>()))
                .Returns(() =>
                {
                    return Task.FromResult(new ValidatorResult());
                })
                .Verifiable();

            return mock;
        }
    }
}
