using System.Threading.Tasks;

namespace StatementIQ.Common.Web.Managers
{
    public interface IRequestManager
    {
        Task<T> GetAsync<T>(string requestUrl, string authToken, string traceIdentifier);
    }
}