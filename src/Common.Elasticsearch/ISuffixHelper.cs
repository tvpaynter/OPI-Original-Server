using System;

namespace StatementIQ.Common.ElasticSearch
{
    public interface ISuffixHelper
    {
        string FieldSuffix(string propertyName, Type dataType);
    }
}