using System;
using System.Threading;
using System.Threading.Tasks;

namespace StatementIQ.Common.Data
{
    public abstract class Retry : IRetry
    {
        protected Retry(int defaultRetryCount = 2, int defaultRetryDelay = 0)
        {
            DefaultRetryCount = defaultRetryCount;
            DefaultRetryDelay = defaultRetryDelay;
        }

        private int DefaultRetryCount { get; }
        private int DefaultRetryDelay { get; }

        public abstract Task<T> RetryOnException<T>(Func<Task<T>> operation, int? retry = null, int? delay = null,
            CancellationToken cancellationToken = default);

        public abstract Task RetryOnException(Func<Task> operation, int? retry = null, int? delay = null,
            CancellationToken cancellationToken = default);

        protected virtual int SetDefaultRetryDelay(int? delay)
        {
            return delay ?? DefaultRetryDelay;
        }

        protected virtual int? SetDefaultRetryCount(int? retry)
        {
            return retry ?? DefaultRetryCount;
        }

        protected virtual bool ShouldThrow(int? retry, int count)
        {
            return count >= retry;
        }
    }
}