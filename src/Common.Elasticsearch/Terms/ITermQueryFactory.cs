using System;
using Nest;

namespace StatementIQ.Common.ElasticSearch.Terms
{
    public interface ITermQueryFactory
    {
        string Separator { get; }
        QueryBase GetQuery(string field, object value, Type dataType);
    }
}