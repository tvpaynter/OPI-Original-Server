using System.Threading;
using System.Threading.Tasks;
using Utg.Api.Models.OPIModels;

namespace Utg.Api.Interfaces
{
    public interface IUTGService
    {
        Task<TransactionResponse> ProcessMessage(TransactionRequest request, CancellationToken cancellationToken = default);
    }
}