using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateCashInput
{
    public class LoggedCreateCashInputRequest :
        IPipelineNode<
            ICreateCashInputRequestContract, 
            ICreateCashInputResultContract>
    {
        private readonly ILogger<LoggedCreateCashInputRequest> _logger;
        private readonly IPipelineNode<
            ICreateCashInputRequestContract, 
            ICreateCashInputResultContract> _nextNode;

        public LoggedCreateCashInputRequest(
            ILogger<LoggedCreateCashInputRequest> logger,
            IPipelineNode<
                ICreateCashInputRequestContract,
                ICreateCashInputResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateCashInputResultContract> Ask(
            ICreateCashInputRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCashInputLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateCashInputLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case ICreateCashInputSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case ICreateCashInputErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}