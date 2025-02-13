using SimpleMigrations;
using StatementIQ.Data.Common.Migrations.Contracts;

namespace Utg.Accounting.Data.Migrations
{
    [Migration(2020072901, "initial objects")]
    public class InitialObjects : Migration, IPostgreSqlInitialObject
    {
        protected override void Up()
        {
        }

        protected override void Down()
        {
        }
    }
}