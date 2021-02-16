using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllClients
{
    public class LoggedGetAllClientsRequest :
        IPipelineNode<
            IGetAllClientsRequestContract, 
            IGetAllClientsResultContract>
    {
        private readonly ILogger<LoggedGetAllClientsRequest> _logger;
        private readonly IPipelineNode<
            IGetAllClientsRequestContract,
            IGetAllClientsResultContract> _nextNode;

        public LoggedGetAllClientsRequest(
            ILogger<LoggedGetAllClientsRequest> logger,
            IPipelineNode<
                IGetAllClientsRequestContract,
                IGetAllClientsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllClientsResultContract> Ask(
            IGetAllClientsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllClientsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
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