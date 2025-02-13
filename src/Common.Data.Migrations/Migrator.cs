using System;
using StatementIQ.Data.Common.Migrations.Simple;

namespace StatementIQ.Data.Common.Migrations
{
    public static class Migrator
    {
        public static void Run(IServiceProvider serviceProvider)
        {
            var bootstrapper = new Bootstrapper(serviceProvider);
            bootstrapper.CreateDb();
            bootstrapper.Migrate();
        }
    }
}