using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsEmail
{
    public class LoggedUpdateClientsEmailRequest :
        PipeNodeBase<
            IUpdateClientsEmailRequestContract,
            IUpdateClientsEmailResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsEmailRequest> _logger;
        

        public LoggedUpdateClientsEmailRequest(
            ILogger<LoggedUpdateClientsEmailRequest> logger,
            IPipeNode<IUpdateClientsEmailRequestContract,
                IUpdateClientsEmailResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateClientsEmailResultContract> Ask(
            IUpdateClientsEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateClientsEmail starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateClientsEmail ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsEmailSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsEmailErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}