using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsName
{
    public class LoggedUpdateClientsNameRequest :
        PipeNodeBase
        <IUpdateClientsNameRequestContract,
            IUpdateClientsNameResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsNameRequest> _logger;

        public LoggedUpdateClientsNameRequest(
            ILogger<LoggedUpdateClientsNameRequest> logger,
            IPipeNode<IUpdateClientsNameRequestContract,
                IUpdateClientsNameResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateClientsNameResultContract> Ask(
            IUpdateClientsNameRequestContract input)
        {
            _logger.LogInformation($"UpdateClientsNameLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateClientsNameLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}