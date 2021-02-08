using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetWorkerById
{
    public class GetWorkerByIdLogger :
        IPipelineNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>
    {
        private readonly ILogger<GetWorkerByIdLogger> _logger;
        private readonly IPipelineNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract> _nextNode;

        public GetWorkerByIdLogger(
            ILogger<GetWorkerByIdLogger> logger,
            IPipelineNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetWorkerByIdResultContract> Ask(IGetWorkerByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetWorkerByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetWorkerByIdLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}