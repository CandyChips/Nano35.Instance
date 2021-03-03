using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateCashOutput
{
    public class LoggedCreateCashOutputRequest :
        IPipelineNode<
            ICreateCashOutputRequestContract,
            ICreateCashOutputResultContract>
    {
        private readonly ILogger<LoggedCreateCashOutputRequest> _logger;
        private readonly IPipelineNode<
            ICreateCashOutputRequestContract,
            ICreateCashOutputResultContract> _nextNode;

        public LoggedCreateCashOutputRequest(
            ILogger<LoggedCreateCashOutputRequest> logger,
            IPipelineNode<
                ICreateCashOutputRequestContract,
                ICreateCashOutputResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateCashOutputResultContract> Ask(
            ICreateCashOutputRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCashOutputLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateCashOutputLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case ICreateCashOutputSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case ICreateCashOutputErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}