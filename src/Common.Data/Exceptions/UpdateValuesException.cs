using System;

namespace StatementIQ.Common.Data.Exceptions
{
    public class UpdateValuesException : ArgumentException
    {
        public UpdateValuesException()
        {
        }

        public UpdateValuesException(string message) : base(message)
        {
        }
    }
}