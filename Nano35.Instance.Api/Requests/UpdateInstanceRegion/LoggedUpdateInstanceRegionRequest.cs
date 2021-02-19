using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceRegion
{
    public class LoggedUpdateInstanceRegionRequest :
        IPipelineNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceRegionRequest> _logger;
        private readonly IPipelineNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract> _nextNode;

        public LoggedUpdateInstanceRegionRequest(
            ILogger<LoggedUpdateInstanceRegionRequest> logger,
            IPipelineNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateInstanceRegionResultContract> Ask(
            IUpdateInstanceRegionRequestContract input)
        {
            _logger.LogInformation($"UpdateInstanceRegionLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateInstanceRegionLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceRegionSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceRegionErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}