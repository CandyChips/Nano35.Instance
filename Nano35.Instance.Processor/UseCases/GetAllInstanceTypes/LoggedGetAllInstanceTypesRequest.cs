using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllInstanceTypes
{
    public class LoggedGetAllInstanceTypesRequest :
        IPipelineNode<
            IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract>
    {
        private readonly ILogger<LoggedGetAllInstanceTypesRequest> _logger;
        private readonly IPipelineNode<
            IGetAllInstanceTypesRequestContract, 
            IGetAllInstanceTypesResultContract> _nextNode;

        public LoggedGetAllInstanceTypesRequest(
            ILogger<LoggedGetAllInstanceTypesRequest> logger,
            IPipelineNode<
                IGetAllInstanceTypesRequestContract,
                IGetAllInstanceTypesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllInstanceTypesResultContract> Ask(
            IGetAllInstanceTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllInstanceTypesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllInstanceTypesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllInstanceTypesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllInstanceTypesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}