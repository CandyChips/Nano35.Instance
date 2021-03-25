using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsState
{
    public class LoggedUpdateClientsStateRequest :
        PipeNodeBase<
            IUpdateClientsStateRequestContract, 
            IUpdateClientsStateResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsStateRequest> _logger;
        public LoggedUpdateClientsStateRequest(
            ILogger<LoggedUpdateClientsStateRequest> logger,
            IPipeNode<IUpdateClientsStateRequestContract,
                IUpdateClientsStateResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateClientsStateResultContract> Ask(
            IUpdateClientsStateRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateClientsState starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateClientsState ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsStateSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsStateErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}