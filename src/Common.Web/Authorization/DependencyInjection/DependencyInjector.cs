using System;
using System.Net;
using System.Net.Http;
using MandateThat;
using Microsoft.Extensions.DependencyInjection;
using StatementIQ.Common.Web.Authorization.Interfaces;

namespace StatementIQ.Common.Web.Authorization.DependencyInjection
{
    public static class DependencyInjector
    {
        public static IServiceCollection AddAuthorization(this IServiceCollection services,
            string authorizationServerUrl)
        {
            Mandate.That(services, nameof(services)).IsNotNull();
            Mandate.That(authorizationServerUrl, nameof(authorizationServerUrl)).IsNotNullOrWhiteSpace();

            services.AddSingleton<IAuthenticationClientManager, AuthenticationClientManager>();

            services.AddHttpClient<IAuthenticationClientManager, AuthenticationClientManager>(
                    authenticationClientManagerSettings =>
                    {
                        authenticationClientManagerSettings.BaseAddress = new Uri(authorizationServerUrl);
                    })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                });

            return services;
        }

        public static IServiceCollection AddAuthorizationSettings(this IServiceCollection services)
        {
            Mandate.That(services, nameof(services)).IsNotNull();

            services.AddSingleton<IAuthenticationSettings, AuthenticationSettings>();

            return services;
        }
    }
}