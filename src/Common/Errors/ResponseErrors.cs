using System.Collections.Generic;
using MandateThat;

namespace StatementIQ.Errors
{
    public class ResponseErrors : List<ResponseError>
    {
        public bool IsEmpty => Count == 0;

        public void Add(string code, string message)
        {
            Mandate.That(code, nameof(code)).IsNotNullOrWhiteSpace();
            Mandate.That(message, nameof(message)).IsNotNullOrWhiteSpace();

            Add(new ResponseError(code, message));
        }
    }
}