using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsAddress
{
    public class LoggedUpdateUnitsAddressRequest :
        IPipelineNode<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsAddressRequest> _logger;
        private readonly IPipelineNode<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract> _nextNode;

        public LoggedUpdateUnitsAddressRequest(
            ILogger<LoggedUpdateUnitsAddressRequest> logger,
            IPipelineNode<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateUnitsAddressResultContract> Ask(
            IUpdateUnitsAddressRequestContract input)
        {
            _logger.LogInformation($"UpdateUnitsAddressLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateUnitsAddressLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsAddressSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsAddressErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}