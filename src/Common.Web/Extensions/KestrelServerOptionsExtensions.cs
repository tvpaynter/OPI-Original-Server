using System;
using System.Linq;
using System.Net;
using MandateThat;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using StatementIQ.Common.Web.Models;

namespace StatementIQ.Common.Web.Extensions
{
    public static class KestrelServerOptionsExtensions
    {
        public static void ListenDefault(this KestrelServerOptions options)
        {
            Mandate.That(options, nameof(options)).IsNotNull();

            var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
            var httpsPortString = Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT");

            var ports = urls?.GetPorts();

            IpModel httpsIpModel = null;

            if (ushort.TryParse(httpsPortString, out var httpsPort))
            {
                httpsIpModel =
                    ports?.FirstOrDefault(port => port.Protocol.Equals("https") && port.Port.Equals(httpsPort));
            }

            httpsIpModel ??= new IpModel
            {
                IpAddress = IPAddress.Any,
                Port = 443
            };

            if (!string.IsNullOrEmpty(urls))
            {
                var ipModels = ports.Where(port => port.Protocol.Equals("http") && !port.Port.Equals(httpsPort));

                foreach (var ipModel in ipModels)
                {
                    options.Listen(ipModel.IpAddress, ipModel.Port);
                }
            }

            options.Listen(httpsIpModel.IpAddress, httpsIpModel.Port,
                async listenOptions => { await listenOptions.UsePemCertificate().ConfigureAwait(false); });
        }
    }
}