using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateClientsName
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

        public async Task<IUpdateClientsNameResultContract> Ask(IUpdateClientsNameRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateClientsName starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateClientsName ends on: {DateTime.Now}");
            
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