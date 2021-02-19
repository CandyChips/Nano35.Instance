using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsName
{
    public class LoggedUpdateClientsNameRequest :
        IPipelineNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsNameRequest> _logger;
        private readonly IPipelineNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract> _nextNode;

        public LoggedUpdateClientsNameRequest(
            ILogger<LoggedUpdateClientsNameRequest> logger,
            IPipelineNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateClientsNameResultContract> Ask(
            IUpdateClientsNameRequestContract input)
        {
            _logger.LogInformation($"UpdateClientsNameLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateClientsNameLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}