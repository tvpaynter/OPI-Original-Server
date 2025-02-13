using MandateThat;
using Nest;
using StatementIQ.Common.ElasticSearch.Exceptions;
using StatementIQ.Common.ElasticSearch.Interfaces;

namespace StatementIQ.Common.ElasticSearch
{
    public abstract class ElasticSearchRepositoryBase
    {
        private readonly IElasticClientFactory _elasticClientFactory;

        protected readonly IQueryBuilder QueryBuilder = new QueryBuilder(new SuffixHelper());

        protected ElasticSearchRepositoryBase(IElasticClientFactory elasticClientFactory)
        {
            Mandate.That(elasticClientFactory).IsNotNull();

            _elasticClientFactory = elasticClientFactory;
        }

        protected IElasticClient GetElasticClient()
        {
            var elasticClient = _elasticClientFactory.Create();

            if (elasticClient == null)
            {
                throw new ElasticSearchException("ElasticSearch client is null");
            }

            CheckPing(elasticClient);

            return elasticClient;
        }

        protected static void ThrowIfException(IResponse response)
        {
            if (response?.OriginalException != null)
            {
                throw new ElasticSearchException(response.OriginalException.Message);
            }
        }

        private void CheckPing(IElasticClient elasticClient)
        {
            var pingResponse = elasticClient.Ping();

            if (!pingResponse.IsValid)
            {
                throw new ElasticSearchException(pingResponse.DebugInformation);
            }
        }
    }
}