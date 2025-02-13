using System;

namespace StatementIQ.Common.ElasticSearch.Exceptions
{
    public class ElasticSearchArgumentNullException : ArgumentNullException
    {
        public ElasticSearchArgumentNullException(string message): base(message)
        {
            
        }
    }
}