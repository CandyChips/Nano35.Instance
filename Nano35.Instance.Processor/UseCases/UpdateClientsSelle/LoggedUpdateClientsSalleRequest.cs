using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsSelle
{
    public class LoggedUpdateClientsSelleRequest :
        PipeNodeBase<
            IUpdateClientsSelleRequestContract,
            IUpdateClientsSelleResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsSelleRequest> _logger;

        public LoggedUpdateClientsSelleRequest(
            ILogger<LoggedUpdateClientsSelleRequest> logger,
            IPipeNode<IUpdateClientsSelleRequestContract,
                IUpdateClientsSelleResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateClientsSelleResultContract> Ask(
            IUpdateClientsSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateClientsSelle starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateClientsSelle ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsSelleSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsSelleErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}