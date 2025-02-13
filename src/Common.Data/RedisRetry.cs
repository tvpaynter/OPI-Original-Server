using System;
using System.Threading;
using System.Threading.Tasks;

namespace StatementIQ.Common.Data
{
    public class RedisRetry : IRetry
    {
        public Task<T> RetryOnException<T>(
            Func<Task<T>> operation, int? retry = null,
            int? delay = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RetryOnException(
            Func<Task> operation, int? retry = null,
            int? delay = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}