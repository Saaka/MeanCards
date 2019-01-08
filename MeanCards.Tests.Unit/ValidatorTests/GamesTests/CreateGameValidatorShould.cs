using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks;
using MeanCards.Validators.Games;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests
{
    public class CreateGameValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var validator = new CreateGameValidator(baseMock.Object);

            var request = new CreateGame
            {
                LanguageId = 1,
                Name = "Test",
                UserId = 1,
                ShowAdultContent = true
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
            baseMock.Verify(x => x.Validate(request));
        }

        [Fact]
        public async Task ReturnFailureForMissingLanguage()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var validator = new CreateGameValidator(baseMock.Object);

            var request = new CreateGame
            {
                Name = "Test",
                UserId = 1,
                ShowAdultContent = true
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameLanguageRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingName()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var validator = new CreateGameValidator(baseMock.Object);

            var request = new CreateGame
            {
                LanguageId = 1,
                UserId = 1,
                ShowAdultContent = true
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameNameRequired, result.Error);
        }
    }
}
