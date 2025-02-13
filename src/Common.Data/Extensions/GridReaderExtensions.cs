using System.Threading.Tasks;
using Dapper;
using MandateThat;

namespace StatementIQ.Common.Data.Extensions
{
    public static class GridReaderExtensions
    {
        public static async Task<Pagination<T>> PaginationMapper<T>(this SqlMapper.GridReader reader)
        {
            Mandate.That(reader, nameof(reader)).IsNotNull();

            return new Pagination<T>
            {
                Count = await reader.ReadFirstAsync<long>().ConfigureAwait(false),
                Results = await reader.ReadAsync<T>().ConfigureAwait(false)
            };
        }
    }
}