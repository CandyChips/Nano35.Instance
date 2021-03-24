using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateCashInput
{
    public class LoggedCreateCashInputRequest :
        PipeNodeBase<
            ICreateCashInputRequestContract, 
            ICreateCashInputResultContract>
    {
        private readonly ILogger<LoggedCreateCashInputRequest> _logger;
        

        public LoggedCreateCashInputRequest(
            ILogger<LoggedCreateCashInputRequest> logger,
            IPipeNode<ICreateCashInputRequestContract, ICreateCashInputResultContract> next) : base(next)
        {
            
            _logger = logger;
        }

        public override async Task<ICreateCashInputResultContract> Ask(
            ICreateCashInputRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateCashInputLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
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