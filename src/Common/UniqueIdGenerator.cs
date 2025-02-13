using System;
using System.Threading;
using System.Threading.Tasks;
using StatementIQ.Exceptions;

namespace StatementIQ
{
    /// <summary>
    ///     To generate unique id's - based on twitter snowflake
    /// </summary>
    public sealed class UniqueIdGenerator : IUniqueIdGenerator
    {
        public const string WorkerIdPropertyName = "workerId";
        private const long MaxWorkerId = -1 ^ (-1 << WorkerIdBits);
        private const int SequenceBits = 6;
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);
        private const int TimestampLeftShift = WorkerIdBits + SequenceBits;
        private const int WorkerIdBits = 10;
        private static readonly DateTime Epoch = new DateTime(2017, 8, 12);
        private readonly Semaphore Semaphore = new Semaphore(1, 1);
        private readonly long ShiftedWorkerId;
        private long lastTimestamp = -1L;
        private long sequence;

        public UniqueIdGenerator(long workerId)
        {
            if (workerId > MaxWorkerId || workerId < 0)
                throw new ArgumentException($"worker Id can't be greater than {MaxWorkerId} or less than 0");

            WorkerId = workerId;
            ShiftedWorkerId = (WorkerId & MaxWorkerId) << SequenceBits;
        }

        public long WorkerId { get; }

        public long[] BlockOfIds(int count)
        {
            var ids = new long[count];

            for (var i = 0; i < count; i++) ids[i] = NextId();

            return ids;
        }

        public async Task<long[]> BlockOfIdsAsync(int count, CancellationToken cancellationToken = default)
        {
            var ids = new long[count];

            for (var i = 0; i < count; i++) ids[i] = await NextIdAsync(cancellationToken).ConfigureAwait(false);

            return ids;
        }

        public long NextId()
        {
            return GetId(GenTimestamp());
        }

        public Task<long> NextIdAsync(CancellationToken cancellationToken = default)
        {
            return Task.Run(() => GetId(GenTimestamp()), cancellationToken);
        }

        private static long GetTimestamp()
        {
            return (long) (DateTime.UtcNow - Epoch).TotalMilliseconds;
        }

        private void ClockCheck(long timestamp)
        {
            if (timestamp < lastTimestamp)
                throw new InvalidSystemClockException(
                    $"Clock moved backwards.  Refusing to generate id for {lastTimestamp - timestamp} milliseconds"
                );
        }

        private long GenerateId(long timestamp)
        {
            return (timestamp << TimestampLeftShift) | ShiftedWorkerId | sequence;
        }

        private long GenTimestamp()
        {
            var timestamp = GetTimestamp();

            ClockCheck(timestamp);
            return timestamp;
        }

        private long GetId(long timestamp)
        {
            Semaphore.WaitOne();

            try
            {
                if (lastTimestamp == timestamp)
                {
                    sequence = NextSequence();

                    if (sequence == 0) timestamp = TillNextTimestamp(lastTimestamp);
                }
                else
                {
                    sequence = 0;
                }

                lastTimestamp = timestamp;

                return GenerateId(timestamp);
            }
            finally
            {
                Semaphore.Release();
            }
        }

        private long NextSequence()
        {
            sequence++;

            return sequence & SequenceMask;
        }

        private long TillNextTimestamp(long lastTimestamp)
        {
            var timestamp = GetTimestamp();

            if (timestamp > lastTimestamp) return timestamp;

            while (timestamp <= lastTimestamp) timestamp = GetTimestamp();

            sequence = 0;

            return timestamp;
        }
    }
}