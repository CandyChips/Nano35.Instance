using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateClient
{
    public class LoggedCreateClientRequest :
        PipeNodeBase<
            ICreateClientRequestContract, 
            ICreateClientResultContract>
    {
        private readonly ILogger<LoggedCreateClientRequest> _logger;

        public LoggedCreateClientRequest(
            ILogger<LoggedCreateClientRequest> logger,
            IPipeNode<ICreateClientRequestContract,
                ICreateClientResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateClientLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"CreateClientLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case ICreateClientSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case ICreateClientErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}