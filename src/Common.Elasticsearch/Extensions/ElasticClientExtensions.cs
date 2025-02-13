using System.Threading;
using System.Threading.Tasks;
using MandateThat;
using Nest;
using StatementIQ.Common.ElasticSearch.Exceptions;

namespace StatementIQ.Common.ElasticSearch.Extensions
{
    public static class ElasticClientExtensions
    {
        public static async Task CreateIndexAsync(this IElasticClient client, string indexName,
            CancellationToken cancellationToken)
        {
            Mandate.That(client, nameof(client)).IsNotNull();
            Mandate.That(indexName, nameof(indexName)).IsNotNullOrEmpty();

            var createIndexRequest = new CreateIndexRequest(indexName);

            var createIndexResponse = await client.Indices.CreateAsync(createIndexRequest, cancellationToken)
                .ConfigureAwait(false);

            if (!createIndexResponse.IsValid)
            {
                throw new ElasticSearchException(
                    $"Create alias '{indexName}' error. {createIndexResponse.DebugInformation}");
            }
        }

        public static async Task<bool> IndexExistsAsync(this IElasticClient client, string indexName,
            CancellationToken cancellationToken)
        {
            Mandate.That(client, nameof(client)).IsNotNull();
            Mandate.That(indexName, nameof(indexName)).IsNotNullOrEmpty();

            var existsRequest = new IndexExistsRequest(indexName);

            var existsResponse =
                await client.Indices.ExistsAsync(existsRequest, cancellationToken).ConfigureAwait(false);

            return existsResponse.Exists;
        }

        public static async Task DeleteIndexAsync(this IElasticClient client, string indexName,
            CancellationToken cancellationToken)
        {
            Mandate.That(client, nameof(client)).IsNotNull();
            Mandate.That(indexName, nameof(indexName)).IsNotNullOrEmpty();

            var deleteRequest = new DeleteIndexRequest(indexName);

            var deleteResponse =
                await client.Indices.DeleteAsync(deleteRequest, cancellationToken).ConfigureAwait(false);

            if (!deleteResponse.IsValid)
            {
                throw new ElasticSearchException($"Exist index '{indexName}' error. {deleteResponse.DebugInformation}");
            }
        }

        public static async Task CreateAliasAsync(this IElasticClient client, string indexName, string aliasName,
            CancellationToken cancellationToken)
        {
            Mandate.That(client, nameof(client)).IsNotNull();
            Mandate.That(indexName, nameof(indexName)).IsNotNullOrEmpty();
            Mandate.That(aliasName, nameof(aliasName)).IsNotNullOrEmpty();

            var putAliasRequest = new PutAliasRequest(indexName, aliasName);

            var putResponse = await client.Indices.PutAliasAsync(putAliasRequest, cancellationToken)
                .ConfigureAwait(false);

            if (!putResponse.IsValid)
            {
                throw new ElasticSearchException(
                    $"Create aliasName '{indexName}' for index '{aliasName}' error. {putResponse.DebugInformation}");
            }
        }

        public static async Task DeleteAliasAsync(this IElasticClient client, string indexName, string aliasName,
            CancellationToken cancellationToken)
        {
            Mandate.That(client, nameof(client)).IsNotNull();
            Mandate.That(indexName, nameof(indexName)).IsNotNullOrEmpty();
            Mandate.That(aliasName, nameof(aliasName)).IsNotNullOrEmpty();

            var deleteAliasRequest = new DeleteAliasRequest(indexName, aliasName);

            var deleteAliasResponse = await client.Indices.DeleteAliasAsync(deleteAliasRequest, cancellationToken)
                .ConfigureAwait(false);

            if (!deleteAliasResponse.IsValid)
            {
                throw new ElasticSearchException(
                    $"Create aliasName '{indexName}' for index '{aliasName}' error. {deleteAliasResponse.DebugInformation}");
            }
        }
    }
}