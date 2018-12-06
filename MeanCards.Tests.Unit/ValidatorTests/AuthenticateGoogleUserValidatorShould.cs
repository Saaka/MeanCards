using MeanCards.Common.Constants;
using MeanCards.Model.Core.Users;
using MeanCards.Validators.Users;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests
{
    public class AuthenticateGoogleUserValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessfulValidationResultForValidRequest()
        {
            var validator = new AuthenticateGoogleUserValidator();

            var request = new AuthenticateGoogleUser
            {
                GoogleId = "123456",
                Email = "test@test.com",
                ImageUrl = "https://picsum.photos/96/96/?image=33",
                DisplayName = "Test"
            };

            var result = await validator.Validate(request);

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task ReturnFailedResultForMissingGoogleId()
        {
            var validator = new AuthenticateGoogleUserValidator();

            var request = new AuthenticateGoogleUser
            {
                Email = "test@test.com",
                ImageUrl = "https://picsum.photos/96/96/?image=33",
                DisplayName = "Test"
            };

            var result = await validator.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Users.GoogleIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailedResultForMissingEmail()
        {
            var validator = new AuthenticateGoogleUserValidator();

            var request = new AuthenticateGoogleUser
            {
                GoogleId = "123456",
                ImageUrl = "https://picsum.photos/96/96/?image=33",
                DisplayName = "Test"
            };

            var result = await validator.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Users.UserEmailRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailedResultForMissingName()
        {
            var validator = new AuthenticateGoogleUserValidator();

            var request = new AuthenticateGoogleUser
            {
                GoogleId = "123456",
                Email = "test@test.com",
                ImageUrl = "https://picsum.photos/96/96/?image=33",
            };

            var result = await validator.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Users.UserNameRequired, result.Error);
        }
    }
}
