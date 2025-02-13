using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StatementIQ
{
    public static class AsyncEx
    {
        public static async Task ParallelForEachAsync(this IEnumerable<Task> tasksList,  int maxDegreeOfParallelism, CancellationToken cancellationToken)
        {
            var semaphoreSlim = new SemaphoreSlim(maxDegreeOfParallelism);
            var tcs = new TaskCompletionSource<object>();
            var exceptions = new ConcurrentBag<Exception>();
            var addingCompleted = false;

            foreach (var item in tasksList)
            {
                await semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
                    
                item.ContinueWith(t =>
                {
                    semaphoreSlim.Release();

                    if (t.Exception != null)
                    {
                        exceptions.Add(t.Exception);
                    }

                    if (Volatile.Read(location: ref addingCompleted) && semaphoreSlim.CurrentCount == maxDegreeOfParallelism)
                    {
                        tcs.TrySetResult(null);
                    }
                }, cancellationToken);
            }

            Volatile.Write(ref addingCompleted, true);
            
            await tcs.Task;
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}