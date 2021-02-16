using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClients
{
    public class LoggedGetAllClientsRequest :
        IPipelineNode<IGetAllClientsRequestContract, IGetAllClientsResultContract>
    {
        private readonly ILogger<LoggedGetAllClientsRequest> _logger;
        private readonly IPipelineNode<IGetAllClientsRequestContract, IGetAllClientsResultContract> _nextNode;

        public LoggedGetAllClientsRequest(
            ILogger<LoggedGetAllClientsRequest> logger,
            IPipelineNode<IGetAllClientsRequestContract, IGetAllClientsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllClientsResultContract> Ask(
            IGetAllClientsRequestContract input)
        {
            _logger.LogInformation($"Get all clients logger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            // Check response of get all client types request
            _logger.LogInformation($"Get all clients logger ends on: {DateTime.Now} with responce {result}");
            switch (result)
            {
                case IGetAllRegionsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllRegionsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}