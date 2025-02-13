using System;
using System.Data.SqlClient;
using System.Threading;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StatementIQ.Data.Common.Migrations.Contracts;
using StatementIQ.Data.Common.Migrations.Simple;

namespace StatementIQ.Data.Common.Migrations.DatabaseCreators
{
    public class SqlServerDatabaseCreator : IDatabaseCreator
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;

        public SqlServerDatabaseCreator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _loggerFactory = _serviceProvider.GetService<ILoggerFactory>();
        }

        public void CreateDb()
        {
            var options = _serviceProvider.GetRequiredService<SimpleMigrationOptions>();
            var logger = _loggerFactory.CreateLogger<Bootstrapper>();
            var builder = new SqlConnectionStringBuilder(options.ConnectionString);
            var dbName = builder.InitialCatalog;

            builder.InitialCatalog = "master";
            var connString = builder.ConnectionString;

            const int timeout = 120;
            // we'll give the SQL Server some time to become available
            var stopAt = DateTimeOffset.Now.AddSeconds(timeout);
            var attempt = 0;

            logger.LogInformation(
                $"Starting MSSQL bootstrapping. We're going to give this up to {timeout}s to succeed, and it will end in failure if your db server can't be reached",
                timeout);

            while (true)
            {
                attempt += 1;
                try
                {
                    using var conn = new SqlConnection(connString);
                    const string sql = @"
                                if not exists (select [name] from sys.databases where [name]='{0}')
                                BEGIN
                                    CREATE DATABASE {0}
                                END";
                    conn.Open();
                    var query = string.Format(sql, dbName);
                    conn.Execute(query);

                    logger.LogDebug("Ran {SQL} on Attempt #{AttemptNumber}", query, attempt);

                    break;
                }
                catch (Exception ex)
                {
                    const int delay = 2000;
                    logger.LogWarning("Connection Attempt #{AttemptNumber} failed", attempt);

                    Thread.Sleep(delay);

                    if (DateTimeOffset.Now < stopAt) continue;

                    logger.LogError(ex, "Failed to bootstrap the database after making {AttemptNumber} attempts",
                        attempt);

                    throw;
                }
            }
        }
    }
}