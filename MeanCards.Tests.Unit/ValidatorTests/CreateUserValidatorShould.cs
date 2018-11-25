using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Users;
using MeanCards.Validators;
using MeanCards.Validators.User;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests
{
    public class CreateUserValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var repoMock = CreateRepositoryMock();
            var validator = new CreateUserValidator(repoMock);

            var request = GetRequest();

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task ReturnFailureForShortPassword()
        {
            var repoMock = CreateRepositoryMock();
            var validator = new CreateUserValidator(repoMock);

            var request = GetRequest(password: "12345");

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.PasswordTooShort, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForDuplicatedEmail()
        {
            var repoMock = CreateRepositoryMock(isDuplicatedEmail: true);
            var validator = new CreateUserValidator(repoMock);

            var request = GetRequest();

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.DuplicatedEmail, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForDuplicatedName()
        {
            var repoMock = CreateRepositoryMock(isDuplicatedName: true);
            var validator = new CreateUserValidator(repoMock);

            var request = GetRequest();

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.DuplicatedUserName, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingEmail()
        {
            var repoMock = CreateRepositoryMock();
            var validator = new CreateUserValidator(repoMock);

            var request = GetRequest(email: "");

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.EmailRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingName()
        {
            var repoMock = CreateRepositoryMock();
            var validator = new CreateUserValidator(repoMock);

            var request = GetRequest(name: "");

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.NameRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingPassword()
        {
            var repoMock = CreateRepositoryMock();
            var validator = new CreateUserValidator(repoMock);

            var request = GetRequest(password: "");

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.PasswordRequired, result.Error);
        }

        private CreateUser GetRequest(
            string name = "Test",
            string email = "Test",
            string password = "Test12")
        {
            return new CreateUser
            {
                DisplayName = name,
                Email = email,
                Password = password
            };
        }

        private IUsersRepository CreateRepositoryMock(bool isDuplicatedEmail = false, bool isDuplicatedName = false)
        {
            var mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.UserEmailExists(It.IsAny<string>())).Returns(Task.FromResult<bool>(isDuplicatedEmail));
            mock.Setup(m => m.UserNameExists(It.IsAny<string>())).Returns(Task.FromResult<bool>(isDuplicatedName));

            return mock.Object;
        }
    }
}
