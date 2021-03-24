using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfSelle
{
    public class LoggedCreatePaymentOfSelleRequest :
        PipeNodeBase<
            ICreatePaymentOfSelleRequestContract, 
            ICreatePaymentOfSelleResultContract>
    {
        private readonly ILogger<LoggedCreatePaymentOfSelleRequest> _logger;
        

        public LoggedCreatePaymentOfSelleRequest(
            ILogger<LoggedCreatePaymentOfSelleRequest> logger,
            IPipeNode<ICreatePaymentOfSelleRequestContract,
                ICreatePaymentOfSelleResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreatePaymentOfSelleLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
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