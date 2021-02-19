using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAvailableCashOfUnit
{
    public class LoggedGetAvailableCashOfUnitRequest :
        IPipelineNode<IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract>
    {
        private readonly ILogger<LoggedGetAvailableCashOfUnitRequest> _logger;
        private readonly IPipelineNode<IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract> _nextNode;

        public LoggedGetAvailableCashOfUnitRequest(
            ILogger<LoggedGetAvailableCashOfUnitRequest> logger,
            IPipelineNode<IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAvailableCashOfUnitResultContract> Ask(
            IGetAvailableCashOfUnitRequestContract input)
        {
            _logger.LogInformation($"GetAvailableCashOfUnitLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAvailableCashOfUnitLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAvailableCashOfUnitSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAvailableCashOfUnitErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}