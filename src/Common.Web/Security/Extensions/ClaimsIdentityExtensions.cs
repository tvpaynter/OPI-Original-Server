using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace StatementIQ.Common.Web.Security.Extensions
{
    internal static class ClaimsIdentityExtensions
    {
        internal static T GetClaimValue<T>(this IIdentity identity, string claimValue)
        {
            var claimsIdentity = identity as ClaimsIdentity;

            var claimValueString = claimsIdentity?.Claims?.SingleOrDefault(claim => claim.Type == claimValue)
                ?.Value;

            if (string.IsNullOrEmpty(claimValueString))
            {
                throw new Exception($"bad {claimValue} claim");
            }

            return (T) Convert.ChangeType(claimValueString, typeof(T));
        }
    }
}