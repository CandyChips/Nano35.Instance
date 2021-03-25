using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsSelle
{
    public class LoggedUpdateClientsSelleRequest :
        PipeNodeBase
        <IUpdateClientsSelleRequestContract,
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
            IUpdateClientsSelleRequestContract input)
        {
            _logger.LogInformation($"UpdateClientsSelleLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateClientsSelleLogger ends on: {DateTime.Now}");
            
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