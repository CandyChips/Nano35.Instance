using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkers
{
    public class LoggedGetAllWorkersRequest :
        IPipelineNode<
            IGetAllWorkersRequestContract,
            IGetAllWorkersResultContract>
    {
        private readonly ILogger<LoggedGetAllWorkersRequest> _logger;
        private readonly IPipelineNode<
            IGetAllWorkersRequestContract, 
            IGetAllWorkersResultContract> _nextNode;

        public LoggedGetAllWorkersRequest(
            ILogger<LoggedGetAllWorkersRequest> logger,
            IPipelineNode<
                IGetAllWorkersRequestContract, 
                IGetAllWorkersResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllWorkersResultContract> Ask(
            IGetAllWorkersRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllWorkersLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllWorkersLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllWorkersSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllWorkersErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}