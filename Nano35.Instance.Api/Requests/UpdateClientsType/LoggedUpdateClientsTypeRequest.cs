using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsType
{
    public class LoggedUpdateClientsTypeRequest :
        IPipelineNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsTypeRequest> _logger;
        private readonly IPipelineNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract> _nextNode;

        public LoggedUpdateClientsTypeRequest(
            ILogger<LoggedUpdateClientsTypeRequest> logger,
            IPipelineNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateClientsTypeResultContract> Ask(
            IUpdateClientsTypeRequestContract input)
        {
            _logger.LogInformation($"UpdateClientsTypeLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateClientsTypeLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsTypeSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsTypeErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}