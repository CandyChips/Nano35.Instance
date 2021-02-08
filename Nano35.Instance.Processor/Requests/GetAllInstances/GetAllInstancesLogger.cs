using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllInstances
{
    public class GetAllInstancesLogger :
        IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly ILogger<GetAllInstancesLogger> _logger;
        private readonly IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> _nextNode;

        public GetAllInstancesLogger(
            ILogger<GetAllInstancesLogger> logger,
            IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllInstancesResultContract> Ask(IGetAllInstancesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllInstancesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllInstancesLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}