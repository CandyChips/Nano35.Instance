using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceName
{
    public class LoggedUpdateInstanceNameRequest :
        IPipelineNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceNameRequest> _logger;
        private readonly IPipelineNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract> _nextNode;

        public LoggedUpdateInstanceNameRequest(
            ILogger<LoggedUpdateInstanceNameRequest> logger,
            IPipelineNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateInstanceNameResultContract> Ask(
            IUpdateInstanceNameRequestContract input)
        {
            _logger.LogInformation($"UpdateInstanceNameLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateInstanceNameLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}