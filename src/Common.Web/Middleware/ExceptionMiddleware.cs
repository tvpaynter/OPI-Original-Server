using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StatementIQ.Common.Web.LogHelper;
using StatementIQ.Common.Web.Models;

namespace StatementIQ.Common.Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly string _serviceName;

        private static readonly IDictionary<HttpStatusCode, string> HttpErrorMessages =
            new Dictionary<HttpStatusCode, string>
            {
                {HttpStatusCode.InternalServerError, "Internal Server Error"},
                {HttpStatusCode.ServiceUnavailable, "Service Unavailable please try again later"},
                {HttpStatusCode.BadRequest, string.Empty}
            };

        public ExceptionMiddleware(RequestDelegate next, string serviceName, ILogger<ExceptionMiddleware> logger)
        {
            _serviceName = serviceName;
            Next = next;
            Logger = logger;

        }

        private RequestDelegate Next { get; }
        private ILogger<ExceptionMiddleware> Logger { get; }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();

            try
            {
                await Next(httpContext);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            await Logger.LogErrorAsync(context, exception).ConfigureAwait(false);

            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}
