using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Utg.Api.Interfaces;
using Utg.Api.Models.OPIModels;

namespace Utg.Api.Controllers
{
    /// <summary>
    /// OpiListener Controller
    /// </summary>
    [Route("v1/TrxService")]
    [ApiController]
    public class OpiListenerController : ControllerBase
    {
        private readonly ILogger<OpiListenerController> _logger;
        private readonly IUTGService _serviceProvider;

        /// <summary>
        /// OpiListenerController Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceProvider"></param>
        public OpiListenerController(ILogger<OpiListenerController> logger,  IUTGService serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// OPI Process request
        /// </summary>
        /// <param name="transRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TransactionResponse> OPIRequest([FromBody] TransactionRequest transRequest)
        {           
            return await _serviceProvider.ProcessMessage(transRequest);
        }
    }
}
