using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateUnit
{
    public class LoggedCreateUnitRequest :
        PipeNodeBase<
            ICreateUnitRequestContract,
            ICreateUnitResultContract>
    {
        private readonly ILogger<LoggedCreateUnitRequest> _logger;
        

        public LoggedCreateUnitRequest(
            ILogger<LoggedCreateUnitRequest> logger,
            IPipeNode<ICreateUnitRequestContract,
                ICreateUnitResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateUnitResultContract> Ask(ICreateUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateUnitLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
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