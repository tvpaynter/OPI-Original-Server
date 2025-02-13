using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StatementIQ.Common.ElasticSearch.Interfaces
{
    public interface IElasticSearchRepository
    {
        Task UpdateAsync(string collectionName, dynamic document, string id,
            CancellationToken cancellationToken = default);

        Task InsertAsync(string collectionName, object document, string id,
            CancellationToken cancellationToken = default);

        Task<object> GetByIdAsync(string collectionName, string id,
            CancellationToken cancellationToken = default);

        Task RemoveAsync(string collectionName, string id, CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(string collectionName, string searchText = "", string term = "",
            CancellationToken cancellationToken = default);

        Task<IEnumerable<object>> SearchAsync(string collectionName, int skip, int take, string sortColumn = "",
            int? sortOrder = null, string searchText = "", string term = "", string fields = "",
            CancellationToken cancellationToken = default);
    }
}