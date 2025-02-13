using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MandateThat;
using StatementIQ.Common.Web.Authorization.Exceptions;
using StatementIQ.Common.Web.Authorization.Models;

namespace StatementIQ.Common.Web.Authorization.HttpClientManager
{
    public static class AuthorizedClientHelper
    {
        public static async Task<HttpClient> GetAuthorizedClientAsync(string baseAddress, string authServiceAddress,
            string userEmail, string userPassword)
        {
            Mandate.That(baseAddress, nameof(baseAddress)).IsNotNullOrWhiteSpace();
            Mandate.That(authServiceAddress, nameof(authServiceAddress)).IsNotNullOrWhiteSpace();
            Mandate.That(userEmail, nameof(userEmail)).IsNotNullOrWhiteSpace();
            Mandate.That(userPassword, nameof(userPassword)).IsNotNullOrWhiteSpace();

            try
            {
                var handler = new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.All
                };

                var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(baseAddress)
                };

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("user-agent",
                    Constants.LoginHeaders.UserAgent);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding",
                    Constants.LoginHeaders.AcceptEncoding);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language",
                    Constants.LoginHeaders.AcceptLanguage);

                var loginViewModel = new LoginViewModel
                {
                    UserName = userEmail,
                    Password = userPassword
                };

                var content = loginViewModel.GetStringContent();

                Console.Write("Attempt to login ");

                var loginResponseMessage = await httpClient
                    .PostAsync($"{authServiceAddress}{Constants.ApiUrls.Authentication.Login}", content)
                    .ConfigureAwait(false);

                Console.WriteLine($"Response status code: {loginResponseMessage.StatusCode}.");

                await httpClient.SetAuthorizationHeaderAsync(loginResponseMessage).ConfigureAwait(false);

                Console.WriteLine("Logged in successfully");

                return httpClient;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error. Server couldn't be found. Url: {authServiceAddress}. Message: {ex.Message}");

                return null;
            }
        }

        public static bool IsTokenAboutToExpire(this HttpClient httpClient)
        {
            Mandate.That(httpClient, nameof(httpClient)).IsNotNull();

            var token = httpClient.DefaultRequestHeaders.Authorization.Parameter;

            return JwtTokenHelper.IsTokenAboutToExpire(token);
        }

        public static async Task UpdateJwtTokenAsync(this HttpClient httpClient)
        {
            Mandate.That(httpClient, nameof(httpClient)).IsNotNull();

            var refreshTokenResponseMessage = await httpClient
                .GetAsync(Constants.ApiUrls.Authentication.RefreshToken)
                .ConfigureAwait(false);

            await httpClient.SetAuthorizationHeaderAsync(refreshTokenResponseMessage).ConfigureAwait(false);
        }

        private static async Task SetAuthorizationHeaderAsync(this HttpClient httpClient,
            HttpResponseMessage responseMessage)
        {
            Mandate.That(responseMessage, nameof(responseMessage)).IsNotNull();
            Mandate.That(httpClient, nameof(httpClient)).IsNotNull();

            var loginResponseString = await responseMessage.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            Console.WriteLine($"Response content: {loginResponseString}");
            
            var loginResponseViewModel = loginResponseString.Deserialize<LoginResponseWrapperViewModel>();

            var token = loginResponseViewModel.Token;

            if (string.IsNullOrEmpty(token)) throw new NoAuthTokenException("token is null or empty");

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
}