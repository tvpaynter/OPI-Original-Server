using System;

namespace StatementIQ.Common.ElasticSearch.Exceptions
{
    public class ElasticSearchTermException : Exception
    {
        public ElasticSearchTermException(string message) : base(message)
        {
        }
    }
}