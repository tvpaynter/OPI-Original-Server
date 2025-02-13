using System;
using System.Threading.Tasks;
using MandateThat;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace StatementIQ.Common.Web.Middleware
{
    public class CorrelationIdMiddleware
    {
        public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
        {
            Mandate.That(next, nameof(next)).IsNotNull();
            Mandate.That(logger, nameof(logger)).IsNotNull();

            Next = next;
            Logger = logger;
        }

        private RequestDelegate Next { get; }
        private ILogger<CorrelationIdMiddleware> Logger { get; }

        public Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Correlation-ID", out var correlationId))
            {
                context.TraceIdentifier = correlationId;
            }
            else
            {
                context.TraceIdentifier = Guid.NewGuid().ToString();
            }

            Logger.Log(LogLevel.Information,
                $"Path: {context.Request.Path}. CorrelationId: {context.TraceIdentifier}.");

            return Next(context);
        }
    }
}