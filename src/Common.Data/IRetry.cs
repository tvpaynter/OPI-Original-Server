using System;
using System.Threading;
using System.Threading.Tasks;

namespace StatementIQ.Common.Data
{
    public interface IRetry
    {
        Task<T> RetryOnException<T>(Func<Task<T>> operation, int? retry = null, int? delay = null,
            CancellationToken cancellationToken = default);

        Task RetryOnException(Func<Task> operation, int? retry = null, int? delay = null,
            CancellationToken cancellationToken = default);
    }
}