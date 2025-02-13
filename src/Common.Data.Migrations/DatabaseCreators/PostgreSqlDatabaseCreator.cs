using System;
using System.Threading;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using StatementIQ.Data.Common.Migrations.Contracts;
using StatementIQ.Data.Common.Migrations.Simple;

namespace StatementIQ.Data.Common.Migrations.DatabaseCreators
{
    public class PostgreSqlDatabaseCreator : IDatabaseCreator
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;

        public PostgreSqlDatabaseCreator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _loggerFactory = _serviceProvider.GetService<ILoggerFactory>();
        }

        public void CreateDb()
        {
            var options = _serviceProvider.GetRequiredService<SimpleMigrationOptions>();
            var logger = _loggerFactory.CreateLogger<Bootstrapper>();
            var builder = new NpgsqlConnectionStringBuilder(options.ConnectionString);
            var dbName = builder.Database;

            builder.Database = "postgres";
            var connString = builder.ConnectionString;

            const int timeout = 120;
            // we'll give the SQL Server some time to become available
            var stopAt = DateTimeOffset.Now.AddSeconds(timeout);
            var attempt = 0;

            logger.LogInformation(
                $"Starting PostgreSql bootstrapping. We're going to give this up to {timeout}s to succeed, and it will end in failure if your db server can't be reached",
                timeout);
            while (true)
            {
                attempt += 1;
                try
                {
                    using var conn = new NpgsqlConnection(connString);

                    conn.Open();

                    var isDbExists =
                        conn.ExecuteScalar<int>($"SELECT 1 FROM pg_database WHERE datname = '{dbName.ToLower()}'");

                    if (isDbExists == 0)
                    {
                        var query = $"CREATE DATABASE {dbName}";
                        conn.Execute(query);
                        logger.LogDebug("Ran {SQL} on Attempt #{AttemptNumber}", query, attempt);
                    }

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