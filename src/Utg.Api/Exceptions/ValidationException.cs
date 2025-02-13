using System;

namespace Utg.Api.Exceptions
{
    public class ValidationException : Exception
    {
        public int ResponseStatusCode { get; set; }
        public ValidationException()
        {
        }
        public ValidationException(string message,int httpStatusCode) : base(message)
        {
            ResponseStatusCode = httpStatusCode;
        }
        public ValidationException(string message) : base(message)
        {

        }
    }
}
