using System;
using System.Data.SqlClient;
using Autofac;
using MandateThat;
using StatementIQ.Common.Data.Models.Enums;

namespace StatementIQ.Common.Data.DependencyInjection
{
    public static class SqlConnectionExtensions
    {
        public static void RegisterSqlConnection(this ContainerBuilder builder, string connectionString)
        {
            Mandate.That(builder, nameof(builder)).IsNotNull();
            Mandate.That(connectionString, nameof(connectionString)).IsNotNullOrWhiteSpace();

            ConnectionStringValidator.ValidateConnectionString(ProviderType.MsSql, connectionString);

            builder.Register(componentContext =>
            {
                SqlConnection ConnFactory()
                {
                    return new SqlConnection(connectionString);
                }

                return (Func<SqlConnection>) ConnFactory;
            }).As<Func<SqlConnection>>().SingleInstance();
        }
    }
}