using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateCashOutput
{
    public class LoggedCreateCashOutputRequest :
        PipeNodeBase<
            ICreateCashOutputRequestContract,
            ICreateCashOutputResultContract>
    {
        private readonly ILogger<LoggedCreateCashOutputRequest> _logger;

        public LoggedCreateCashOutputRequest(
            ILogger<LoggedCreateCashOutputRequest> logger,
            IPipeNode<ICreateCashOutputRequestContract,
                ICreateCashOutputResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateCashOutputResultContract> Ask(
            ICreateCashOutputRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCashOutputLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
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