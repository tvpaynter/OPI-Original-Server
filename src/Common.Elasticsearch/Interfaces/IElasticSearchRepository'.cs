using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StatementIQ.Common.Data;

namespace StatementIQ.Common.ElasticSearch.Interfaces
{
    public interface IElasticSearchRepository<T> where T : class, new()
    {
        string IndexName { get; }

        Task UpdateAsync(T document, string id, CancellationToken cancellationToken = default);
        
        Task PatchAsync(NameValueObject<T> document, string id, CancellationToken cancellationToken = default);
        
        Task InsertAsync(T document, string id, CancellationToken cancellationToken = default);

        Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        Task RemoveAsync(string id, CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(string searchText = "", string term = "",
            CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> SearchAsync(int skip, int take, string sortColumn = "",
            int? sortOrder = null, string searchText = "", string term = "",
            CancellationToken cancellationToken = default);

        Task<long> GetTxnCountAsync(string searchText = "", string term = "",
          CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> SearchTxnAsync(int skip, int take, string sortColumn = "",
            int? sortOrder = null, string searchText = "", string term = "",
            CancellationToken cancellationToken = default);
    }
}