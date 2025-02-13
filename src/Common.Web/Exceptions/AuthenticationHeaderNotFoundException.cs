using System;
using System.Runtime.Serialization;

namespace StatementIQ.Common.Web.Exceptions
{
    [Serializable]
    public class AuthenticationHeaderNotFoundException : ArgumentException
    {
        public AuthenticationHeaderNotFoundException()
        {
        }

        public AuthenticationHeaderNotFoundException(string message)
            : base(message)
        {
        }

        protected AuthenticationHeaderNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}