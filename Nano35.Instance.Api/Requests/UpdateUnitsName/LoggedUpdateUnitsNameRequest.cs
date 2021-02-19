using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsName
{
    public class LoggedUpdateUnitsNameRequest :
        IPipelineNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>
    {
        private readonly ILogger<LoggedUpdateUnitsNameRequest> _logger;
        private readonly IPipelineNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract> _nextNode;

        public LoggedUpdateUnitsNameRequest(
            ILogger<LoggedUpdateUnitsNameRequest> logger,
            IPipelineNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateUnitsNameResultContract> Ask(
            IUpdateUnitsNameRequestContract input)
        {
            _logger.LogInformation($"UpdateUnitsNameLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateUnitsNameLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateUnitsNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateUnitsNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}