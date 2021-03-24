using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringsByIds
{
    public class LoggedGetWorkerStringsByIdsRequest :
        IPipelineNode<
            IGetWorkerStringsByIdsRequestContract,
            IGetWorkerStringsByIdsResultContract>
    {
        private readonly ILogger<LoggedGetWorkerStringsByIdsRequest> _logger;
        private readonly IPipelineNode<
            IGetWorkerStringsByIdsRequestContract, 
            IGetWorkerStringsByIdsResultContract> _nextNode;

        public LoggedGetWorkerStringsByIdsRequest(
            ILogger<LoggedGetWorkerStringsByIdsRequest> logger,
            IPipelineNode<
                IGetWorkerStringsByIdsRequestContract, 
                IGetWorkerStringsByIdsResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetWorkerStringsByIdsResultContract> Ask(
            IGetWorkerStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetWorkerStringsByIdsLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetWorkerStringsByIdsLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetWorkerStringsByIdsSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetWorkerStringsByIdsErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}