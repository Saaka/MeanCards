﻿using MeanCards.Common;
using MeanCards.DAL;
using MeanCards.GameManagement;
using MeanCards.Tests.Base.Fixtures;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace MeanCards.Tests.Integration.Config
{
    public class DomainServiceCollectionFixture : ServiceCollectionFixture
    {
        protected SqliteConnection _connection;

        public override IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            _connection = CreateConnection();
            return serviceCollection
                .RegisterSQLiteInmemoryContext(_connection)
                .RegisterDAL()
                .RegisterIdentityStore()
                .RegisterDomainServices()
                .RegisterCommon()
                .RegisterLegacyHandlerInterfaces();
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
