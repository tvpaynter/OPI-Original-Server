using Microsoft.AspNetCore.Mvc;

namespace StatementIQ.Common.Web.Security.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static long GetUserId(this ControllerBase controller)
        {
            return controller.User.Identity.GetClaimValue<long>(Constants.AuthenticationClaimTypes.UserId);
        }
    }
}