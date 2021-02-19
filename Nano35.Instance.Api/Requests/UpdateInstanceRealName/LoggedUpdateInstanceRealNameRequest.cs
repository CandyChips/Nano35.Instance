using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceRealName
{
    public class LoggedUpdateInstanceRealNameRequest :
        IPipelineNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceRealNameRequest> _logger;
        private readonly IPipelineNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract> _nextNode;

        public LoggedUpdateInstanceRealNameRequest(
            ILogger<LoggedUpdateInstanceRealNameRequest> logger,
            IPipelineNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateInstanceRealNameResultContract> Ask(
            IUpdateInstanceRealNameRequestContract input)
        {
            _logger.LogInformation($"UpdateInstanceRealNameLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateInstanceRealNameLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceRealNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceRealNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}