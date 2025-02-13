using MandateThat;
using Microsoft.AspNetCore.Builder;

namespace StatementIQ.Common.Web.Middleware.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseRequestResponseLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }

        public static void CommonExceptionMiddleware(this IApplicationBuilder app, string serviceName)
        {
            app.UseMiddleware<ExceptionMiddleware>(serviceName);
        }

        public static void UseCorrelationIdMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorrelationIdMiddleware>();
        }

        public static void UseSecurityHeaderMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityHeaderMiddleware>();
        }

        public static void CommonErrorHandlerMiddleware(this IApplicationBuilder app, string serviceName)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>(serviceName);
        }
    }
}