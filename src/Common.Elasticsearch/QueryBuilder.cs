using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MandateThat;
using Nest;
using StatementIQ.Common.ElasticSearch.Interfaces;
using StatementIQ.Common.ElasticSearch.Terms;
using StatementIQ.Common.ElasticSearch.Tools;

namespace StatementIQ.Common.ElasticSearch
{
    public class QueryBuilder : IQueryBuilder
    {
        private readonly ISuffixHelper _suffixHelper;

        public QueryBuilder(ISuffixHelper suffixHelper)
        {
            Mandate.That(suffixHelper, nameof(suffixHelper)).IsNotNull();

            _suffixHelper = suffixHelper;
        }

        public QueryBase CreateSearchQuery(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return null;
            }

            if (Regex.IsMatch(searchText, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                QueryBase result = new MatchPhrasePrefixQuery
                {
                    Query = $"{searchText}",
                    Field = "email"
                };

                return result;
            }
            else
            {
                searchText = Regex.Replace(searchText, "[-+@/;.,\t\r]|[\n]", m => $@"\{m.Value}");

                QueryBase result = new QueryStringQuery
                {
                    Query = $"*{searchText}*",
                    DefaultOperator = Operator.And
                };

                return result;
            }
        }

        public QueryBase CreateSearchQuery(string searchText, List<string> visibleFields)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return null;
            }

            searchText = Regex.Replace(searchText, "[-+@/;,\t\r ]|[\n]", m => $@" {m.Value}");

            var query = new MultiMatchQuery
            {
                Query = $"*{searchText}*",
                Operator = Operator.And,
                Type = TextQueryType.CrossFields
            };

            foreach (var field in visibleFields.Select((value, index) => new { value, index }))
            {
                query.Fields = field.index == 0 ? new Field(field.value) : query.Fields.And(field.value);
            }
            QueryBase result = query;

            return result;
        }

        public QueryBase CreateFilterQuery(string terms, Type dataType)
        {
            if (string.IsNullOrWhiteSpace(terms))
            {
                return null;
            }

            var query = AddArrayTerms(ref terms, dataType);

            var termQueryFactories = new List<ITermQueryFactory>
            {
                new GteTermQueryFactory(), new LteTermQueryFactory(), new LtTermQuery(), new GtTermQueryFactory(),
                new EqualTermQuery(_suffixHelper)
            };

            return GetAllTermQueries(terms, dataType, termQueryFactories, query);
        }

        public List<ISort> CreateSortField(string sortColumn, int? sortOrder, Type dataType)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                return null;
            }

            return new List<ISort>
            {
                new FieldSort
                {
                    Field = new Field($"{sortColumn.ToCamelCase()}{_suffixHelper.FieldSuffix(sortColumn, dataType)}"),
                    Order = sortOrder.HasValue && sortOrder.Value == -1 ? SortOrder.Descending : SortOrder.Ascending
                }
            };
        }

        private QueryBase GetAllTermQueries(string terms, Type dataType, List<ITermQueryFactory> termQueryFactories,
            QueryBase query)
        {
            foreach (var termQueryFactory in termQueryFactories)
            {
                foreach (var term in terms.Split(','))
                {
                    var @params = term.Split(termQueryFactory.Separator);

                    if (@params.Length == 2)
                    {
                        query = query && termQueryFactory.GetQuery(
                            @params[0].ToCamelCase(), @params[1], dataType);
                        terms = terms.Replace(term, string.Empty);
                    }
                }
            }

            return query;
        }

        private QueryBase AddArrayTerms(ref string terms, Type dataType)
        {
            QueryBase query = null;
            while (!string.IsNullOrWhiteSpace(terms))
            {
                var startPosition = 0;
                var startArray = startPosition + 1;
                startArray = terms.IndexOf('[', startArray);

                if (startArray == -1)
                {
                    break;
                }

                var endArray = terms.IndexOf(']', startArray + 1);
                var propertyNameStart = terms.Substring(0, startArray - 1).LastIndexOf(',') + 1;
                var propertyName = terms.Substring(propertyNameStart, startArray - propertyNameStart - 1);
                var arrayValues = terms.Substring(startArray + 1, endArray - startArray - 1);
                QueryBase queryArray = null;
                foreach (var term in arrayValues.Split(','))
                {
                    queryArray = queryArray ||
                                 new EqualTermQuery(_suffixHelper).GetQuery(propertyName.ToCamelCase(), term, dataType);
                }

                query = query && queryArray;

                if (endArray == terms.Length - 1)
                {
                    propertyNameStart--;
                }
                else
                {
                    endArray++;
                }

                var deleteArrayTermLength = endArray - propertyNameStart + 1;

                if (propertyNameStart < 0)
                {
                    terms = string.Empty;
                    break;
                }

                terms = terms.Remove(propertyNameStart, deleteArrayTermLength);
            }

            return query;
        }
    }
}