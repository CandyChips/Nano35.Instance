using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsState
{
    public class LoggedUpdateClientsStateRequest :
        IPipelineNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsStateRequest> _logger;
        private readonly IPipelineNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract> _nextNode;

        public LoggedUpdateClientsStateRequest(
            ILogger<LoggedUpdateClientsStateRequest> logger,
            IPipelineNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateClientsStateResultContract> Ask(
            IUpdateClientsStateRequestContract input)
        {
            _logger.LogInformation($"UpdateClientsStateLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateClientsStateLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsStateSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsStateErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}