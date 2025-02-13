using SimpleMigrations;

namespace Utg.Accounting.Data.Migrations
{
    [Migration(2021111501, "Create Utg Schema")]
    public class CreateSchema : Migration
    {
        protected override void Up()
        {
            Execute(@"create schema if not exists Utg;");
        }

        protected override void Down()
        {
            Execute(@"drop schema if exists Utg cascade;");
        }
    }
}