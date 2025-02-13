using MandateThat;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StatementIQ.Common.Web.Logging.LogHelper;

namespace StatementIQ.Common.Web.Logging.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void UseCorrelationIdLogger(this IServiceCollection services, bool addSerilog = false)
        {
            Mandate.That(services, nameof(services)).IsNotNull();

            if (addSerilog)
            {
                services.AddSingleton(typeof(ILogger<>), typeof(SerilogLogger<>));
            }
            else
            {
                services.AddScoped(typeof(ILogger<>), typeof(CorrelationIdLogger<>));
            }
            services.AddScoped<ITraceIdentifierGetter, TraceIdentifierGetter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}