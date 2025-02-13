using Microsoft.AspNetCore.Http;

namespace StatementIQ.Common.Web.Logging.LogHelper
{
    public class TraceIdentifierGetter : ITraceIdentifierGetter
    {
        private IHttpContextAccessor ContextAccessor { get; }

        public TraceIdentifierGetter(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
        }

        public string Identifier => ContextAccessor.HttpContext?.TraceIdentifier;
    }
}