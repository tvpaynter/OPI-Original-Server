using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MandateThat;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace StatementIQ.Common.Web.Managers
{
    public class RequestManager : IRequestManager
    {
        public RequestManager(HttpClient httpClient, ILogger<RequestManager> logger)
        {
            Mandate.That(httpClient, nameof(httpClient)).IsNotNull();
            Mandate.That(logger, nameof(logger)).IsNotNull();

            HttpClient = httpClient;
            Logger = logger;
        }

        private HttpClient HttpClient { get; }
        private ILogger<RequestManager> Logger { get; }

        public async Task<T> GetAsync<T>(string requestUrl, string authToken, string traceIdentifier)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            requestMessage.Headers.Authorization =
                new AuthenticationHeaderValue(Constants.Authentication.Bearer, authToken);

            requestMessage.Headers.TryAddWithoutValidation("X-Correlation-ID", traceIdentifier);

            var response = await HttpClient.SendAsync(requestMessage);

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T featuresPagination = default;

            Logger.Log(LogLevel.Information, $"Response code {response.StatusCode} content {responseString}");

            if (response.IsSuccessStatusCode)
            {
                featuresPagination = JsonConvert.DeserializeObject<T>(responseString);
            }

            return featuresPagination;
        }
    }
}