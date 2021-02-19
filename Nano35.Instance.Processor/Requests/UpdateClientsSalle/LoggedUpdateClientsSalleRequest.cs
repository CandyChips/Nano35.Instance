using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateClientsSalle
{
    public class LoggedUpdateClientsSalleRequest :
        IPipelineNode<
            IUpdateClientsSalleRequestContract,
            IUpdateClientsSalleResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsSalleRequest> _logger;
        
        private readonly IPipelineNode<
            IUpdateClientsSalleRequestContract, 
            IUpdateClientsSalleResultContract> _nextNode;

        public LoggedUpdateClientsSalleRequest(
            ILogger<LoggedUpdateClientsSalleRequest> logger,
            IPipelineNode<
                IUpdateClientsSalleRequestContract, 
                IUpdateClientsSalleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateClientsSalleResultContract> Ask(
            IUpdateClientsSalleRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateClientsSalle starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateClientsSalle ends on: {DateTime.Now}");
            
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