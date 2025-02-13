using MandateThat;
using Microsoft.AspNetCore.Http;
using StatementIQ.Common.Autofac;

namespace StatementIQ.Common.Web.Managers
{
    [InstancePerLifetimeScope]
    public class TraceIdentifierGetter : ITraceIdentifierProvider
    {
        private readonly IHttpContextAccessor _context;

        public TraceIdentifierGetter(IHttpContextAccessor context)
        {
            Mandate.That(context, nameof(context)).IsNotNull();

            _context = context;
        }

        public string GetRequestIdentifier()
        {
            return _context.HttpContext?.TraceIdentifier;
        }
    }
}