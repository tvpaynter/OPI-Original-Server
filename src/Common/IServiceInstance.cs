using System;
using System.Threading;
using System.Threading.Tasks;

namespace StatementIQ
{
    public interface IServiceInstance
    {
        Task RunAsync(CancellationToken cancellationToken = default);

        void OnException(Exception ex);
    }
}