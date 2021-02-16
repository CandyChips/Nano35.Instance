using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateUnit
{
    public class LoggedCreateUnitRequest :
        IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        private readonly ILogger<LoggedCreateUnitRequest> _logger;
        private readonly IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract> _nextNode;

        public LoggedCreateUnitRequest(
            ILogger<LoggedCreateUnitRequest> logger,
            IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateUnitResultContract> Ask(ICreateUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateUnitLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateUnitLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case ICreateUnitSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case ICreateUnitErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}