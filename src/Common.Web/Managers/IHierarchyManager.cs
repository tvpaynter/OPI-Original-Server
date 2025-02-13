using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;

namespace StatementIQ.Common.Web.Managers
{
    public interface IHierarchyManager
    {
        Task<IEnumerable<long>> GetCurrentUserHierarchiesIdsAsync();
    }
}