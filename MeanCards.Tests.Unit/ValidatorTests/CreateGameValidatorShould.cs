using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Validators.Games;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests
{
    public class CreateGameValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var usersRepo = CreateMock();
            var validator = new CreateGameValidator(usersRepo);

            var request = new CreateGame
            {
                LanguageId = 1,
                Name = "Test",
                UserId = 1,
                ShowAdultContent = true
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task ReturnFailureForMissingLanguage()
        {
            var usersRepo = CreateMock();
            var validator = new CreateGameValidator(usersRepo);

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
            var usersRepo = CreateMock();
            var validator = new CreateGameValidator(usersRepo);

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

        [Fact]
        public async Task ReturnFailureForMissingOwner()
        {
            var usersRepo = CreateMock();
            var validator = new CreateGameValidator(usersRepo);

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

        [Fact]
        public async Task ReturnFailureForNotExistingUser()
        {
            var usersRepo = CreateMock(false);
            var validator = new CreateGameValidator(usersRepo);

            var request = new CreateGame
            {
                LanguageId = 1,
                Name = "Test",
                UserId = 1,
                ShowAdultContent = true
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Users.UserIdNotFound, result.Error);
        }

        private IUsersRepository CreateMock(bool userExists = true)
        {
            var mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.ActiveUserExists(It.IsAny<int>())).Returns(Task.FromResult<bool>(userExists));

            return mock.Object;
        }
    }
}
