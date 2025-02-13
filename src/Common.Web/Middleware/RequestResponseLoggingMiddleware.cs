using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MandateThat;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StatementIQ.Common.Web.Extensions;

namespace StatementIQ.Common.Web.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private RequestDelegate Next { get; }
        private ILogger<RequestResponseLoggingMiddleware> Logger { get; }

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            Mandate.That(next, nameof(next)).IsNotNull();
            Mandate.That(logger, nameof(logger)).IsNotNull();

            Next = next;
            Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestBody = string.Empty;
            var requestHeaders = new List<string>(context.Request.HttpContext.Request.Headers.Keys);
            string responseBody;
            var responseHeaders = new List<string>();
            int responseStatusCode;

            if (context.Request.ContentLength.HasValue && context.Request.ContentLength > 0)
            {
                context.Request.EnableBuffering();

                using var reader = new StreamReader(
                    context.Request.Body,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    bufferSize: Convert.ToInt32(context.Request.ContentLength),
                    leaveOpen: true);

                requestBody = await reader.ReadToEndAsync().ConfigureAwait(false);

                context.Request.Body.Position = 0;
            }

            foreach (var key in context.Request.HttpContext.Request.Headers.Keys)
            {
                requestHeaders.Add($"{key}={context.Request.HttpContext.Request.Headers[key]}");
            }

            var originalBodyStream = context.Response.Body;

            using (var responseStream = new MemoryStream())
            {
                context.Response.Body = responseStream;

                await Next(context);

                responseBody = await GetResponseBody(context.Response).ConfigureAwait(false);

                foreach (var key in context.Response.HttpContext.Request.Headers.Keys)
                {
                    responseHeaders.Add($"{key}={context.Response.HttpContext.Response.Headers[key]}");
                }

                responseStatusCode = context.Response.StatusCode;

                await responseStream.CopyToAsync(originalBodyStream).ConfigureAwait(false);
            }
            if (context.Request != null && context.Request.ContentLength.HasValue && !string.IsNullOrEmpty(requestBody))
            {
                requestBody = Utils.GetRequestResponseBodyMasked(requestBody);
            }
            else if (context.Response != null && context.Response.ContentLength.HasValue && !string.IsNullOrEmpty(responseBody))
            {
                responseBody = Utils.GetRequestResponseBodyMasked(responseBody);
            }
            var result = new
            {
                Request = new
                {
                    Info = context.GetRequestInfo(),
                    context.Request.QueryString,
                    Body = requestBody,
                    Headers = requestHeaders
                },
                Response = new
                {
                    Body = responseBody,
                    Headers = responseHeaders,
                    StatusCode = responseStatusCode
                },
            };

            Logger.Log(LogLevel.Information, JsonConvert.SerializeObject(result));
        }

        private async Task<string> GetResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(response.Body).ReadToEndAsync().ConfigureAwait(false);

            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }
    }
}