using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllWorkers
{
    public class GetAllWorkersLogger :
        IPipelineNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract>
    {
        private readonly ILogger<GetAllWorkersLogger> _logger;
        private readonly IPipelineNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract> _nextNode;

        public GetAllWorkersLogger(
            ILogger<GetAllWorkersLogger> logger,
            IPipelineNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllWorkersResultContract> Ask(IGetAllWorkersRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllWorkersLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllWorkersLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}