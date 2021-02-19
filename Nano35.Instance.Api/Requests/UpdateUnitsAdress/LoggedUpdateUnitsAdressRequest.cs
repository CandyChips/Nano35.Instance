using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsAdress
{
    public class LoggedUpdateUnitsAdressRequest :
        IPipelineNode<IUpdateUnitsAdressRequestContract, IUpdateUnitsAdressResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsAdressRequest> _logger;
        private readonly IPipelineNode<IUpdateUnitsAdressRequestContract, IUpdateUnitsAdressResultContract> _nextNode;

        public LoggedUpdateUnitsAdressRequest(
            ILogger<LoggedUpdateUnitsAdressRequest> logger,
            IPipelineNode<IUpdateUnitsAdressRequestContract, IUpdateUnitsAdressResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateUnitsAdressResultContract> Ask(
            IUpdateUnitsAdressRequestContract input)
        {
            _logger.LogInformation($"UpdateUnitsAdressLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateUnitsAdressLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsAdressSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsAdressErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}