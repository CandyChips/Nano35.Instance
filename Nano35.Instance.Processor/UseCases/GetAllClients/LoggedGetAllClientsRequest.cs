using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllClients
{
    public class LoggedGetAllClientsRequest :
        PipeNodeBase<
            IGetAllClientsRequestContract, 
            IGetAllClientsResultContract>
    {
        private readonly ILogger<LoggedGetAllClientsRequest> _logger;

        public LoggedGetAllClientsRequest(
            ILogger<LoggedGetAllClientsRequest> logger,
            IPipeNode<IGetAllClientsRequestContract,
                IGetAllClientsResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllClientsResultContract> Ask(
            IGetAllClientsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllClientsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"GetAllClientsLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllClientsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllClientsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}