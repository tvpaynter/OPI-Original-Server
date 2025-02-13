using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleMigrations;
using StatementIQ.Data.Common.Migrations.Contracts;

namespace StatementIQ.Data.Common.Migrations.Simple
{
    internal class Bootstrapper
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;

        public Bootstrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _loggerFactory = _serviceProvider.GetService<ILoggerFactory>();
        }

        internal void CreateDb()
        {
            var databaseCreator = _serviceProvider.GetRequiredService<IDatabaseCreator>();
            databaseCreator.CreateDb();
        }

        internal void Migrate()
        {
            var logger = _loggerFactory.CreateLogger<Bootstrapper>();

            try
            {
                logger.LogInformation("Running migrations...");

                var migrationsAssemblies = GetMigrationAssembly();

                if (migrationsAssemblies != null && !migrationsAssemblies.Any())
                    throw new ArgumentNullException("No migration assemblies found");

                logger.LogInformation("Migration assemblies: {migrationsAssembly}",
                    string.Join(',', migrationsAssemblies.Select(a => a.FullName)));

                var dbProvider = GetDatabaseProvider();

                var customProvider = new CustomMigrationProvider(migrationsAssemblies);

                var migrator =
                    new SimpleMigrator(customProvider, dbProvider, new SimpleMigrationLogger(_loggerFactory));
                migrator.Load();
                logger.LogInformation("Current Migration: {currentMigration}", migrator.CurrentMigration);
                logger.LogInformation("Latest Migration: {latest}", migrator.LatestMigration);
                logger.LogInformation("Attempting to run to latest...");
                migrator.MigrateToLatest();

                logger.LogInformation("Migration complete.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to migrate");
                throw;
            }
        }

        private IEnumerable<Assembly> GetMigrationAssembly()
        {
            var initialObjects = _serviceProvider.GetRequiredService<InitialObjectList>();

            foreach (var initialObject in initialObjects)
                yield return initialObject.Assembly;
        }

        private IDatabaseProvider<DbConnection> GetDatabaseProvider()
        {
            return _serviceProvider.GetRequiredService<IDatabaseProvider<DbConnection>>();
        }
    }
}