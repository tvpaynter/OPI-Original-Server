using System.Threading;
using System.Threading.Tasks;

namespace StatementIQ
{
    public interface IUniqueIdGenerator
    {
        long WorkerId { get; }

        long[] BlockOfIds(int count);

        Task<long[]> BlockOfIdsAsync(int count, CancellationToken cancellationToken = default);

        long NextId();
        Task<long> NextIdAsync(CancellationToken cancellationToken = default);
    }
}