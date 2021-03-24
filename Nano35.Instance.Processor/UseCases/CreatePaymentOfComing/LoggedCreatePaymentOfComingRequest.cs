using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfComing
{
    public class LoggedCreatePaymentOfComingRequest :
        PipeNodeBase<
            ICreatePaymentOfComingRequestContract, 
            ICreatePaymentOfComingResultContract>
    {
        private readonly ILogger<LoggedCreatePaymentOfComingRequest> _logger;

        public LoggedCreatePaymentOfComingRequest(
            ILogger<LoggedCreatePaymentOfComingRequest> logger,
            IPipeNode<ICreatePaymentOfComingRequestContract,
                ICreatePaymentOfComingResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreatePaymentOfComingResultContract> Ask(
            ICreatePaymentOfComingRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreatePaymentOfComingLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"CreatePaymentOfComingLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case ICreatePaymentOfComingSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case ICreatePaymentOfComingErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}