using System;

namespace StatementIQ.Common.Web.Authorization.Exceptions
{
    public class DeserializerException : Exception
    {
        public DeserializerException(string message) : base(message)
        {
        }
    }
}