using System;

namespace StatementIQ.Common.Web.Authorization.Exceptions
{
    public class NoAuthTokenException : Exception
    {
        public NoAuthTokenException(string message) : base(message)
        {
        }
    }
}