using MandateThat;
using Microsoft.AspNetCore.Mvc;

namespace StatementIQ.Common.Web.Filters.PermissionFeatureRequirement
{
    /// <summary>
    ///     Attribute that checks if a user has required features by feature code.
    /// </summary>
    /// <example>
    ///     This sample shows how to use this attribute. Could be used with controller or with action method.
    ///     <code>
    /// [PermissionsRequirement("documents_read")]
    /// public class DomainController : ControllerBase
    /// {
    ///     ...
    /// }
    /// </code>
    /// </example>
    public class PermissionFeatureRequirementAttribute : TypeFilterAttribute
    {
        public PermissionFeatureRequirementAttribute(string featureCode)
            : base(typeof(PermissionsFilter))
        {
            Mandate.That(featureCode, nameof(featureCode)).IsNotNullOrWhiteSpace();

            Arguments = new object[] {featureCode};
        }
    }
}