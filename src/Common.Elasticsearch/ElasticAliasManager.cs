using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MandateThat;
using Nest;
using StatementIQ.Common.ElasticSearch.Extensions;
using StatementIQ.Common.ElasticSearch.Interfaces;

namespace StatementIQ.Common.ElasticSearch
{
    public class ElasticAliasManager : ElasticSearchRepositoryBase, IElasticAliasManager
    {
        public ElasticAliasManager(IElasticClientFactory elasticClientFactory) : base(elasticClientFactory)
        {
        }

        public async Task AddAsync(List<string> aliases, CancellationToken cancellationToken)
        {
            Mandate.That(aliases, nameof(aliases)).IsNotNull();

            var client = GetElasticClient();

            foreach (var newAlias in aliases)
            {
                var indexName = (await client.GetIndicesPointingToAliasAsync(newAlias).ConfigureAwait(false))
                    ?.FirstOrDefault();

                if (string.IsNullOrEmpty(indexName))
                {
                    Mandate.That(newAlias, nameof(newAlias)).IsNotNull();

                    var newIndexName = $"{newAlias}{DateTime.UtcNow.GetTimeStamp()}";

                    await client.CreateIndexAsync(newIndexName, cancellationToken).ConfigureAwait(false);

                    await client.CreateAliasAsync(newIndexName, newAlias, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public async Task SwitchAliasToNewIndex(string newIndexName, string aliasName,
            CancellationToken cancellationToken)
        {
            var client = GetElasticClient();

            var oldIndexNames = await client.GetIndicesPointingToAliasAsync(aliasName).ConfigureAwait(false);

            var oldIndexName = oldIndexNames.FirstOrDefault();

            if (!string.IsNullOrEmpty(oldIndexName))
            {
                await client.DeleteAliasAsync(oldIndexName, aliasName, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                var indexExists = await client.IndexExistsAsync(aliasName, cancellationToken).ConfigureAwait(false);

                if (indexExists)
                {
                    await client.DeleteIndexAsync(aliasName, cancellationToken).ConfigureAwait(false);
                }
            }

            var exists = await client.IndexExistsAsync(newIndexName, cancellationToken).ConfigureAwait(false);

            if (!exists)
            {
                await client.CreateIndexAsync(newIndexName, cancellationToken).ConfigureAwait(false);
            }

            await client.CreateAliasAsync(newIndexName, aliasName, cancellationToken).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(oldIndexName))
            {
                await client.DeleteIndexAsync(oldIndexName, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task<bool> CheckIfAliasExist(string aliasName, CancellationToken cancellationToken)
        {
            var client = GetElasticClient();

            var indices = await client.GetIndicesPointingToAliasAsync(aliasName).ConfigureAwait(false);

            if (indices.Any())
            {
                return true;
            }

            return await client.IndexExistsAsync(aliasName, cancellationToken).ConfigureAwait(false);
        }
    }
}