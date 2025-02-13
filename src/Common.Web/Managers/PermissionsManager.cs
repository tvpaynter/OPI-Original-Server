using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MandateThat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using StatementIQ.Common.Autofac;
using StatementIQ.Common.Data;
using StatementIQ.Common.Web.Extensions;
using StatementIQ.Common.Web.Models;

namespace StatementIQ.Common.Web.Managers
{
    [SingleInstance]
    public class PermissionsManager : IPermissionsManager
    {
        public PermissionsManager(IRequestManager requestManager,
            ILogger<PermissionsManager> logger, IHttpContextAccessor iHttpContextAccessor)
        {
            Mandate.That(requestManager, nameof(requestManager)).IsNotNull();
            Mandate.That(logger, nameof(logger)).IsNotNull();
            Mandate.That(iHttpContextAccessor, nameof(iHttpContextAccessor)).IsNotNull();

            RequestManager = requestManager;
            Logger = logger;
            HttpContextAccessor = iHttpContextAccessor;
        }

        private IHttpContextAccessor HttpContextAccessor { get; }
        private ILogger<PermissionsManager> Logger { get; }
        private IRequestManager RequestManager { get; }

        public async Task<bool> CheckIfUserHasPermission(AuthorizationFilterContext context, string featureCode)
        {
            Mandate.That(featureCode, nameof(featureCode)).IsNotNullOrEmpty();

            var authToken = context.HttpContext.Request.Headers[HeaderNames.Authorization].ToString()?
                .Replace("Bearer ", string.Empty);

            var hasPermissions = false;

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                var hierarchyId = GetCurrentHierarchyId();

                if (hierarchyId != default)
                {
                    var stringBuilder = new StringBuilder();

                    try
                    {
                        var requestUrl = $"/me/features/?take={int.MaxValue}";

                        stringBuilder.AppendLine(
                            $"{context.GetRequestInfo()}. Permissions validation api url {requestUrl}. Hierarchy id: {hierarchyId}.");

                        var featuresPagination =
                            await RequestManager
                                .GetAsync<Pagination<FeatureViewModel>>(requestUrl, authToken,
                                    context.HttpContext.TraceIdentifier)
                                .ConfigureAwait(false);

                        if (featuresPagination != null)
                        {
                            stringBuilder.AppendLine(
                                $"{context.GetRequestInfo()}. Got {featuresPagination.Count} features by Hierarchy id: {hierarchyId}");

                            hasPermissions = featuresPagination.Results.Any(x =>
                                x.FeatureCode.Equals(featureCode, StringComparison.InvariantCultureIgnoreCase));
                        }
                    }
                    finally
                    {
                        Logger.Log(LogLevel.Information, stringBuilder.ToString());
                    }
                }
                else
                {
                    Logger.Log(LogLevel.Information,
                        $"{context.GetRequestInfo()}. User HierarchyId id is default(0). No features available.");
                }
            }

            return hasPermissions;
        }

        private long GetCurrentHierarchyId()
        {
            return HttpContextAccessor.HttpContext.User.Identity.GetClaimValue(Constants.AuthenticationClaimTypes
                .HierarchyId);
        }
    }
}