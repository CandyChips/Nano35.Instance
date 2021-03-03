using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitsByType
{
    public class LoggedGetAllUnitsByTypeRequest :
        IPipelineNode<
            IGetAllUnitsByTypeRequestContract,
            IGetAllUnitsByTypeResultContract>
    {
        private readonly ILogger<LoggedGetAllUnitsByTypeRequest> _logger;
        private readonly IPipelineNode<
            IGetAllUnitsByTypeRequestContract,
            IGetAllUnitsByTypeResultContract> _nextNode;

        public LoggedGetAllUnitsByTypeRequest(
            ILogger<LoggedGetAllUnitsByTypeRequest> logger,
            IPipelineNode<
                IGetAllUnitsByTypeRequestContract, 
                IGetAllUnitsByTypeResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllUnitsByTypeResultContract> Ask(
            IGetAllUnitsByTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllUnitsByTypeLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllUnitsByTypeLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllUnitsByTypeSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllUnitsByTypeErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}