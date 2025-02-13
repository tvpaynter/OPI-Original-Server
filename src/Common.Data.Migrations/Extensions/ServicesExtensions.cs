using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;
using StatementIQ.Data.Common.Migrations.Contracts;
using StatementIQ.Data.Common.Migrations.DatabaseCreators;
using StatementIQ.Data.Common.Migrations.Simple;

namespace StatementIQ.Data.Common.Migrations.Extensions
{
    public static class ServicesExtensions
    {
        private static IEnumerable<Type> GetInitialObjects<T>() where T : IInitialObject
        {
            var binPath = AppDomain.CurrentDomain.BaseDirectory;

            foreach (var dll in Directory.GetFiles(binPath, "*.dll"))
            {
                var initialObjectType = typeof(T);
                var types = Assembly.LoadFile(dll).GetTypes()
                    .Where(type => initialObjectType.IsAssignableFrom(type));
                foreach (var initialObject in types) yield return initialObject;
            }
        }

        public static void SetupMigration(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new SimpleMigrationOptions
            {
                ConnectionString = configuration["SimpleMigrations:ConnectionString"],
                SchemaName = configuration["SimpleMigrations:SchemaName"],
                DbProvider = configuration["SimpleMigrations:DbProvider"]
            };

            services.AddSingleton(options);
            Func<IServiceProvider, IDatabaseProvider<DbConnection>> gettingPostgreDatabaseProviderFunction = null;

            switch (options.DbProvider)
            {
                case "SqlServer":
                    services.AddSingleton<IDatabaseCreator, SqlServerDatabaseCreator>();
                    gettingPostgreDatabaseProviderFunction =
                        serviceProvider =>
                            new MssqlDatabaseProvider(new SqlConnection(options.ConnectionString))
                                {SchemaName = options.SchemaName, CreateSchema = true};
                    services.AddSingleton(new InitialObjectList(GetInitialObjects<ISqlServerInitialObject>()));
                    break;

                case "PostgreSql":
                    services.AddSingleton<IDatabaseCreator, PostgreSqlDatabaseCreator>();
                    gettingPostgreDatabaseProviderFunction =
                        serviceProvider =>
                            new PostgresqlDatabaseProvider(new NpgsqlConnection(options.ConnectionString))
                                {SchemaName = options.SchemaName, CreateSchema = true};
                    services.AddSingleton(new InitialObjectList(GetInitialObjects<IPostgreSqlInitialObject>()));
                    break;

                default:
                    throw new InvalidEnumArgumentException($"Invalid DbProvider name: {options.DbProvider}");
            }

            services.AddSingleton(gettingPostgreDatabaseProviderFunction);
        }
    }
}