using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace StatementIQ.Common.Data
{
    public interface IUnitOfWork<out TConnection, out TTransaction>
        where TConnection : IDbConnection
        where TTransaction : IDbTransaction
    {
        Task<T> WrapWithRetryAsync<T>(Func<TConnection, TTransaction, Task<T>> operation,
            CancellationToken cancellationToken = default);

        Task WrapWithRetryAsync(Func<TConnection, TTransaction, Task> operation,
            CancellationToken cancellationToken = default);

        Task<T> WrapWithRetryAsync<T>(Func<TConnection, Task<T>> operation,
            CancellationToken cancellationToken = default);

        Task WrapWithRetryAsync(Func<TConnection, Task> operation,
            CancellationToken cancellationToken = default);
    }

    public interface IUnitOfWork : IUnitOfWork<SqlConnection, SqlTransaction>
    {
    }
}