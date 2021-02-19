using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstancePhone
{
    public class LoggedUpdateInstancePhoneRequest :
        IPipelineNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>
    {
        private readonly ILogger<LoggedUpdateInstancePhoneRequest> _logger;
        private readonly IPipelineNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract> _nextNode;

        public LoggedUpdateInstancePhoneRequest(
            ILogger<LoggedUpdateInstancePhoneRequest> logger,
            IPipelineNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateInstancePhoneResultContract> Ask(
            IUpdateInstancePhoneRequestContract input)
        {
            _logger.LogInformation($"UpdateInstancePhoneLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateInstancePhoneLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstancePhoneSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstancePhoneErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}