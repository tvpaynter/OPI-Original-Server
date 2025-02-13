using Nest;

namespace StatementIQ.Common.ElasticSearch.Interfaces
{
    public interface IElasticClientFactory
    {
        IElasticClient Create();
    }
}