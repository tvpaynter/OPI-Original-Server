using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using StatementIQ.Common.Web.Extensions;
using StatementIQ.Common.Web.Security.Extensions;

namespace StatementIQ.Common.Web.LogHelper
{
    internal static class LogHelper
    {
        public static async Task LogErrorAsync(this ILogger logger, HttpContext context, Exception exception)
        {
            var contextInfo = await GetContextInfo(context).ConfigureAwait(false);

            logger.Log(LogLevel.Error, $"Context info: {contextInfo}. Exception: {exception}");
        }

        private static async Task<string> GetContextInfo(HttpContext context)
        {
            if (context?.Request == null)
            {
                return string.Empty;
            }

            var result = new StringBuilder();

            result.AppendLine($"Authenticated: {context?.User?.Identity?.IsAuthenticated}");

            if (context.User?.Identity?.IsAuthenticated ?? false)
            {
                result.AppendLine(
                    $"UserId: {context.User.Identity.GetClaimValue<long>(Constants.AuthenticationClaimTypes.UserId)}");
                result.AppendLine(
                    $"HierarchyId: {context.User.Identity.GetClaimValue<long>(Constants.AuthenticationClaimTypes.HierarchyId)}");
                result.AppendLine($"CorrelationId: {context.TraceIdentifier}");
                result.AppendLine($"Request: {context.Request.Scheme} {context.Request.Host}{context.Request.Path}");
                result.AppendLine($"Request method: {context.Request.Method}'");
                result.AppendLine(
                    $"Bearer: {context.Request.Headers[HeaderNames.Authorization].ToString()?.Replace("Bearer ", string.Empty)}");

                result.AppendLine($"Headers: ");
                foreach (var key in context.Request.Headers.Keys)
                {
                    result.AppendLine($"{key}={context.Request.Headers[key]}");
                }
            }

            var body = context.Request.Body;

            result.AppendLine($"Request info: {context.GetRequestInfo()}");
            result.AppendLine($"QueryString: {context.Request.QueryString}");

            if (body != null)
            {
                var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];

                await body.ReadAsync(buffer, 0, buffer.Length)
                    .ConfigureAwait(false);

                var bodyAsText = Encoding.UTF8.GetString(buffer).Trim();

                if (!string.IsNullOrEmpty(bodyAsText))
                {
                    result.AppendLine($"Body: {bodyAsText}");
                }

                context.Request.Body = body;
            }

            return result.ToString();
        }
    }
}