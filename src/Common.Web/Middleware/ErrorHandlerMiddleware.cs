using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StatementIQ.Common.Web.Exceptions;
using StatementIQ.Common.Web.LogHelper;
using StatementIQ.Common.Web.Models;
using StatementIQ.Common.Web.Constant;

namespace StatementIQ.Common.Web.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly string _serviceName;
        public static IConfiguration Configuration { get; set; }

        public ErrorHandlerMiddleware(RequestDelegate next, string serviceName, ILogger<ErrorHandlerMiddleware> logger, IConfiguration configuration)
        {
            _serviceName = serviceName;
            Next = next;
            Logger = logger;
            Configuration = configuration;
        }

        private RequestDelegate Next { get; }
        private ILogger<ErrorHandlerMiddleware> Logger { get; }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();

            try
            {
                await Next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            context.Response.ContentType = "application/json";
            string exceptionType;
            LogLevel logLevel = LogLevel.Error;
            var errorMsg = string.Empty;
            switch (exception?.GetType()?.Name)
            {
                case "BinNotFoundException":
                case "BinIncompleteException":
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exceptionType = ExceptionType.ValidationError.ToString();
                    logLevel = LogLevel.Warning;
                    break;
                case "UtgException":
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exceptionType = ExceptionType.BusinessError.ToString();
                    break;
                case "PostgresException":
                    errorMsg = ApplicationErrorMessages.GenericException;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exceptionType = ExceptionType.DBError.ToString();
                    break;
                default:
                    // unhandled error
                    errorMsg = ApplicationErrorMessages.GenericException;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exceptionType = ExceptionType.GenericError.ToString();
                    break;
            }
            errorMsg = !string.IsNullOrEmpty(errorMsg) ? errorMsg : exception?.Message;
            Logger.LogErrorDetails(context, exception, _serviceName,Configuration, logLevel);
            var result = JsonSerializer.Serialize(new
            {
                DateTime = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss"),
                ErrorCode = Convert.ToString(context.Response.StatusCode),
                ErrorMessage = errorMsg,
                ErrorType = exceptionType,
                TraceId = context?.TraceIdentifier,
                ServiceName = _serviceName
            }); 
            await context.Response.WriteAsync(result);
        }
    }
}