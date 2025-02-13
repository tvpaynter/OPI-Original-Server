using System;
using System.Collections.Generic;
using Nest;

namespace StatementIQ.Common.ElasticSearch.Interfaces
{
    public interface IQueryBuilder
    {
        QueryBase CreateSearchQuery(string searchText);
        QueryBase CreateSearchQuery(string searchText, List<string> visibleFields);
        QueryBase CreateFilterQuery(string terms, Type dataType);
        List<ISort> CreateSortField(string sortColumn, int? sortOrder, Type dataType);
    }
}