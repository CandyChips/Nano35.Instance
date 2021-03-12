using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringById
{
    public class LoggedGetUnitStringByIdRequest :
        IPipelineNode<
            IGetUnitStringByIdRequestContract,
            IGetUnitStringByIdResultContract>
    {
        private readonly ILogger<LoggedGetUnitStringByIdRequest> _logger;
        private readonly IPipelineNode<
            IGetUnitStringByIdRequestContract, 
            IGetUnitStringByIdResultContract> _nextNode;

        public LoggedGetUnitStringByIdRequest(
            ILogger<LoggedGetUnitStringByIdRequest> logger,
            IPipelineNode<
                IGetUnitStringByIdRequestContract, 
                IGetUnitStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetUnitStringByIdResultContract> Ask(
            IGetUnitStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetUnitStringByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetUnitStringByIdLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetUnitStringByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetUnitStringByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}