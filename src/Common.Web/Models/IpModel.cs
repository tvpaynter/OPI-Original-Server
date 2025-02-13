using System.Net;

namespace StatementIQ.Common.Web.Models
{
    internal class IpModel
    {
        internal ushort Port { get; set; }
        internal string Protocol { get; set; }
        internal string Ip { get; set; }
        internal IPAddress IpAddress { get; set; }
    }
}