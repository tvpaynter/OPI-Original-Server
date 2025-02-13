using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using MandateThat;
using Nest;
using StatementIQ.Common.ElasticSearch.Exceptions;
using StatementIQ.Common.ElasticSearch.Interfaces;

namespace StatementIQ.Common.ElasticSearch
{
    public class ElasticSearchRepository<T> : ElasticSearchRepositoryBase, IElasticSearchRepository
        where T : class
    {
        public ElasticSearchRepository(IElasticClientFactory elasticClientFactory) : base(elasticClientFactory)
        {
        }

        public async Task UpdateAsync(string collectionName, dynamic document, string id,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(collectionName).IsNotNullOrWhiteSpace();
            Mandate.That(id).IsNotNullOrWhiteSpace();

            if (document == null)
            {
                throw new ElasticSearchArgumentNullException("Document is null");
            }

            var elasticClient = GetElasticClient();

            var getResponse = await elasticClient
                .GetAsync<object>(id, selector => selector.Index(collectionName), cancellationToken)
                .ConfigureAwait(false);

            var objResponse = (IDictionary<string, dynamic>) getResponse.Source;

            ApplyChangesToObject(document, objResponse);

            var request = new UpdateRequest<dynamic, dynamic>(collectionName, id)
            {
                Doc = objResponse,
                Refresh = Refresh.WaitFor
            };

            var response = await elasticClient.UpdateAsync(request, cancellationToken)
                .ConfigureAwait(false);

            ThrowIfException(response);
        }

        public async Task InsertAsync(string collectionName, object document, string id,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(collectionName).IsNotNullOrWhiteSpace();
            Mandate.That(id).IsNotNullOrWhiteSpace();

            if (document == null)
            {
                throw new ElasticSearchArgumentNullException("Document is null");
            }

            var request = new IndexRequest<object>(document, collectionName, id) {Refresh = Refresh.WaitFor};

            var elasticClient = GetElasticClient();
            var response = await elasticClient.IndexAsync(request, cancellationToken)
                .ConfigureAwait(false);

            ThrowIfException(response);
        }

        public async Task<object> GetByIdAsync(string collectionName, string id,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(collectionName).IsNotNullOrWhiteSpace();
            Mandate.That(id).IsNotNullOrWhiteSpace();

            var elasticClient = GetElasticClient();

            var response = await elasticClient
                .GetAsync<object>(id, selector => selector.Index(collectionName), cancellationToken)
                .ConfigureAwait(false);

            ThrowIfException(response);
            return response.Source;
        }

        public async Task RemoveAsync(string collectionName, string id, CancellationToken cancellationToken = default)
        {
            Mandate.That(collectionName).IsNotNullOrWhiteSpace();
            Mandate.That(id).IsNotNullOrWhiteSpace();

            var deleteRequest = new DeleteRequest(collectionName, id)
            {
                Refresh = Refresh.WaitFor
            };

            var elasticClient = GetElasticClient();
            var response = await elasticClient
                .DeleteAsync(deleteRequest, cancellationToken)
                .ConfigureAwait(false);
            ThrowIfException(response);
        }

        public async Task<long> GetCountAsync(string collectionName, string searchText = "", string term = "",
            CancellationToken cancellationToken = default)
        {
            var searchQuery = QueryBuilder.CreateSearchQuery(searchText);
            var filterQuery = QueryBuilder.CreateFilterQuery(term, typeof(T));
            var query = searchQuery && filterQuery;

            var request = new CountRequest<object>(collectionName)
            {
                Query = query
            };

            var elasticClient = GetElasticClient();
            var response = await elasticClient.CountAsync<object>(x => request, cancellationToken)
                .ConfigureAwait(false);

            ThrowIfException(response);
            return response.Count;
        }

        public async Task<IEnumerable<object>> SearchAsync(string collectionName, int skip, int take,
            string sortColumn = "",
            int? sortOrder = null, string searchText = "", string term = "", string fields = "",
            CancellationToken cancellationToken = default)
        {
            Mandate.That(collectionName).IsNotNullOrWhiteSpace();
            Mandate.That(take, nameof(take)).IsGreaterThan(default);
            Mandate.That(skip, nameof(skip)).IsGreaterThanOrEqualTo(default);

            var searchQuery = QueryBuilder.CreateSearchQuery(searchText);
            var sort = QueryBuilder.CreateSortField(sortColumn, sortOrder, typeof(T));
            var filterQuery = QueryBuilder.CreateFilterQuery(term, typeof(T));
            var query = searchQuery && filterQuery;

            var request = new SearchRequest<object>(collectionName)
            {
                Sort = sort,
                Query = query,
                From = skip,
                Size = take,
                Source = GetSource(fields)
            };

            var elasticClient = GetElasticClient();
            var response = await elasticClient.SearchAsync<object>(request, cancellationToken).ConfigureAwait(false);
            ThrowIfException(response);
            return response.Documents;
        }
        
        private static SourceFilter GetSource(string fields)
        {
            return new SourceFilter
                {Includes = string.IsNullOrWhiteSpace(fields) ? "*" : FieldsCovertToSourceQuery(fields)};
        }

        private static string FieldsCovertToSourceQuery(string fields)
        {
            Mandate.That(fields, nameof(fields)).IsNotNullOrWhiteSpace();
            var input = fields;

            while (true)
            {
                var lastOpenBracesPosition = input.LastIndexOf("{", StringComparison.Ordinal);
                if (lastOpenBracesPosition == -1)
                {
                    break;
                }

                var closeBracesPosition = input.IndexOf("}", lastOpenBracesPosition, StringComparison.Ordinal);
                var firstComaPositionBeforeOpenBraces =
                    input.LastIndexOf(",", lastOpenBracesPosition - 1, StringComparison.Ordinal) + 1;
                var firstOpenBracesPositionBeforeOpenBraces =
                    input.LastIndexOf("{", lastOpenBracesPosition - 1, StringComparison.Ordinal) + 1;
                if (firstOpenBracesPositionBeforeOpenBraces > firstComaPositionBeforeOpenBraces)
                {
                    firstComaPositionBeforeOpenBraces = firstOpenBracesPositionBeforeOpenBraces;
                }

                var segment = input.Substring(firstComaPositionBeforeOpenBraces,
                    closeBracesPosition - firstComaPositionBeforeOpenBraces + 1);

                var propertyName = input.Substring(firstComaPositionBeforeOpenBraces,
                    lastOpenBracesPosition - firstComaPositionBeforeOpenBraces).Trim(',').Trim();
                var insideBraces = input.Substring(lastOpenBracesPosition + 1,
                    closeBracesPosition - lastOpenBracesPosition - 1);

                var newSegment = string.Join(',',
                    insideBraces.Split(",").Select(split => $"{propertyName}.{split.Trim()}"));
                input = input.Replace(segment, newSegment);
            }

            return input;
        }

        private static void ApplyChangesToObject(dynamic document, IDictionary<string, dynamic> objResponse)
        {
            Mandate.That(objResponse).IsNotNull();

            foreach (var pair in (IDictionary<string, object>) document)
            {
                dynamic currentObj = objResponse;
                var key = pair.Key;
                var parts = key.Split('.');
                for (var i = 0; i < parts.Length - 1; i++)
                {
                    if (int.TryParse(parts[i], out var index))
                    {
                        currentObj = currentObj[index];
                    }
                    else
                    {
                        var property = parts[i];
                        if (!((IDictionary<string, dynamic>) currentObj).Keys.Contains(property))
                        {
                            currentObj[property] = new ExpandoObject();
                        }

                        currentObj = currentObj[property];
                    }
                }

                ((IDictionary<string, dynamic>) currentObj)[parts[^1]] = pair.Value;
            }
        }
    }
}