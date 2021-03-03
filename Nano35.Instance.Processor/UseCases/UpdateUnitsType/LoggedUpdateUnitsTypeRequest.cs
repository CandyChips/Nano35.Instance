using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsType
{
    public class LoggedUpdateUnitsTypeRequest :
        IPipelineNode<
            IUpdateUnitsTypeRequestContract,
            IUpdateUnitsTypeResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsTypeRequest> _logger;
        private readonly IPipelineNode<
            IUpdateUnitsTypeRequestContract,
            IUpdateUnitsTypeResultContract> _nextNode;

        public LoggedUpdateUnitsTypeRequest(
            ILogger<LoggedUpdateUnitsTypeRequest> logger,
            IPipelineNode<
                IUpdateUnitsTypeRequestContract,
                IUpdateUnitsTypeResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateUnitsTypeResultContract> Ask(
            IUpdateUnitsTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateUnitsType starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateUnitsType ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsTypeSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsTypeErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}