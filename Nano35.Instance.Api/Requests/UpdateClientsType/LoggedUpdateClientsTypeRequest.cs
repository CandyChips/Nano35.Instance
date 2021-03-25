using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsType
{
    public class LoggedUpdateClientsTypeRequest :
        PipeNodeBase
        <IUpdateClientsTypeRequestContract,
            IUpdateClientsTypeResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsTypeRequest> _logger;

        public LoggedUpdateClientsTypeRequest(
            ILogger<LoggedUpdateClientsTypeRequest> logger,
            IPipeNode<IUpdateClientsTypeRequestContract,
                IUpdateClientsTypeResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateClientsTypeResultContract> Ask(
            IUpdateClientsTypeRequestContract input)
        {
            _logger.LogInformation($"UpdateClientsTypeLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"UpdateClientsTypeLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateClientsTypeSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateClientsTypeErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}