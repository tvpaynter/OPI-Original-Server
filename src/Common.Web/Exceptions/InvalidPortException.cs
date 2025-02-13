using System;
using System.Runtime.Serialization;

namespace StatementIQ.Common.Web.Exceptions
{
    [Serializable]
    public class InvalidPortException : ArgumentException
    {
        public InvalidPortException()
        {
        }

        public InvalidPortException(string message)
            : base(message)
        {
        }

        protected InvalidPortException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}