using MeanCards.Common.Constants;
using MeanCards.Model.Core.Games;
using MeanCards.Validators.Games;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests
{
    public class CreateGameValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var validator = new CreateGameValidator();

            var request = new CreateGame
            {
                LanguageId = 1,
                Name = "Test",
                OwnerId = 1,
                ShowAdultContent = true
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task ReturnFailureForMissingLanguage()
        {
            var validator = new CreateGameValidator();

            var request = new CreateGame
            {
                Name = "Test",
                OwnerId = 1,
                ShowAdultContent = true
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameLanguageRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingName()
        {
            var validator = new CreateGameValidator();

            var request = new CreateGame
            {
                LanguageId = 1,
                OwnerId = 1,
                ShowAdultContent = true
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameNameRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingOwner()
        {
            var validator = new CreateGameValidator();

            var request = new CreateGame
            {
                LanguageId = 1,
                Name = "Test",
                ShowAdultContent = true
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameOwnerRequired, result.Error);
        }
    }
}
