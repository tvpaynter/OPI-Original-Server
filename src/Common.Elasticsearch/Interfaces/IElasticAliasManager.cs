using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StatementIQ.Common.ElasticSearch.Interfaces
{
    public interface IElasticAliasManager
    {
        Task AddAsync(List<string> aliases, CancellationToken cancellationToken);

        Task SwitchAliasToNewIndex(string newIndexName, string aliasName, CancellationToken cancellationToken);
        
        Task<bool> CheckIfAliasExist(string aliasName, CancellationToken cancellationToken);
    }
}