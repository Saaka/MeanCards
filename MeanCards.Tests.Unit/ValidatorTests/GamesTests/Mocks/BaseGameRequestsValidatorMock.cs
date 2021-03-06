﻿using MeanCards.Validators;
using MeanCards.Validators.Games;
using MediatR;
using Moq;
using System.Threading.Tasks;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks
{
    public class BaseGameRequestsValidatorMock
    {
        public static Mock<IBaseGameRequestsValidator> CreateMock()
        {
            var mock = new Mock<IBaseGameRequestsValidator>();
            mock.Setup(x => x.Validate(It.IsAny<IBaseRequest>()))
                .Returns(Task.FromResult(new ValidatorResult()))
                .Verifiable();

            return mock;
        }
    }
}
