using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StatementIQ.Common.Web.Managers
{
    public interface IPermissionsManager
    {
        Task<bool> CheckIfUserHasPermission(AuthorizationFilterContext context, string featureCode);
    }
}