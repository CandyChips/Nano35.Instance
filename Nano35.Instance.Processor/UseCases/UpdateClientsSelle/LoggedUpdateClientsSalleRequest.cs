using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsSelle
{
    public class LoggedUpdateClientsSelleRequest :
        IPipelineNode<
            IUpdateClientsSelleRequestContract,
            IUpdateClientsSelleResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsSelleRequest> _logger;
        
        private readonly IPipelineNode<
            IUpdateClientsSelleRequestContract, 
            IUpdateClientsSelleResultContract> _nextNode;

        public LoggedUpdateClientsSelleRequest(
            ILogger<LoggedUpdateClientsSelleRequest> logger,
            IPipelineNode<
                IUpdateClientsSelleRequestContract, 
                IUpdateClientsSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateClientsSelleResultContract> Ask(
            IUpdateClientsSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateClientsSelle starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateClientsSelle ends on: {DateTime.Now}");
            
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