using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceById
{
    public class LoggedGetInstanceByIdRequest :
        IPipelineNode<
            IGetInstanceByIdRequestContract,
            IGetInstanceByIdResultContract>
    {
        private readonly ILogger<LoggedGetInstanceByIdRequest> _logger;
        
        private readonly IPipelineNode<
            IGetInstanceByIdRequestContract,
            IGetInstanceByIdResultContract> _nextNode;

        public LoggedGetInstanceByIdRequest(
            ILogger<LoggedGetInstanceByIdRequest> logger,
            IPipelineNode<
                IGetInstanceByIdRequestContract,
                IGetInstanceByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetInstanceByIdResultContract> Ask(
            IGetInstanceByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedGetInstanceById starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedGetInstanceById ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetInstanceByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetInstanceByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}