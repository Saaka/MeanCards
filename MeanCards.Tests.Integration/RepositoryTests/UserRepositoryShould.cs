using MeanCards.DAL.Interfaces.Repository;
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
            var model = new Model.Creation.Users.CreateUserModel
            {
                DisplayName = "Name",
                Email = "test@test.com",
                ImageUrl = "imageurl",
                Password = "password12",
                UserCode = "12345"
            };

            var userId = await repository.CreateUser(model);

            Assert.NotEqual(0, userId);
        }

        [Fact]
        public async Task CheckIfUserExistsForValidEmail()
        {
            var email = "test1@test.com";
            var user = Fixture.CreateDefaultUser(email: email);
            var repository = Fixture.GetService<IUsersRepository>();

            var exists = await repository.UserExists(email);

            Assert.True(exists);
        }

        [Fact]
        public async Task CheckIfUserNotExistsForInvalidEmail()
        {
            var email = "test1@test.com";
            var user = Fixture.CreateDefaultUser(email: email);
            var repository = Fixture.GetService<IUsersRepository>();

            var exists = await repository.UserExists("other@mail.com");

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
    }
}
