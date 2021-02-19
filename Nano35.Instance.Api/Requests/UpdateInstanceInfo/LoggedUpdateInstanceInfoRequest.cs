using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceInfo
{
    public class LoggedUpdateInstanceInfoRequest :
        IPipelineNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceInfoRequest> _logger;
        private readonly IPipelineNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract> _nextNode;

        public LoggedUpdateInstanceInfoRequest(
            ILogger<LoggedUpdateInstanceInfoRequest> logger,
            IPipelineNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateInstanceInfoResultContract> Ask(
            IUpdateInstanceInfoRequestContract input)
        {
            _logger.LogInformation($"UpdateInstanceInfoLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateInstanceInfoLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceInfoSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceInfoErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}