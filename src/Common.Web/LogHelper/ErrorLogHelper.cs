using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using StatementIQ.Common.Web.Extensions;
using StatementIQ.Common.Web.Models;
using StatementIQ.Common.Web.Security.Extensions;

namespace StatementIQ.Common.Web.LogHelper
{
    internal static class ErrorLogHelper
    {
        public static void LogErrorDetails(this ILogger logger, HttpContext context, Exception exception,
            string serviceName,IConfiguration config, LogLevel logLevel)
        {
            try
            {
                var contextInfo = GetContextInfo(context, exception, serviceName, config);
                logger.Log(logLevel, $"{contextInfo}");
            }
            catch (Exception)
            {
                //supress exception
            }
          
        }

        private static string GetContextInfo(HttpContext context,Exception exception,string serviceName,IConfiguration config)
        {
            if (context?.Request == null)
            {
                return string.Empty;
            }
            var exEmmiter = new ExceptionEmitter
            {
                DateTime = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss"),
                ErrorCode = Convert.ToString(context.Response.StatusCode),
                ErrorMessage = exception?.Message,
                ErrorDescription = exception?.StackTrace,
                TraceId = context?.TraceIdentifier,
                ServiceName = serviceName,
                Scheme = context.Request.Scheme,
                Host = Convert.ToString(context.Request.Host),
                Path = context.Request.Path,
                RequestMethod = context.Request.Method,
                QueryString = Convert.ToString(context.Request.QueryString),
                Client = config["Client"],
                Release = config["Release"],
                Environment = config["environment"]
            };

            return JsonSerializer.Serialize(exEmmiter);
        }
    }
}