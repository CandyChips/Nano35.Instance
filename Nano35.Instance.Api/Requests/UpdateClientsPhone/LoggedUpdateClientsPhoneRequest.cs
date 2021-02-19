using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsPhone
{
    public class LoggedUpdateClientsPhoneRequest :
        IPipelineNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>
    {
        private readonly ILogger<LoggedUpdateClientsPhoneRequest> _logger;
        private readonly IPipelineNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract> _nextNode;

        public LoggedUpdateClientsPhoneRequest(
            ILogger<LoggedUpdateClientsPhoneRequest> logger,
            IPipelineNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateClientsPhoneResultContract> Ask(
            IUpdateClientsPhoneRequestContract input)
        {
            _logger.LogInformation($"UpdateClientsPhoneLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateClientsPhoneLogger ends on: {DateTime.Now}");
            
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