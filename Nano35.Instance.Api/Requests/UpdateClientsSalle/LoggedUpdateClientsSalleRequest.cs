using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsSalle
{
    public class LoggedUpdateClientsSalleRequest :
        IPipelineNode<IUpdateClientsSalleRequestContract, IUpdateClientsSalleResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsSalleRequest> _logger;
        private readonly IPipelineNode<IUpdateClientsSalleRequestContract, IUpdateClientsSalleResultContract> _nextNode;

        public LoggedUpdateClientsSalleRequest(
            ILogger<LoggedUpdateClientsSalleRequest> logger,
            IPipelineNode<IUpdateClientsSalleRequestContract, IUpdateClientsSalleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateClientsSalleResultContract> Ask(
            IUpdateClientsSalleRequestContract input)
        {
            _logger.LogInformation($"UpdateClientsSalleLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateClientsSalleLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsSalleSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsSalleErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}