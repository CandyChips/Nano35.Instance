using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstancePhone
{
    public class LoggedUpdateInstancePhoneRequest :
        PipeNodeBase<
            IUpdateInstancePhoneRequestContract, 
            IUpdateInstancePhoneResultContract>
    {
        private readonly ILogger<LoggedUpdateInstancePhoneRequest> _logger;

        public LoggedUpdateInstancePhoneRequest(
            ILogger<LoggedUpdateInstancePhoneRequest> logger,
            IPipeNode<IUpdateInstancePhoneRequestContract,
                IUpdateInstancePhoneResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateInstancePhoneResultContract> Ask(
            IUpdateInstancePhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateInstancePhone starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateInstancePhone ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstancePhoneSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstancePhoneErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}