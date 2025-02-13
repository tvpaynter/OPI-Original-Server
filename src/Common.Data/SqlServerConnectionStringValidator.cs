using Microsoft.Data.SqlClient;
using MandateThat;
using Npgsql;
using StatementIQ.Common.Data.Exceptions;
using StatementIQ.Common.Data.Models.Enums;

namespace StatementIQ.Common.Data
{
    public static class ConnectionStringValidator
    {
        public static void ValidateConnectionString(ProviderType providerType, string connectionString)
        {
            Mandate.That(connectionString, nameof(connectionString)).IsNotNullOrWhiteSpace();

            if (providerType == ProviderType.MsSql)
                ValidateSqlConnectionString(connectionString);
            else if (providerType == ProviderType.Postgres) ValidatePostgresConnectionString(connectionString);
        }

        private static void ValidateSqlConnectionString(string connectionString)
        {
            Mandate.That(connectionString, nameof(connectionString)).IsNotNullOrWhiteSpace();

            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            ValidateFieldIsNotNullOrEmpty(nameof(connectionStringBuilder.DataSource),
                connectionStringBuilder.DataSource);
            ValidateFieldIsNotNullOrEmpty(nameof(connectionStringBuilder.InitialCatalog),
                connectionStringBuilder.InitialCatalog);
            ValidateFieldIsNotNullOrEmpty(nameof(connectionStringBuilder.UserID), connectionStringBuilder.UserID);
            ValidateFieldIsNotNullOrEmpty(nameof(connectionStringBuilder.Password), connectionStringBuilder.Password);
        }

        private static void ValidatePostgresConnectionString(string connectionString)
        {
            Mandate.That(connectionString, nameof(connectionString)).IsNotNullOrWhiteSpace();

            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);

            ValidateFieldIsNotNullOrEmpty(nameof(connectionStringBuilder.Host), connectionStringBuilder.Host);
            ValidateFieldIsNotNullOrEmpty(nameof(connectionStringBuilder.Port),
                connectionStringBuilder.Port.ToString());
            ValidateFieldIsNotNullOrEmpty(nameof(connectionStringBuilder.Database), connectionStringBuilder.Database);
            ValidateFieldIsNotNullOrEmpty(nameof(connectionStringBuilder.Username), connectionStringBuilder.Username);
            ValidateFieldIsNotNullOrEmpty(nameof(connectionStringBuilder.Password), connectionStringBuilder.Password);
        }

        private static void ValidateFieldIsNotNullOrEmpty(string fieldName, string fieldValue)
        {
            Mandate.That(fieldName, nameof(fieldName)).IsNotNullOrWhiteSpace();

            if (string.IsNullOrWhiteSpace(fieldValue))
                throw new ConnectionStringException($"Validation error. Field '{fieldName}' is required");
        }
    }
}