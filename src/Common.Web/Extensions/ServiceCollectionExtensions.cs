using System;
using MandateThat;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StatementIQ.Common.Web.Managers;
using StatementIQ.Common.Web.Models;

namespace StatementIQ.Common.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthentication(
            this IServiceCollection services, AuthenticationOptionsModel authenticationOptions)
        {
            Mandate.That(services, nameof(services)).IsNotNull();
            Mandate.That(authenticationOptions, nameof(authenticationOptions)).IsNotNull();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.EventsType = authenticationOptions.EventsType;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authenticationOptions.SecretKey,

                    ValidateIssuer = true,
                    ValidIssuer = authenticationOptions.TokenIssuer,

                    ValidateAudience = true,
                    ValidAudience = authenticationOptions.TokenAudience,

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = authenticationOptions.ClockSkew //5 minute tolerance for the expiration date
                };
            });
        }

        /// <summary>
        ///     Register http client
        /// </summary>
        /// <param name="services">
        ///     <see cref="IServiceCollection" />
        /// </param>
        /// <param name="baseAddress">Handler base address</param>
        /// <param name="timeSpan">Handler life time timespan</param>
        public static void AddPermissionsManagerHttpClient(
            this IServiceCollection services, string baseAddress, TimeSpan timeSpan)
        {
            Mandate.That(services, nameof(services)).IsNotNull();
            Mandate.That(baseAddress, nameof(baseAddress)).IsNotNullOrWhiteSpace();

            services.AddHttpClient<IRequestManager, RequestManager>(x => { x.BaseAddress = new Uri(baseAddress); })
                .SetHandlerLifetime(timeSpan);
        }
    }
}