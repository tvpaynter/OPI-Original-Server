using System;
using System.Runtime.Serialization;

namespace StatementIQ.Common.Data.Exceptions
{
    [Serializable]
    public class ConnectionStringException : Exception
    {
        public ConnectionStringException()
        {
        }

        public ConnectionStringException(string message)
            : base(message)
        {
        }

        public ConnectionStringException(string message, Exception ex)
            : base(message, ex)
        {
        }

        protected ConnectionStringException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }
    }
}