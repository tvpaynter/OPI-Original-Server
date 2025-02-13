using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using MandateThat;
using StatementIQ.Common.Web.Exceptions;

namespace StatementIQ.Common.Web.Extensions
{
    internal static class ClaimsIdentityExtensions
    {
        internal static long GetClaimValue(this IIdentity identity, string claimValue)
        {
            Mandate.That(identity, nameof(claimValue)).IsNotNull();
            Mandate.That(claimValue, nameof(claimValue)).IsNotNullOrWhiteSpace();

            var claimsIdentity = identity as ClaimsIdentity;

            var claimValueString = claimsIdentity?.Claims
                ?.SingleOrDefault(claim => claim.Type == claimValue)
                ?.Value;

            if (string.IsNullOrEmpty(claimValueString))
            {
                throw new BadClaimException($"bad {claimValue} claim");
            }

            if (!long.TryParse(claimValueString, out var userId))
            {
                throw new BadClaimException($"Couldn't parse {claimValue} to long");
            }

            return userId;
        }
    }
}