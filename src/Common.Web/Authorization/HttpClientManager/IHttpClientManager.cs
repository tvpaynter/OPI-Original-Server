using System.Threading.Tasks;

namespace StatementIQ.Common.Web.Authorization.HttpClientManager
{
    public interface IHttpClientManager
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object objectToSend);
    }
}