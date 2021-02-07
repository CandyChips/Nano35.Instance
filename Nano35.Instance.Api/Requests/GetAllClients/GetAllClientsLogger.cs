using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClients
{
    public class GetAllClientsLogger :
        IPipelineNode<IGetAllClientsRequestContract, IGetAllClientsResultContract>
    {
        private readonly ILogger<GetAllClientsLogger> _logger;
        private readonly IPipelineNode<IGetAllClientsRequestContract, IGetAllClientsResultContract> _nextNode;

        public GetAllClientsLogger(
            ILogger<GetAllClientsLogger> logger,
            IPipelineNode<IGetAllClientsRequestContract, IGetAllClientsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllClientsResultContract> Ask(
            IGetAllClientsRequestContract input)
        {
            _logger.LogInformation($"GetAllClientsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllClientsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}