using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using MandateThat;

namespace StatementIQ.Common.Web.Authorization
{
    public static class JwtTokenHelper
    {
        public static JwtSecurityToken Parse(string jwtTokenString)
        {
            Mandate.That(jwtTokenString, nameof(jwtTokenString)).IsNotNullOrWhiteSpace(jwtTokenString);

            var handler = new JwtSecurityTokenHandler();

            return handler.ReadToken(jwtTokenString) as JwtSecurityToken;
        }

        public static T Parse<T>(string jwtTokenString, string claimType)
        {
            Mandate.That(jwtTokenString, nameof(jwtTokenString)).IsNotNullOrWhiteSpace(jwtTokenString);

            var token = Parse(jwtTokenString);

            var tokenValueString = token.Claims.SingleOrDefault(claim => claim.Type == claimType)?.Value;

            return (T) Convert.ChangeType(tokenValueString, typeof(T));
        }

        public static bool IsTokenAboutToExpire(string jwtTokenString)
        {
            Mandate.That(jwtTokenString, nameof(jwtTokenString)).IsNotNullOrWhiteSpace(jwtTokenString);

            var expiryTimestamp = Parse<int>(jwtTokenString, "exp");

            var expiryDateTime = DateTimeOffset.FromUnixTimeSeconds(expiryTimestamp).LocalDateTime;

            return expiryDateTime < DateTime.Now;
        }
    }
}