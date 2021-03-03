using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfSelle
{
    public class LoggedCreatePaymentOfSelleRequest :
        IPipelineNode<
            ICreatePaymentOfSelleRequestContract, 
            ICreatePaymentOfSelleResultContract>
    {
        private readonly ILogger<LoggedCreatePaymentOfSelleRequest> _logger;
        
        private readonly IPipelineNode<
            ICreatePaymentOfSelleRequestContract, 
            ICreatePaymentOfSelleResultContract> _nextNode;

        public LoggedCreatePaymentOfSelleRequest(
            ILogger<LoggedCreatePaymentOfSelleRequest> logger,
            IPipelineNode<
                ICreatePaymentOfSelleRequestContract,
                ICreatePaymentOfSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreatePaymentOfSelleLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreatePaymentOfSelleLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case ICreatePaymentOfSelleSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case ICreatePaymentOfSelleErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}