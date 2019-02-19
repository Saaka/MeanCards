using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Access.Users;
using MeanCards.Model.DAL.Creation.Users;
using MeanCards.Tests.Integration.BaseTests;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class UserRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreateUser()
        {
            var repository = Fixture.GetService<IUsersRepository>();
            var model = new CreateUserModel
            {
                DisplayName = "Name",
                Email = "test@test.com",
                ImageUrl = "imageurl",
                Password = "password12",
                Code = "12345"
            };

            var user = await repository.CreateUser(model);

            Assert.NotNull(user);
            Assert.NotNull(user.Model);
            Assert.NotEqual(0, user.Model.UserId);
            Assert.Equal("Name", user.Model.DisplayName);
            Assert.Equal("Name", user.Model.UserName);
            Assert.Equal("12345", user.Model.Code);
            Assert.Equal("test@test.com", user.Model.Email);
            Assert.Equal("imageurl", user.Model.ImageUrl);
            TestHelper.AssertNumberOfFields<CreateUserModel>(5);
        }

        [Fact]
        public async Task CheckIfUserExistsForValidEmail()
        {
            var email = "test1@test.com";
            var user = Fixture.CreateDefaultUser(email: email);
            var repository = Fixture.GetService<IUsersRepository>();

            var exists = await repository.UserEmailExists(email);

            Assert.True(exists);
        }

        [Fact]
        public async Task CheckIfUserNotExistsForInvalidEmail()
        {
            var email = "test1@test.com";
            var user = Fixture.CreateDefaultUser(email: email);
            var repository = Fixture.GetService<IUsersRepository>();

            var exists = await repository.UserEmailExists("other@mail.com");

            Assert.False(exists);
        }

        [Fact]
        public async Task CheckIfUserExistsForValidName()
        {
            var name = "TestName";
            var user = Fixture.CreateDefaultUser(userName: name);
            var repository = Fixture.GetService<IUsersRepository>();

            var exists = await repository.UserNameExists(name);

            Assert.True(exists);
        }

        [Fact]
        public async Task CheckIfUserNotExistsForInvalidName()
        {
            var name = "TestName";
            var user = Fixture.CreateDefaultUser(userName: name);
            var repository = Fixture.GetService<IUsersRepository>();

            var exists = await repository.UserNameExists("OtherName");

            Assert.False(exists);
        }

        [Fact]
        public async Task ReturnUserForValidCredentials()
        {
            var credentials = new GetUserByCredentialsModel
            {
                Email = "user@test.com",
                Password = "Pass123"
            };
            var user = Fixture.CreateDefaultUser(
                email: credentials.Email,
                password: credentials.Password);
            var repository = Fixture.GetService<IUsersRepository>();
            
            var result = await repository.GetUserByCredentials(credentials);

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.Model);
        }

        [Fact]
        public async Task NotReturnUserForInalidCredentials()
        {
            var credentials = new GetUserByCredentialsModel
            {
                Email = "user@test.com",
                Password = "Pass123"
            };
            var user = Fixture.CreateDefaultUser(
                email: credentials.Email,
                password: "Invalid123");
            var repository = Fixture.GetService<IUsersRepository>();
            
            var result = await repository.GetUserByCredentials(credentials);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Null(result.Model);
        }
    }
}
