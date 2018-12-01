using MeanCards.DAL;
using MeanCards.DAL.Interfaces.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using MeanCards.Model.DAL.Creation.Users;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DAL.Creation.Languages;
using MeanCards.Tests.Base.Fixtures;

namespace MeanCards.Tests.Integration.Config
{
    public class DALServiceCollectionFixture : ServiceCollectionFixture
    {
        protected SqliteConnection _connection;

        public override IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            _connection = CreateConnection();
            return serviceCollection
                .RegisterSQLiteInmemoryContext(_connection)
                .RegisterDAL()
                .RegisterIdentityStore();
        }

        public async Task<int> CreateDefaultUser(
            string userName = "Kowalski",
            string email = "test @test.com",
            string password = "TestPassword1!",
            string userCode = "12345")
        {
            var usersRepository = GetService<IUsersRepository>();

            var result = await usersRepository.CreateUser(new CreateUserModel { DisplayName = userName, Email = email, Password = password, Code = userCode });
            return result.Model.UserId;
        }

        public async Task<int> CreateDefaultLanguage(
            string code = "PL",
            string name = "Polski")
        {
            var languageRepository = GetService<ILanguagesRepository>();
            return await languageRepository.CreateLanguage(new CreateLanguageModel { Code = code, Name = name });
        }

        public async Task<int> CreateDefaultPlayer(
            int gameId,
            int userId)
        {
            var playersRepository = GetService<IPlayersRepository>();
            var createPlayer = new CreatePlayerModel
            {
                GameId = gameId,
                UserId = userId
            };

            var player = await playersRepository.CreatePlayer(createPlayer);
            return player.PlayerId;
        }

        public async Task<int> CreateDefaultGame(
            int languageId,
            int userId,
            string gameCode = "gamecode1",
            string gameName = "Test game",
            bool showAdultContent = true)
        {
            var gamesRepository = GetService<IGamesRepository>();
            var createModel = new CreateGameModel
            {
                Code = gameCode,
                LanguageId = languageId,
                OwnerId = userId,
                Name = gameName,
                ShowAdultContent = showAdultContent
            };

            var game = await gamesRepository.CreateGame(createModel);
            return game.GameId;
        }

        protected virtual SqliteConnection CreateConnection()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return connection;
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
