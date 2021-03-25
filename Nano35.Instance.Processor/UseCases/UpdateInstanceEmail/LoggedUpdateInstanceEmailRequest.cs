using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceEmail
{
    public class LoggedUpdateInstanceEmailRequest :
        PipeNodeBase<
            IUpdateInstanceEmailRequestContract,
            IUpdateInstanceEmailResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceEmailRequest> _logger;

        public LoggedUpdateInstanceEmailRequest(
            ILogger<LoggedUpdateInstanceEmailRequest> logger,
            IPipeNode<IUpdateInstanceEmailRequestContract,
                IUpdateInstanceEmailResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateInstanceEmailResultContract> Ask(
            IUpdateInstanceEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateInstanceEmail starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateInstanceEmail ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceEmailSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceEmailErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}