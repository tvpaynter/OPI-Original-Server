using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MandateThat;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StatementIQ.Common.Web.Extensions;

namespace StatementIQ.Common.Web.Middleware
{
    public class SecurityHeaderMiddleware
    {
        private RequestDelegate Next { get; }
        private ILogger<SecurityHeaderMiddleware> Logger { get; }
        public static IConfiguration Configuration { get; private set; }

        public SecurityHeaderMiddleware(RequestDelegate next, ILogger<SecurityHeaderMiddleware> logger, IConfiguration configuration)
        {
            Mandate.That(next, nameof(next)).IsNotNull();
            Mandate.That(logger, nameof(logger)).IsNotNull();
            Next = next;
            Logger = logger;
            Configuration = configuration;
        }

        public Task Invoke(HttpContext httpContext)
        {
            try
            {
                httpContext.Response.Headers.Add("X-Xss-Protection", "1");
                httpContext.Response.Headers.Add("X-Frame-Options", "DENY");
                httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                httpContext.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
                httpContext.Response.Headers.Add("Referrer-Policy", "no-referrer");
                httpContext.Response.Headers.Add("Content-Security-Policy", GetCspHeaderValue());
                httpContext.Response.Headers.Add("Feature-Policy", GetFeaturePolicyHeaderValue());
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error. Path: {httpContext?.Request?.Path}. Error while setting response header. Message: {ex?.Message}");
            }
            return Next(httpContext);
        }

        public string GetCspHeaderValue()
        {
            StringBuilder sbCspValue = new();

            var cspheaderVal = sbCspValue.Append("default-src 'self';")
                      .Append("img-src 'self'; ")
                      .Append("style-src 'self' ")
                      .Append("script-src 'self' ")
                      .Append("frame-src 'self'; ")
                      .Append("default-src 'self';")
                      .Append("object-src 'self' ")
                      .Append("form-action 'self' ")
                      .Append("connect-src 'self';").ToString();

            var SwaggerAllowedEnv = Configuration["SwaggerAllowedEnv"];

            if (SwaggerAllowedEnv != null && SwaggerAllowedEnv.Contains(Configuration["environment"]))
            {
                cspheaderVal = sbCspValue.Append("style-src 'self' 'unsafe-inline';")
                   .Append("script-src 'self' 'unsafe-inline' 'unsafe-eval';").ToString();
            }

            return cspheaderVal;
        }

        public string GetFeaturePolicyHeaderValue()
        {
            StringBuilder sbFeaturePolicyValue = new();
            return sbFeaturePolicyValue.Append("geolocation 'none';")
                      .Append("midi 'none';")
                      .Append("notifications 'none';")
                      .Append("push 'none'; ")
                      .Append("sync-xhr 'none'; ")
                      .Append("microphone 'none'; ")
                      .Append("camera 'none';")
                      .Append("magnetometer 'none'; ")
                      .Append("gyroscope 'none'; ")
                      .Append("speaker 'self'; ")
                      .Append("vibrate 'none'; ")
                      .Append("fullscreen 'self'; ")
                      .Append("payment 'self';").ToString();

        }
    }
}
