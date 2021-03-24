using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateInstance
{
    public class LoggedCreateInstanceRequest :
        PipeNodeBase<
            ICreateInstanceRequestContract, 
            ICreateInstanceResultContract>
    {
        private readonly ILogger<LoggedCreateInstanceRequest> _logger;
        

        public LoggedCreateInstanceRequest(
            ILogger<LoggedCreateInstanceRequest> logger,
            IPipeNode<ICreateInstanceRequestContract,
                ICreateInstanceResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateInstanceLogger starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"CreateInstanceLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case ICreateInstanceSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case ICreateInstanceErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}