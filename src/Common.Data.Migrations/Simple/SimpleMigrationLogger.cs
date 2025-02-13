using System;
using Microsoft.Extensions.Logging;
using SimpleMigrations;
using ILogger = SimpleMigrations.ILogger;

namespace StatementIQ.Data.Common.Migrations.Simple
{
    public class SimpleMigrationLogger : ILogger
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public SimpleMigrationLogger(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<SimpleMigrationLogger>();
        }

        public void BeginMigration(MigrationData migration, MigrationDirection direction)
        {
            WriteLog("{direction} migration: {version}: {name}", direction, migration.Version, migration.FullName);
        }

        public void BeginSequence(MigrationData from, MigrationData to)
        {
            WriteLog("Begin migrating from {fromVersion} to {toVersion}", from.Version, to.Version);
        }

        public void EndMigration(MigrationData migration, MigrationDirection direction)
        {
        }

        public void EndMigrationWithError(Exception exception, MigrationData migration, MigrationDirection direction)
        {
        }

        public void EndSequence(MigrationData from, MigrationData to)
        {
            WriteLog("End migrating from {fromVersion} to {toVersion}", from.Version, to.Version);
        }

        public void EndSequenceWithError(Exception exception, MigrationData from, MigrationData currentVersion)
        {
        }

        public void Info(string message)
        {
            WriteLog(message);
        }

        public void LogSql(string sql)
        {
        }

        private void WriteLog(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}