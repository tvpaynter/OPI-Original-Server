using System.Threading.Tasks;
using MandateThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using StatementIQ.Common.Web.Extensions;
using StatementIQ.Common.Web.Managers;

namespace StatementIQ.Common.Web.Filters.PermissionFeatureRequirement
{
    public class PermissionsFilter : IAsyncAuthorizationFilter
    {
        public PermissionsFilter(
            string featureCode, ILogger<PermissionsFilter> logger, IPermissionsManager featuresManager)
        {
            Mandate.That(featureCode, nameof(featureCode)).IsNotNullOrWhiteSpace();
            Mandate.That(logger, nameof(logger)).IsNotNull();
            Mandate.That(featuresManager, nameof(featuresManager)).IsNotNull();

            FeatureCode = featureCode;
            Logger = logger;
            PermissionsManager = featuresManager;
        }

        private string FeatureCode { get; }
        private ILogger<PermissionsFilter> Logger { get; }
        private IPermissionsManager PermissionsManager { get; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            Logger.Log(LogLevel.Information,
                $"{context.GetRequestInfo()}. Check if user has permission: {FeatureCode}");

            var hasPermission = await PermissionsManager.CheckIfUserHasPermission(context, FeatureCode);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}