using System;
using System.Runtime.Serialization;

namespace StatementIQ.Common.Web.Exceptions
{
    [Serializable]
    public class BadClaimException : ArgumentException
    {
        public BadClaimException()
        {
        }

        public BadClaimException(string message)
            : base(message)
        {
        }

        protected BadClaimException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}