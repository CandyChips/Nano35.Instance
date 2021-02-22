using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsSelle
{
    public class LoggedUpdateClientsSelleRequest :
        IPipelineNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsSelleRequest> _logger;
        private readonly IPipelineNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract> _nextNode;

        public LoggedUpdateClientsSelleRequest(
            ILogger<LoggedUpdateClientsSelleRequest> logger,
            IPipelineNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateClientsSelleResultContract> Ask(
            IUpdateClientsSelleRequestContract input)
        {
            _logger.LogInformation($"UpdateClientsSelleLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateClientsSelleLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsSelleSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsSelleErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}