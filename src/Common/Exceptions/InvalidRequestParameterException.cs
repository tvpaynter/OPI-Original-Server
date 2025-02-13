using System;
using System.Runtime.Serialization;

namespace StatementIQ.Exceptions
{
    [Serializable]
    public class InvalidRequestParameterException : Exception
    {
        public InvalidRequestParameterException(string parameterName = null)
            : this(null, parameterName)
        {
        }

        public InvalidRequestParameterException(string message, string parameterName = null)
            : this(message, null, parameterName)
        {
        }

        public InvalidRequestParameterException(string message, Exception ex, string parameterName = null)
            : base(message, ex)
        {
            ParameterName = parameterName;
        }

        protected InvalidRequestParameterException(SerializationInfo serializationInfo,
            StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public string ParameterName { get; set; }
    }
}