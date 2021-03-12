using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringById
{
    public class LoggedGetClientStringByIdRequest :
        IPipelineNode<
            IGetClientStringByIdRequestContract,
            IGetClientStringByIdResultContract>
    {
        private readonly ILogger<LoggedGetClientStringByIdRequest> _logger;
        private readonly IPipelineNode<
            IGetClientStringByIdRequestContract, 
            IGetClientStringByIdResultContract> _nextNode;

        public LoggedGetClientStringByIdRequest(
            ILogger<LoggedGetClientStringByIdRequest> logger,
            IPipelineNode<
                IGetClientStringByIdRequestContract, 
                IGetClientStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetClientStringByIdResultContract> Ask(
            IGetClientStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetClientStringByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetClientStringByIdLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetClientStringByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetClientStringByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}