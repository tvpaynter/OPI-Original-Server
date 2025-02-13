using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace StatementIQ.Common.Data
{
    public sealed class SqlRetry : Retry
    {
        private static readonly List<int> TransientErrorNumbers =
            new List<int>
            {
                4060, 40197, 40501, 40613,
                49918, 49919, 49920, 11001
            };

        public SqlRetry(ILogger<SqlRetry> log, int defaultRetryCount = 2, int defaultRetryDelay = 0)
            : base(defaultRetryCount, defaultRetryDelay)
        {
            Log = log;
        }

        private ILogger<SqlRetry> Log { get; }

        public override async Task<T> RetryOnException<T>(Func<Task<T>> operation, int? retry = null, int? delay = null,
            CancellationToken cancellationToken = default)
        {
            retry = SetDefaultRetryCount(retry);

            var count = 0;

            while (true)
                try
                {
                    return await operation().ConfigureAwait(false);
                }
                catch (SqlException ex)
                {
                    count++;

                    Log.LogError($"SqlError: {ex.Message}");

                    if (ShouldThrow(retry, count) || ShouldRetry(ex)) throw;

                    Log.LogInformation("Process will retry the operation");

                    await Task.Delay(SetDefaultRetryDelay(delay), cancellationToken)
                        .ConfigureAwait(false);

                    cancellationToken.ThrowIfCancellationRequested();
                }
        }

        public override async Task RetryOnException(Func<Task> operation, int? retry = null, int? delay = null,
            CancellationToken cancellationToken = default)
        {
            retry = SetDefaultRetryCount(retry);

            var count = 0;

            while (true)
                try
                {
                    await operation().ConfigureAwait(false);

                    break;
                }
                catch (SqlException ex)
                {
                    count++;

                    Log.LogError($"SqlError: {ex.Message}");

                    if (ShouldThrow(retry, count) || ShouldRetry(ex)) throw;

                    Log.LogInformation("Process will retry the operation");

                    await Task.Delay(SetDefaultRetryDelay(delay), cancellationToken)
                        .ConfigureAwait(false);

                    cancellationToken.ThrowIfCancellationRequested();
                }
        }

        private static bool ShouldRetry(SqlException ex)
        {
            return !TransientErrorNumbers.Contains(ex.Number);
        }
    }
}