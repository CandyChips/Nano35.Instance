using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateWorker
{
    public class LoggedCreateWorkerRequest :
        IPipelineNode<
            ICreateWorkerRequestContract,
            ICreateWorkerResultContract>
    {
        private readonly ILogger<LoggedCreateWorkerRequest> _logger;
        private readonly IPipelineNode<
            ICreateWorkerRequestContract, 
            ICreateWorkerResultContract> _nextNode;

        public LoggedCreateWorkerRequest(
            ILogger<LoggedCreateWorkerRequest> logger,
            IPipelineNode<
                ICreateWorkerRequestContract, 
                ICreateWorkerResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateWorkerLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"CreateWorkerLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case ICreateWorkerSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case ICreateWorkerErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}