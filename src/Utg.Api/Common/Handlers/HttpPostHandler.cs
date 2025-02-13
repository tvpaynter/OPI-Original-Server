using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utg.Api.Common.Handlers
{
    /// <summary>
    /// Http Post Handler
    /// </summary>
    public class HttpPostHandler
    {
        private IConfiguration _configuration { get; set; }
        private readonly ILogger<HttpPostHandler> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        public HttpPostHandler(ILogger<HttpPostHandler> logger, IConfiguration config)
        {
            _configuration = config;
            _logger = logger;
        }
        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="requestXml"></param>
        /// <returns></returns>
        public async Task<string> SendMessage(string requestXml)
        {
            /*The XML request data to be posted to Rapid Connect Transaction Service URL*/            
            string responseXML = string.Empty;
            string Url = _configuration["TrxSettings:URL"];
            /* Instantiate the WebRequest object.*/
            using (var client = CreateWorkaroundClient())
            {
                StringContent content = new(requestXml, Encoding.UTF8, "application/xml");
                _logger.Log(LogLevel.Debug, "Sending request to TRX Service");
                client.Timeout = TimeSpan.FromSeconds(int.Parse(_configuration["TrxSettings:ClientTimeout"]));
                HttpResponseMessage response = await client.PostAsync(Url, content);                
                responseXML = response.Content.ReadAsStringAsync().Result;
            }
            return responseXML;
        }

        public static HttpClient CreateWorkaroundClient()
        {
            SocketsHttpHandler handler = new SocketsHttpHandler
            {
                ConnectCallback = IPv4ConnectAsync
            };
            return new HttpClient(handler);

            static async ValueTask<Stream> IPv4ConnectAsync(SocketsHttpConnectionContext context, CancellationToken cancellationToken)
            {
                // By default, we create dual-mode sockets:
                // Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.NoDelay = true;

                try
                {
                    await socket.ConnectAsync(context.DnsEndPoint, cancellationToken).ConfigureAwait(false);
                    return new NetworkStream(socket, ownsSocket: true);
                }
                catch
                {
                    socket.Dispose();
                    throw;
                }
            }
        }
    }
}
