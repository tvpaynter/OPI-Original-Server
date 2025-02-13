using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using MandateThat;
using Microsoft.Extensions.Configuration;
using Nest;
using StatementIQ.Common.Data;
using StatementIQ.Common.ElasticSearch.Exceptions;
using StatementIQ.Common.ElasticSearch.Interfaces;
using StatementIQ.Extensions;

namespace StatementIQ.Common.ElasticSearch
{
    public abstract class ElasticSearchRepositoryGeneric<T> : ElasticSearchRepositoryBase, IElasticSearchRepository<T>
        where T : class, new()
    {
        private readonly IConfiguration _config;
        protected ElasticSearchRepositoryGeneric(IElasticClientFactory elasticClientFactory, IConfiguration config) : base(
            elasticClientFactory)
        {
            _config = config;
        }

        public abstract string IndexName { get; }

        public async Task UpdateAsync(T document, string id, CancellationToken cancellationToken = default)
        {
            Mandate.That(IndexName).IsNotNullOrWhiteSpace();
            Mandate.That(id).IsNotNullOrWhiteSpace();

            if (document == null)
            {
                throw new ElasticSearchArgumentNullException("Document is null");
            }

            var elasticClient = GetElasticClient();

            var request = new UpdateRequest<dynamic, dynamic>(IndexName, id)
            {
                Doc = document,
                Refresh = Refresh.WaitFor
            };

            var response = await elasticClient.UpdateAsync(request, cancellationToken)
                .ConfigureAwait(false);

            ThrowIfException(response);
        }

        public async Task PatchAsync(NameValueObject<T> document, string id, CancellationToken cancellationToken = default)
        {
            Mandate.That(id).IsNotNullOrWhiteSpace();

            if (document == null)
            {
                throw new ElasticSearchArgumentNullException("Document is null");
            }

            var elasticClient = GetElasticClient();

            var getResponse = await elasticClient
                .GetAsync<object>(id, selector => selector.Index(IndexName), cancellationToken)
                .ConfigureAwait(false);

            var objResponse = getResponse.Source as IDictionary<string, dynamic>;

            ApplyChangesToObject(document, objResponse);

            var request = new UpdateRequest<dynamic, dynamic>(IndexName, id)
            {
                Doc = objResponse,
                Refresh = Refresh.WaitFor
            };

            var response = await elasticClient.UpdateAsync(request, cancellationToken)
                .ConfigureAwait(false);

            ThrowIfException(response);
        }

        public async Task InsertAsync(T document, string id,
            CancellationToken cancellationToken = default)
        {
            Mandate.That(IndexName).IsNotNullOrWhiteSpace();
            Mandate.That(id).IsNotNullOrWhiteSpace();

            if (document == null)
            {
                throw new ElasticSearchArgumentNullException("Document is null");
            }

            var request = new IndexRequest<object>(document, IndexName, id) { Refresh = Refresh.WaitFor };

            var elasticClient = GetElasticClient();
            var response = await elasticClient.IndexAsync(request, cancellationToken)
                .ConfigureAwait(false);

            ThrowIfException(response);
        }

        public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            Mandate.That(IndexName).IsNotNullOrWhiteSpace();
            Mandate.That(id).IsNotNullOrWhiteSpace();

            var elasticClient = GetElasticClient();

            var response = await elasticClient
                .GetAsync<T>(id, selector => selector.Index(IndexName), cancellationToken)
                .ConfigureAwait(false);

            ThrowIfException(response);

            return response.Source;
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
        {
            Mandate.That(IndexName).IsNotNullOrWhiteSpace();
            Mandate.That(id).IsNotNullOrWhiteSpace();

            var deleteRequest = new DeleteRequest(IndexName, id)
            {
                Refresh = Refresh.WaitFor
            };

            var elasticClient = GetElasticClient();
            var response = await elasticClient
                .DeleteAsync(deleteRequest, cancellationToken)
                .ConfigureAwait(false);
            ThrowIfException(response);
        }

        public async Task<long> GetCountAsync(string searchText = "", string term = "",
            CancellationToken cancellationToken = default)
        {
            var searchQuery = QueryBuilder.CreateSearchQuery(searchText);
            var filterQuery = QueryBuilder.CreateFilterQuery(term, typeof(T));
            var query = searchQuery && filterQuery;

            var request = new CountRequest<object>(IndexName)
            {
                Query = query
            };

            var elasticClient = GetElasticClient();
            var response = await elasticClient.CountAsync<object>(x => request, cancellationToken)
                .ConfigureAwait(false);

            ThrowIfException(response);
            return response.Count;
        }

        public async Task<IEnumerable<T>> SearchAsync(int skip, int take,
            string sortColumn = "",
            int? sortOrder = null, string searchText = "", string term = "",
            CancellationToken cancellationToken = default)
        {
            Mandate.That(IndexName).IsNotNullOrWhiteSpace();
            Mandate.That(take, nameof(take)).IsGreaterThan(default);
            Mandate.That(skip, nameof(skip)).IsGreaterThanOrEqualTo(default);

            var searchQuery = QueryBuilder.CreateSearchQuery(searchText);
            var sort = QueryBuilder.CreateSortField(sortColumn, sortOrder, typeof(T));
            var filterQuery = QueryBuilder.CreateFilterQuery(term, typeof(T));
            var query = searchQuery && filterQuery;

            var request = new SearchRequest<T>(IndexName)
            {
                Sort = sort,
                Query = query,
                From = skip,
                Size = take
            };

            var elasticClient = GetElasticClient();
            var response = await elasticClient.SearchAsync<T>(request, cancellationToken).ConfigureAwait(false);
            ThrowIfException(response);

            return response.Documents;
        }

        public async Task<long> GetTxnCountAsync(string searchText = "", string term = "",
            CancellationToken cancellationToken = default)
        {
            var visibleTxnFields = _config["VisibleTxnFields"]?.Split(",")?.ToList<string>();
            var amount = GetAmountInSearchText(searchText);
            if (amount > 0)
            {
                searchText = searchText.Replace(amount.ToString(), "").Replace("$", "");
            }
            var date = GetDateIfExists(searchText);
            if (!string.IsNullOrEmpty(date))
            {
                searchText = searchText.Replace(date, "");
            }
            var searchQuery = QueryBuilder.CreateSearchQuery(searchText, visibleTxnFields);
            var filterQuery = QueryBuilder.CreateFilterQuery(term, typeof(T));
            var query = searchQuery && filterQuery;

            var request = new CountRequest<object>(IndexName)
            {
                Query = query,

            };

            var elasticClient = GetElasticClient();
            var response = await elasticClient.CountAsync<object>(x => request, cancellationToken)
                .ConfigureAwait(false);

            ThrowIfException(response);
            return response.Count;
        }

        public async Task<IEnumerable<T>> SearchTxnAsync(int skip, int take,
            string sortColumn = "",
            int? sortOrder = null, string searchText = "", string term = "",
            CancellationToken cancellationToken = default)
        {
            Mandate.That(IndexName).IsNotNullOrWhiteSpace();
            Mandate.That(take, nameof(take)).IsGreaterThan(default);
            Mandate.That(skip, nameof(skip)).IsGreaterThanOrEqualTo(default);

            var amount = GetAmountInSearchText(searchText);
            if (amount > 0)
            {
                searchText = searchText.Replace(amount.ToString(), "").Replace("$", "");
            }
            var enteredDate = GetDateIfExists(searchText);
            if (!string.IsNullOrEmpty(enteredDate))
            {
                searchText = searchText.Replace(enteredDate, "");
            }
            var visibleTxnFields = _config["VisibleTxnFields"]?.Split(",")?.ToList<string>();
            var searchQuery = QueryBuilder.CreateSearchQuery(searchText, visibleTxnFields);
            var sort = QueryBuilder.CreateSortField(sortColumn, sortOrder, typeof(T));
            var filterQuery = QueryBuilder.CreateFilterQuery(term, typeof(T));

            var query = searchQuery && filterQuery;

            QueryContainer queryContainer = null;
            var request = new SearchRequest<T>(IndexName)
            {
                Sort = sort,
                Query = query,
                From = skip,
                Size = take
            };
            if (amount > 0)
            {
                var amountField = Convert.ToString(_config["VisibleTxnNumericFields:amount"]);
                queryContainer = new TermQuery { Field = amountField, Value = amount };
            }
            if (!string.IsNullOrEmpty(enteredDate))
            {
                var dateField = Convert.ToString(_config["VisibleTxnNumericFields:CreatedAt"]);
                var dateQry = new TermQuery { Field = dateField, Value = enteredDate };
                queryContainer = queryContainer != null ? queryContainer && dateQry : dateQry;
            }
            if (queryContainer != null)
            {
                request.PostFilter = queryContainer;
            }
            var elasticClient = GetElasticClient();
            var response = await elasticClient.SearchAsync<T>(request, cancellationToken).ConfigureAwait(false);
            ThrowIfException(response);

            return response.Documents;
        }
        private string GetDateIfExists(string searchText)
        {
            Match mat = null;
            try
            {
                if (string.IsNullOrEmpty(searchText))
                    return string.Empty;
                Regex rgxDateTime = new(@"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}");
                Regex rgxDate = new(@"\d{4}-\d{2}-\d{2}");
                mat = rgxDateTime.Match(searchText);
                if (string.IsNullOrEmpty(mat.ToString()))
                {
                    mat = rgxDate.Match(searchText);
                }
            }
            catch (Exception)
            {
            }
           
            return mat.ToString();
        }
        private double GetAmountInSearchText(string searchText)
        {
            Double amount = 0;
            try
            {
                if (string.IsNullOrEmpty(searchText))
                    return amount;
                IEnumerable<string> a1 = searchText.Split(" ".ToCharArray())
                                 .ToList()
                                 .Where(X => Regex.Match(X, "(\\$[0-9]*)").Success);
                amount = Convert.ToDouble(a1.First().Replace("$", ""));
            }
            catch (Exception)
            {

            }
            return amount;
        }

        private static void ApplyChangesToObject(NameValueObject<T> document, IDictionary<string, dynamic> objResponse)
        {
            Mandate.That(objResponse).IsNotNull();

            var fields = document.GetFields();

            foreach (var pair in fields)
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
                        if (!((IDictionary<string, dynamic>)currentObj).Keys.Contains(property))
                        {
                            currentObj[property] = new ExpandoObject();
                        }

                        currentObj = currentObj[property];
                    }
                }

                ((IDictionary<string, dynamic>)currentObj)[parts[^1].ToCamelCase()] = pair.Value;
            }
        }
    }
}