using MeanCards.DAL;
using MeanCards.DAL.Interfaces.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MeanCards.Tests.Integration.Config
{
    public class DALServiceCollectionFixture : ServiceCollectionFixture
    {
        public const string DefaultLanguageCode = "PL";
        public const string DefaultLanguageName = "Polski";
        public const string DefaultUserName = "Kowalski";
        protected SqliteConnection _connection;

        public override IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            _connection = CreateConnection();
            return serviceCollection
                .RegisterSQLiteInmemoryContext(_connection)
                .RegisterDAL();
        }

        protected virtual SqliteConnection CreateConnection()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return connection;
        }

        public async Task<int> CreateDefaultUser()
        {
            var usersRepository = GetService<IUsersRepository>();
            return await usersRepository.CreateUser(new Model.Creation.CreateUserModel { DisplayName = DefaultUserName });
        }

        public async Task<int> CreateDefaultLanguage()
        {
            var languageRepository = GetService<ILanguagesRepository>();
            return await languageRepository.CreateLanguage(new Model.Creation.CreateLanguageModel { Code = DefaultLanguageCode, Name = DefaultLanguageName });
        }

        public override void Dispose()
        {
            base.Dispose();

            if(_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
