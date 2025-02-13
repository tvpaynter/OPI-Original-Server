using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MandateThat;
using Microsoft.Extensions.Logging;
using StatementIQ.Common.Web.Authorization.Exceptions;
using StatementIQ.Common.Web.Authorization.Interfaces;
using StatementIQ.Common.Web.Authorization.Models;

namespace StatementIQ.Common.Web.Authorization
{
    public class AuthenticationClientManager : IAuthenticationClientManager
    {
        private readonly IAuthenticationSettings _authenticationSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthenticationClientManager> _logger;

        public AuthenticationClientManager(HttpClient httpClient, IAuthenticationSettings authenticationSettings,
            ILogger<AuthenticationClientManager> logger)
        {
            Mandate.That(logger, nameof(logger)).IsNotNull();
            Mandate.That(httpClient, nameof(httpClient)).IsNotNull();
            Mandate.That(httpClient.BaseAddress, nameof(httpClient.BaseAddress)).IsNotNull();
            Mandate.That(httpClient.BaseAddress?.Host, nameof(httpClient.BaseAddress.Host)).IsNotNullOrWhiteSpace();
            Mandate.That(authenticationSettings, nameof(authenticationSettings)).IsNotNull();
            Mandate.That(authenticationSettings.Password, nameof(authenticationSettings.Password))
                .IsNotNullOrWhiteSpace();
            Mandate.That(authenticationSettings.User, nameof(authenticationSettings.User)).IsNotNullOrWhiteSpace();


            _httpClient = httpClient;
            _authenticationSettings = authenticationSettings;
            _logger = logger;
        }

        public async Task<string> GetTokenAsync()
        {
            var loginViewModel = new LoginViewModel
            {
                UserName = _authenticationSettings.User,
                Password = _authenticationSettings.Password
            };

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("user-agent",
                LoginHeaders.UserAgent);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding",
                LoginHeaders.AcceptEncoding);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language",
                LoginHeaders.AcceptLanguage);

            var content = loginViewModel.GetStringContent();

            try
            {
                _logger.LogInformation("Attempt to login ");

                var loginResponseMessage = await _httpClient.PostAsync(ApiUrls.Authentication.Login, content)
                    .ConfigureAwait(false);

                if (loginResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    throw new NoAuthTokenException($"Server couldn't be found: {_httpClient.BaseAddress}");
                }

                var loginResponseString = await loginResponseMessage.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);

                var loginResponseViewModel = loginResponseString.Deserialize<LoginResponseWrapperViewModel>();

                var token = loginResponseViewModel.Token;

                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new NoAuthTokenException("Token is empty");
                }

                _logger.LogInformation("Logged in successfully");

                return token;
            }
            catch (HttpRequestException)
            {
                _logger.LogError($"Error. Server couldn't be found. Url: {_httpClient.BaseAddress}");
                throw;
            }
        }
    }
}