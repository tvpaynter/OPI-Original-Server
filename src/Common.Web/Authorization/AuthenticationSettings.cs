using MandateThat;
using Microsoft.Extensions.Configuration;
using StatementIQ.Common.Web.Authorization.Interfaces;

namespace StatementIQ.Common.Web.Authorization
{
    public class AuthenticationSettings : IAuthenticationSettings
    {
        private readonly IConfiguration _config;

        public AuthenticationSettings(IConfiguration config)
        {
            Mandate.That(config).IsNotNull();

            _config = config;
        }

        public string Password => _config["AuthenticationSettings:Password"];

        public string User => _config["AuthenticationSettings:User"];
    }
}