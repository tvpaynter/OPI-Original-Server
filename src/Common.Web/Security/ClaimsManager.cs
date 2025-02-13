using MandateThat;
using Microsoft.AspNetCore.Http;
using StatementIQ.Common.Autofac;
using StatementIQ.Common.Web.Exceptions;
using StatementIQ.Common.Web.Security.Extensions;
using StatementIQ.Common.Web.Security.Interfaces;

namespace StatementIQ.Common.Web.Security
{
    [InstancePerLifetimeScope]
    public class ClaimsManager : IClaimsManager
    {
        private readonly IHttpContextAccessor _iHttpContextAccessor;

        public ClaimsManager(IHttpContextAccessor iHttpContextAccessor)
        {
            Mandate.That(iHttpContextAccessor, nameof(iHttpContextAccessor)).IsNotNull();

            _iHttpContextAccessor = iHttpContextAccessor;
        }

        public long GetCurrentSessionId()
        {
            return _iHttpContextAccessor.HttpContext.User.Identity.GetClaimValue<long>(Constants
                .AuthenticationClaimTypes.SessionId);
        }

        public long GetCurrentUserId()
        {
            return _iHttpContextAccessor.HttpContext.User.Identity.GetClaimValue<long>(Constants
                .AuthenticationClaimTypes.UserId);
        }

        public long GetCurrentUserHierarchyId()
        {
            return _iHttpContextAccessor.HttpContext.User.Identity.GetClaimValue<long>(Constants
                .AuthenticationClaimTypes.HierarchyId);
        }

        public string GetCurrentAuthorizationToken()
        {
            if (_iHttpContextAccessor.HttpContext.Request.Headers.TryGetValue("authorization", out var token))
            {
                return token.ToString();
            }

            return string.Empty;
        }
    }
}