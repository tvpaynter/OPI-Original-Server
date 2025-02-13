using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleMigrations;

namespace StatementIQ.Data.Common.Migrations.Simple
{
    public class CustomMigrationProvider : IMigrationProvider
    {
        public CustomMigrationProvider(IEnumerable<Assembly> assemblies)
        {
            MigrationTypes = assemblies.SelectMany(a => a.GetTypes()).ToArray();
        }

        public Type[] MigrationTypes { get; set; } = new Type[0];

        public IEnumerable<MigrationData> LoadMigrations()
        {
            var migrations = from type in MigrationTypes
                let attribute = type.GetCustomAttribute<MigrationAttribute>()
                where attribute != null
                select new MigrationData(attribute.Version, attribute.Description, type.GetTypeInfo());
            return migrations;
        }
    }
}