using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRegions
{
    public class LoggedGetAllRegionsRequest :
        IPipelineNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract>
    {
        private readonly ILogger<LoggedGetAllRegionsRequest> _logger;
        private readonly IPipelineNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract> _nextNode;

        public LoggedGetAllRegionsRequest(
            ILogger<LoggedGetAllRegionsRequest> logger,
            IPipelineNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllRegionsResultContract> Ask(
            IGetAllRegionsRequestContract input)
        {
            _logger.LogInformation($"GetAllRegionsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllRegionsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}