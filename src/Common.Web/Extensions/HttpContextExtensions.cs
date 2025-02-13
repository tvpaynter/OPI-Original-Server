using MandateThat;
using Microsoft.AspNetCore.Http;

namespace StatementIQ.Common.Web.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetRequestInfo(this HttpContext context)
        {
            Mandate.That(context, nameof(context)).IsNotNull();

            return $"CorrelationId: {context.TraceIdentifier}. Request-'{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}";
        }
    }
}