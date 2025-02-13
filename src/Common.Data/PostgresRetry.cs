using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace StatementIQ.Common.Data
{
    public class PostgresRetry : Retry
    {
        public PostgresRetry(
            ILogger<PostgresRetry> log,
            int defaultRetryCount = 2,
            int defaultRetryDelay = 0)
            : base(defaultRetryCount, defaultRetryDelay)
        {
            Log = log;
        }

        private ILogger<PostgresRetry> Log { get; }

        public override async Task<T> RetryOnException<T>(
            Func<Task<T>> operation,
            int? retry = null,
            int? delay = null,
            CancellationToken cancellationToken = default)
        {
            retry = SetDefaultRetryCount(retry);

            var count = 0;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                while (true)
                    try
                    {
                        return await operation().ConfigureAwait(false);
                    }
                    catch (NpgsqlException ex)
                    {
                        count++;

                        Log.LogError($"PostgresError: {ex.Message}");

                        if (ShouldThrow(retry, count) || ShouldRetry(ex)) throw;

                        Log.LogInformation("Process will retry the operation");

                        await Task.Delay(SetDefaultRetryDelay(delay), cancellationToken)
                            .ConfigureAwait(false);

                        cancellationToken.ThrowIfCancellationRequested();
                    }
            }
            finally
            {
                stopwatch.Stop();

                Log.LogInformation($"Query retries count: {count}, time elapsed {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        public override async Task RetryOnException(
            Func<Task> operation,
            int? retry = null,
            int? delay = null,
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
                catch (NpgsqlException ex)
                {
                    count++;

                    Log.LogError($"PostgresError: {ex.Message}");

                    if (ShouldThrow(retry, count) || ShouldRetry(ex)) throw;

                    Log.LogInformation("Process will retry the operation");

                    await Task.Delay(SetDefaultRetryDelay(delay), cancellationToken)
                        .ConfigureAwait(false);

                    cancellationToken.ThrowIfCancellationRequested();
                }
        }

        private static bool ShouldRetry(NpgsqlException ex)
        {
            return ex.IsTransient;
        }
    }
}