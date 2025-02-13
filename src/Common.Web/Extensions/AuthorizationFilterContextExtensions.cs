using MandateThat;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StatementIQ.Common.Web.Extensions
{
    public static class AuthorizationFilterContextExtensions
    {
        public static string GetRequestInfo(this AuthorizationFilterContext context)
        {
            Mandate.That(context, nameof(context)).IsNotNull();

            return context.HttpContext.GetRequestInfo();
        }
    }
}