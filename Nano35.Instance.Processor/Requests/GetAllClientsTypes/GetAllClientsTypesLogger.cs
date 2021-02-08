using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllClientsTypes
{
    public class GetAllClientTypesLogger :
        IPipelineNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>
    {
        private readonly ILogger<GetAllClientTypesLogger> _logger;
        private readonly IPipelineNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract> _nextNode;

        public GetAllClientTypesLogger(
            ILogger<GetAllClientTypesLogger> logger,
            IPipelineNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllClientTypesResultContract> Ask(IGetAllClientTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllClientTypesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllClientTypesLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}