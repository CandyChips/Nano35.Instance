using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class CreateWorkerLogger :
        IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly ILogger<CreateWorkerLogger> _logger;
        private readonly IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract> _nextNode;

        public CreateWorkerLogger(
            ILogger<CreateWorkerLogger> logger,
            IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input)
        {
            _logger.LogInformation($"CreateWorkerLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"CreateWorkerLogger ends on: {DateTime.Now}");
            _logger.LogInformation("");
            return result;
        }
    }
}