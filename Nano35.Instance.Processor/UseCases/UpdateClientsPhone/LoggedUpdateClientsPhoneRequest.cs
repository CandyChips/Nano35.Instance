using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsPhone
{
    public class LoggedUpdateClientsPhoneRequest :
        PipeNodeBase<
            IUpdateClientsPhoneRequestContract,
            IUpdateClientsPhoneResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsPhoneRequest> _logger;

        public LoggedUpdateClientsPhoneRequest(
            ILogger<LoggedUpdateClientsPhoneRequest> logger,
            IPipeNode<IUpdateClientsPhoneRequestContract,
                IUpdateClientsPhoneResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateClientsPhoneResultContract> Ask(
            IUpdateClientsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateClientsPhone starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateClientsPhone ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsPhoneSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsPhoneErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}