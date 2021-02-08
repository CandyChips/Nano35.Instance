using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllRegions
{
    public class GetAllRegionsLogger :
        IPipelineNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract>
    {
        private readonly ILogger<GetAllRegionsLogger> _logger;
        private readonly IPipelineNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract> _nextNode;

        public GetAllRegionsLogger(
            ILogger<GetAllRegionsLogger> logger,
            IPipelineNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllRegionsResultContract> Ask(IGetAllRegionsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllRegionsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllRegionsLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}