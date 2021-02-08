using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetInstanceById
{
    public class GetInstanceByIdLogger :
        IPipelineNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>
    {
        private readonly ILogger<GetInstanceByIdLogger> _logger;
        private readonly IPipelineNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract> _nextNode;

        public GetInstanceByIdLogger(
            ILogger<GetInstanceByIdLogger> logger,
            IPipelineNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetInstanceByIdResultContract> Ask(IGetInstanceByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetInstanceByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetInstanceByIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}