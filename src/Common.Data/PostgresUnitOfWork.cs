using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MandateThat;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace StatementIQ.Common.Data
{
    public class PostgresUnitOfWork : IUnitOfWork<NpgsqlConnection, NpgsqlTransaction>
    {
        public PostgresUnitOfWork
        (Func<NpgsqlConnection> connection,
            IRetry retry,
            ILogger<PostgresUnitOfWork> logger)
        {
            Mandate.That(connection, nameof(connection)).IsNotNull();
            Mandate.That(retry, nameof(retry)).IsNotNull();
            Mandate.That(logger, nameof(logger)).IsNotNull();

            Connection = connection;
            Retry = retry;
            Logger = logger;
        }

        private Func<NpgsqlConnection> Connection { get; }
        private IRetry Retry { get; }
        private ILogger Logger { get; }

        public Task<T> WrapWithRetryAsync<T>(
            Func<NpgsqlConnection, NpgsqlTransaction, Task<T>> operation,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(operation, nameof(operation)).IsNotNull();

            return Retry.RetryOnException(async () =>
            {
                T valueToReturn;

                Logger.LogInformation("Creating connection");

                await using var connection = Connection();

                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                Logger.LogInformation("Starting transaction");

                await using var transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadUncommitted, cancellationToken);

                try
                {
                    Logger.LogInformation("Trying to execute the operation");

                    valueToReturn = await operation(connection, transaction)
                        .ConfigureAwait(false);

                    await transaction.CommitAsync(cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    await transaction.RollbackAsync(cancellationToken)
                        .ConfigureAwait(false);

                    Logger.LogError(exception, exception.Message);

                    throw;
                }

                return valueToReturn;
            }, cancellationToken: cancellationToken);
        }

        public Task WrapWithRetryAsync(
            Func<NpgsqlConnection, NpgsqlTransaction, Task> operation,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(operation, nameof(operation)).IsNotNull();

            return Retry.RetryOnException(async () =>
            {
                Logger.LogInformation("Creating connection");

                await using var connection = Connection();

                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                Logger.LogInformation("Starting transaction");

                await using var transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadUncommitted, cancellationToken);

                try
                {
                    Logger.LogInformation("Trying to execute the operation");

                    await operation(connection, transaction)
                        .ConfigureAwait(false);

                    await transaction.CommitAsync(cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    await transaction.RollbackAsync(cancellationToken)
                        .ConfigureAwait(false);

                    Logger.LogError(exception, exception.Message);

                    throw;
                }
            }, cancellationToken: cancellationToken);
        }

        public Task<T> WrapWithRetryAsync<T>(
            Func<NpgsqlConnection, Task<T>> operation,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(operation, nameof(operation)).IsNotNull();

            return Retry.RetryOnException(async () =>
            {
                Logger.LogInformation("Creating connection");

                await using var connection = Connection();

                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                Logger.LogInformation("Trying to execute the operation");

                return await operation(connection)
                    .ConfigureAwait(false);
            }, cancellationToken: cancellationToken);
        }

        public Task WrapWithRetryAsync(
            Func<NpgsqlConnection, Task> operation,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(operation, nameof(operation)).IsNotNull();

            return Retry.RetryOnException(async () =>
            {
                Logger.LogInformation("Creating connection");

                await using var connection = Connection();

                await connection.OpenAsync(cancellationToken)
                    .ConfigureAwait(false);

                Logger.LogInformation("Trying to execute the operation");

                await operation(connection)
                    .ConfigureAwait(false);
            }, cancellationToken: cancellationToken);
        }
    }
}