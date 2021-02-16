using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetWorkerById
{
    public class LoggedGetWorkerByIdRequest :
        IPipelineNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>
    {
        private readonly ILogger<LoggedGetWorkerByIdRequest> _logger;
        private readonly IPipelineNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract> _nextNode;

        public LoggedGetWorkerByIdRequest(
            ILogger<LoggedGetWorkerByIdRequest> logger,
            IPipelineNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetWorkerByIdResultContract> Ask(IGetWorkerByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedGetWorkerById starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedGetWorkerById ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetWorkerByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetWorkerByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}