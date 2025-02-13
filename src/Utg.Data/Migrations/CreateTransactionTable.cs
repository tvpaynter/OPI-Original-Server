using System;
using SimpleMigrations;

namespace Utg.Accounting.Data.Migrations
{
	[Migration(2021111502, "Create [Utg].[Transaction] Table")]
	public class CreateTransactionTable : Migration
	{
		protected override void Up()
		{
			Execute(@"create table if not exists Utg.Transaction
                    (
					    Id bigint primary key,
						TransactionId bigint NOT NULL,
						CreatedAt timestamp NOT NULL,
						CreatedBy bigint NOT NULL,
						Amount numeric(18,2),
						RequestData varchar,
						ResponseData varchar,
						ResponseType varchar,
						TransactionType varchar,
						IpAddress inet,
						HostName varchar null,
						ProcessingTime bigint default 0,
						ApprovedAmount decimal(18, 2) default 0
                    );");
		}

		protected override void Down()
		{
			Execute(@"DROP TABLE IF EXISTS Utg.Transaction");
		}
	}
}
