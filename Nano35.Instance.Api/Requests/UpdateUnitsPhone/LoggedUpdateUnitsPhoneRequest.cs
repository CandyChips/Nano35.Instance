using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsPhone
{
    public class LoggedUpdateUnitsPhoneRequest :
        IPipelineNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsPhoneRequest> _logger;
        private readonly IPipelineNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract> _nextNode;

        public LoggedUpdateUnitsPhoneRequest(
            ILogger<LoggedUpdateUnitsPhoneRequest> logger,
            IPipelineNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateUnitsPhoneResultContract> Ask(
            IUpdateUnitsPhoneRequestContract input)
        {
            _logger.LogInformation($"UpdateUnitsPhoneLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateUnitsPhoneLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsPhoneSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsPhoneErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}