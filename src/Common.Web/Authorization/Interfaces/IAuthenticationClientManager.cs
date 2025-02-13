using System.Threading.Tasks;

namespace StatementIQ.Common.Web.Authorization.Interfaces
{
    public interface IAuthenticationClientManager
    {
        Task<string> GetTokenAsync();
    }
}