using System.Net.Http;
using System.Threading.Tasks;
using MandateThat;

namespace StatementIQ.Common.Web.Authorization.HttpClientManager
{
    public class HttpClientManager : IHttpClientManager
    {
        private readonly string _authenticationServiceUrl;
        private readonly string _baseUrl;
        private readonly string _userEmail;
        private readonly string _userPassword;

        private HttpClient _httpClient;

        public HttpClientManager(string serverAddress, string userEmail, string userPassword)
        {
            Mandate.That(serverAddress, nameof(serverAddress)).IsNotNullOrEmpty();
            Mandate.That(userEmail, nameof(userEmail)).IsNotNullOrEmpty();
            Mandate.That(userPassword, nameof(userPassword)).IsNotNullOrEmpty();

            _authenticationServiceUrl = serverAddress;
            _baseUrl = serverAddress;
            _userEmail = userEmail;
            _userPassword = userPassword;
        }

        public HttpClientManager(string baseAddress, string serverAddress, string userEmail, string userPassword)
        {
            Mandate.That(serverAddress, nameof(serverAddress)).IsNotNullOrEmpty();
            Mandate.That(userEmail, nameof(userEmail)).IsNotNullOrEmpty();
            Mandate.That(userPassword, nameof(userPassword)).IsNotNullOrEmpty();
            Mandate.That(baseAddress, nameof(baseAddress)).IsNotNullOrEmpty();

            _baseUrl = baseAddress;
            _authenticationServiceUrl = serverAddress;
            _userEmail = userEmail;
            _userPassword = userPassword;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            Mandate.That(url, nameof(url)).IsNotNullOrEmpty();

            var client = await GetHttpClientAsync().ConfigureAwait(false);

            var responseMessage = await client.GetAsync(url).ConfigureAwait(false);

            return !responseMessage.IsSuccessStatusCode
                ? default
                : (await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false)).Deserialize<T>();
        }

        public async Task<T> PostAsync<T>(string url, object objectToSend)
        {
            Mandate.That(url, nameof(url)).IsNotNullOrEmpty();
            Mandate.That(objectToSend, nameof(objectToSend)).IsNotNull();

            var client = await GetHttpClientAsync().ConfigureAwait(false);

            var responseMessage =
                await client.PostAsync(url, objectToSend.GetStringContent()).ConfigureAwait(false);

            return !responseMessage.IsSuccessStatusCode
                ? default
                : (await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false)).Deserialize<T>();
        }

        private async Task<HttpClient> GetHttpClientAsync()
        {
            _httpClient ??= await AuthorizedClientHelper
                .GetAuthorizedClientAsync(_baseUrl, _authenticationServiceUrl, _userEmail, _userPassword)
                .ConfigureAwait(false);

            if (_httpClient.IsTokenAboutToExpire())
            {
                await _httpClient.UpdateJwtTokenAsync().ConfigureAwait(false);
            }

            return _httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            Mandate.That(url, nameof(url)).IsNotNullOrEmpty();

            var client = await GetHttpClientAsync().ConfigureAwait(false);

            return await client.GetAsync(url).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object objectToSend)
        {
            Mandate.That(url, nameof(url)).IsNotNullOrEmpty();
            Mandate.That(objectToSend, nameof(objectToSend)).IsNotNull();

            var client = await GetHttpClientAsync().ConfigureAwait(false);

            return await client.PostAsync(url, objectToSend.GetStringContent()).ConfigureAwait(false);
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage requestMessage)
        {
            Mandate.That(requestMessage, nameof(requestMessage)).IsNotNull();

            var client = await GetHttpClientAsync().ConfigureAwait(false);

            var responseMessage = await client.SendAsync(requestMessage)
                .ConfigureAwait(false);

            return !responseMessage.IsSuccessStatusCode
                ? default
                : (await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false)).Deserialize<T>();
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            Mandate.That(requestMessage, nameof(requestMessage)).IsNotNull();

            var client = await GetHttpClientAsync().ConfigureAwait(false);

            return await client.SendAsync(requestMessage).ConfigureAwait(false);
        }
    }
}