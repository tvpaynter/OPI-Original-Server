using System.Collections.Generic;
using System.Linq;
using System.Net;
using StatementIQ.Common.Web.Models;

namespace StatementIQ.Common.Web.Extensions
{
    internal static class StringExtensions
    {
        internal static List<IpModel> GetPorts(this string urlsString)
        {
            return urlsString.Split(';')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.GetIpModel()).ToList();
        }

        private static IpModel GetIpModel(this string url)
        {
            var scheme = url.GetScheme();
            var ip = url.GetIp();
            var port = url.GetPort();

            IPAddress ipAddress;

            switch (ip)
            {
                case "+":
                case "*":
                    ipAddress = IPAddress.Any;
                    break;
                case "localhost":
                    ipAddress = IPAddress.Loopback;
                    break;
                default:
                    ipAddress = IPAddress.Parse(ip);
                    break;
            }

            return new IpModel
            {
                Ip = ip,
                Port = port,
                IpAddress = ipAddress,
                Protocol = scheme
            };
        }

        private static string GetScheme(this string url)
        {
            return url.Split("://").FirstOrDefault();
        }

        private static string GetIp(this string url)
        {
            return url.Split("://").LastOrDefault()?.Split(':').FirstOrDefault();
        }

        private static ushort GetPort(this string url)
        {
            var portString = url.Split("://").LastOrDefault()?.Split(':').LastOrDefault();

            return ushort.Parse(portString);
        }
    }
}