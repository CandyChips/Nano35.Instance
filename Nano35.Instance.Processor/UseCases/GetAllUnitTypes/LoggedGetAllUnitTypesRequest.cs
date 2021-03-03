using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitTypes
{
    public class LoggedGetAllUnitTypesRequest :
        IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>
    {
        private readonly ILogger<LoggedGetAllUnitTypesRequest> _logger;
        private readonly IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract> _nextNode;

        public LoggedGetAllUnitTypesRequest(
            ILogger<LoggedGetAllUnitTypesRequest> logger,
            IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllUnitTypesResultContract> Ask(IGetAllUnitTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllUnitTypesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllUnitTypesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllUnitTypesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllUnitTypesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}