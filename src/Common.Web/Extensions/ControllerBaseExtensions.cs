using MandateThat;
using Microsoft.AspNetCore.Mvc;

namespace StatementIQ.Common.Web.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static long GetUserId(this ControllerBase controller)
        {
            Mandate.That(controller, nameof(controller)).IsNotNull();

            return controller.User.Identity.GetClaimValue(Constants.AuthenticationClaimTypes.UserId);
        }
    }
}