using System;
using System.Runtime.Serialization;

namespace StatementIQ.Common.Data.Exceptions
{
    [Serializable]
    public class BaseRepositoryException : Exception
    {
        public BaseRepositoryException()
        {
        }

        public BaseRepositoryException(string message)
            : base(message)
        {
        }

        public BaseRepositoryException(string message, Exception ex)
            : base(message, ex)
        {
        }

        protected BaseRepositoryException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}