using System;

namespace StatementIQ.Common.ElasticSearch.Exceptions
{
    public class ElasticSearchException : Exception
    {
        public ElasticSearchException(string message): base(message)
        {
            
        }
    }
}