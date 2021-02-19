using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsType
{
    public class LoggedUpdateUnitsTypeRequest :
        IPipelineNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsTypeRequest> _logger;
        private readonly IPipelineNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract> _nextNode;

        public LoggedUpdateUnitsTypeRequest(
            ILogger<LoggedUpdateUnitsTypeRequest> logger,
            IPipelineNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateUnitsTypeResultContract> Ask(
            IUpdateUnitsTypeRequestContract input)
        {
            _logger.LogInformation($"UpdateUnitsTypeLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateUnitsTypeLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsTypeSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsTypeErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}