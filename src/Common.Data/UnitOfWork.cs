using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using MandateThat;
using Microsoft.Extensions.Logging;

namespace StatementIQ.Common.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(Func<SqlConnection> connection, IRetry retry, ILogger<UnitOfWork> logger)
        {
            Mandate.That(connection, nameof(connection)).IsNotNull();
            Mandate.That(retry, nameof(retry)).IsNotNull();
            Mandate.That(logger, nameof(logger)).IsNotNull();

            Connection = connection;
            Retry = retry;
            Logger = logger;
        }

        private Func<SqlConnection> Connection { get; }
        private IRetry Retry { get; }
        private ILogger Logger { get; }

        /// <summary>
        ///     Wraps operation with retry <see cref="Data.Retry" /> and connection/transaction creation
        ///     <see cref="SqlTransaction" />>
        /// </summary>
        /// <param name="operation">Operation to be wrapped, delegate of type Func</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <typeparam name="T">Type parameter</typeparam>
        /// <returns>Returns task</returns>
        public Task<T> WrapWithRetryAsync<T>(
            Func<SqlConnection, SqlTransaction, Task<T>> operation,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(operation, nameof(operation)).IsNotNull();

            return Retry.RetryOnException(async () =>
            {
                Logger.LogInformation("Creating connection");

                await using var connection = Connection();
                try
                {
                    await connection.OpenAsync(cancellationToken)
                        .ConfigureAwait(false);

                    Logger.LogInformation("Starting transaction");

                    await using var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);

                    T valueToReturn;
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
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Wraps operation with retry <see cref="Data.Retry" /> and connection/transaction creation
        ///     <see cref="SqlTransaction" />>
        /// </summary>
        /// <param name="operation">Operation to be wrapped, delegate of type Func</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns task</returns>
        public Task WrapWithRetryAsync(
            Func<SqlConnection, SqlTransaction, Task> operation,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(operation, nameof(operation)).IsNotNull();

            return Retry.RetryOnException(async () =>
            {
                Logger.LogInformation("Creating connection");
                
                await using var connection = Connection();
                try
                {
                    await connection.OpenAsync(cancellationToken)
                        .ConfigureAwait(false);
                    
                    await connection.OpenAsync(cancellationToken)
                        .ConfigureAwait(false);

                    Logger.LogInformation("Starting transaction");

                    await using var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

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
                    
                }
                finally
                {
                    await connection.CloseAsync();
                }


            }, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Wraps operation with retry <see cref="Data.Retry" /> and connection creation <see cref="SqlTransaction" />>
        /// </summary>
        /// <param name="operation">Operation to be wrapped, delegate of type Func</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <typeparam name="T">Type parameter</typeparam>
        /// <returns>Returns task</returns>
        public Task<T> WrapWithRetryAsync<T>(
            Func<SqlConnection, Task<T>> operation,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(operation, nameof(operation)).IsNotNull();

            return Retry.RetryOnException(async () =>
            {
                Logger.LogInformation("Creating connection");

                await using var connection = Connection();
                try
                {

                    await connection.OpenAsync(cancellationToken)
                        .ConfigureAwait(false);

                    Logger.LogInformation("Trying to execute the operation");

                    return await operation(connection)
                        .ConfigureAwait(false);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Wraps operation with retry <see cref="Data.Retry" /> and connection creation <see cref="SqlTransaction" />>
        /// </summary>
        /// <param name="operation">Operation to be wrapped, delegate of type Func</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns task</returns>
        public Task WrapWithRetryAsync(
            Func<SqlConnection, Task> operation,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(operation, nameof(operation)).IsNotNull();

            return Retry.RetryOnException(async () =>
            {
                Logger.LogInformation("Creating connection");

                await using var connection = Connection();

                try
                {
                    await connection.OpenAsync(cancellationToken)
                        .ConfigureAwait(false);

                    Logger.LogInformation("Trying to execute the operation");

                    await operation(connection)
                        .ConfigureAwait(false);
                }
                finally
                {
                    await connection.CloseAsync();
                }
                
            }, cancellationToken: cancellationToken);
        }
    }
}