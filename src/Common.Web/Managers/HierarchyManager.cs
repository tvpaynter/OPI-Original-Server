using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MandateThat;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using StatementIQ.Common.Autofac;
using StatementIQ.Common.Web.Extensions;

namespace StatementIQ.Common.Web.Managers
{
    [SingleInstance]
    public class HierarchyManager : IHierarchyManager
    {
        public HierarchyManager(IRequestManager requestManager,
            ILogger<HierarchyManager> logger, IHttpContextAccessor iHttpContextAccessor)
        {
            Mandate.That(requestManager, nameof(requestManager)).IsNotNull();
            Mandate.That(logger, nameof(logger)).IsNotNull();
            Mandate.That(iHttpContextAccessor, nameof(iHttpContextAccessor)).IsNotNull();

            RequestManager = requestManager;
            Logger = logger;
            HttpContextAccessor = iHttpContextAccessor;
        }

        private IHttpContextAccessor HttpContextAccessor { get; }
        private ILogger<HierarchyManager> Logger { get; }
        private IRequestManager RequestManager { get; }

        private const string CurrentUserHierarchiesIds = "me/hierarchiesIds";

        public async Task<IEnumerable<long>> GetCurrentUserHierarchiesIdsAsync()
        {
            var authToken = HttpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString()?
                .Replace("Bearer ", string.Empty);

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                var stringBuilder = new StringBuilder();

                try
                {
                    stringBuilder.AppendLine(
                        $"{HttpContextAccessor.HttpContext.GetRequestInfo()}. Permissions validation api url {CurrentUserHierarchiesIds}");

                    var hierarchies =
                        await RequestManager
                            .GetAsync<IEnumerable<long>>(CurrentUserHierarchiesIds, authToken,
                                HttpContextAccessor.HttpContext.TraceIdentifier)
                            .ConfigureAwait(false);

                    if (hierarchies != null)
                    {
                        stringBuilder.AppendLine(
                            $"{HttpContextAccessor.HttpContext.GetRequestInfo()}. Got {hierarchies.Count()} hierarchies");

                        return hierarchies;
                    }
                }
                finally
                {
                    Logger.Log(LogLevel.Information, stringBuilder.ToString());
                }
            }

            return null;
        }
    }
}