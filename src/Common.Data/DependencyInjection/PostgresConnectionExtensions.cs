using System;
using Autofac;
using MandateThat;
using Npgsql;
using StatementIQ.Common.Data.Models.Enums;

namespace StatementIQ.Common.Data.DependencyInjection
{
    public static class PostgresConnectionExtensions
    {
        public static void RegisterPostgresConnection(this ContainerBuilder builder, string connectionString)
        {
            Mandate.That(builder, nameof(builder)).IsNotNull();
            Mandate.That(connectionString, nameof(connectionString)).IsNotNullOrWhiteSpace();

            ConnectionStringValidator.ValidateConnectionString(ProviderType.Postgres, connectionString);

            builder.Register(componentContext =>
            {
                NpgsqlConnection ConnFactory()
                {
                    return new NpgsqlConnection(connectionString);
                }

                return (Func<NpgsqlConnection>) ConnFactory;
            }).As<Func<NpgsqlConnection>>().SingleInstance();
        }
    }
}