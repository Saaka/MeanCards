using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Creation;
using MeanCards.Tests.Integration.Config;
using System;
using System.Threading.Tasks;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public abstract class BaseRepositoryTests : IDisposable
    {
        public const string DefaultLanguageCode = "PL";
        public const string DefaultLanguageName = "Polski";
        public const string DefaultUserName = "Kowalski";
        protected readonly DALServiceCollectionFixture Fixture;

        public BaseRepositoryTests()
        {
            Fixture = new DALServiceCollectionFixture();
        }

        protected async Task<int> CreateDefaultUser()
        {
            var usersRepository = Fixture.GetService<IUsersRepository>();
            return await usersRepository.CreateUser(new Model.Creation.CreateUserModel { DisplayName = DefaultUserName });
        }

        protected async Task<int> CreateDefaultLanguage()
        {
            var languageRepository = Fixture.GetService<ILanguagesRepository>();
            return await languageRepository.CreateLanguage(new Model.Creation.CreateLanguageModel { Code = DefaultLanguageCode, Name = DefaultLanguageName });
        }

        protected async Task<int> CreateDefaultPlayer(int gameId, int userId)
        {
            var playersRepository = Fixture.GetService<IPlayersRepository>();
            var createPlayer = new CreatePlayerModel
            {
                GameId = gameId,
                UserId = userId
            };

            var playerId = await playersRepository.CreatePlayer(createPlayer);
            return playerId;
        }

        protected async Task<int> CreateDefaultGame(int languageId, int userId, bool showAdultContent = true)
        {
            var gamesRepository = Fixture.GetService<IGamesRepository>();
            var createModel = new CreateGameModel
            {
                GameCode = "gamecode1",
                LanguageId = languageId,
                OwnerId = userId,
                Name = "Test game",
                ShowAdultContent = true
            };

            var gameId = await gamesRepository.CreateGame(createModel);
            return gameId;
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}
