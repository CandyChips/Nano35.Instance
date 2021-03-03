using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersName
{
    public class LoggedUpdateWorkersNameRequest :
        IPipelineNode<
            IUpdateWorkersNameRequestContract,
            IUpdateWorkersNameResultContract>
    {
        private readonly ILogger<LoggedUpdateWorkersNameRequest> _logger;
        private readonly IPipelineNode<
            IUpdateWorkersNameRequestContract,
            IUpdateWorkersNameResultContract> _nextNode;

        public LoggedUpdateWorkersNameRequest(
            ILogger<LoggedUpdateWorkersNameRequest> logger,
            IPipelineNode<
                IUpdateWorkersNameRequestContract,
                IUpdateWorkersNameResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateWorkersNameResultContract> Ask(
            IUpdateWorkersNameRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateWorkersName starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateWorkersName ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateWorkersNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateWorkersNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}