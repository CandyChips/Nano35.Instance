using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringById
{
    public class LoggedGetInstanceStringByIdRequest :
        IPipelineNode<
            IGetInstanceStringByIdRequestContract,
            IGetInstanceStringByIdResultContract>
    {
        private readonly ILogger<LoggedGetInstanceStringByIdRequest> _logger;
        private readonly IPipelineNode<
            IGetInstanceStringByIdRequestContract, 
            IGetInstanceStringByIdResultContract> _nextNode;

        public LoggedGetInstanceStringByIdRequest(
            ILogger<LoggedGetInstanceStringByIdRequest> logger,
            IPipelineNode<
                IGetInstanceStringByIdRequestContract, 
                IGetInstanceStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetInstanceStringByIdResultContract> Ask(
            IGetInstanceStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetInstanceStringByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetInstanceStringByIdLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetInstanceStringByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetInstanceStringByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}