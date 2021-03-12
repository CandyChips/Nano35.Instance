using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringById
{
    public class LoggedGetWorkerStringByIdRequest :
        IPipelineNode<
            IGetWorkerStringByIdRequestContract,
            IGetWorkerStringByIdResultContract>
    {
        private readonly ILogger<LoggedGetWorkerStringByIdRequest> _logger;
        private readonly IPipelineNode<
            IGetWorkerStringByIdRequestContract, 
            IGetWorkerStringByIdResultContract> _nextNode;

        public LoggedGetWorkerStringByIdRequest(
            ILogger<LoggedGetWorkerStringByIdRequest> logger,
            IPipelineNode<
                IGetWorkerStringByIdRequestContract, 
                IGetWorkerStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetWorkerStringByIdResultContract> Ask(
            IGetWorkerStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetWorkerStringByIdLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetWorkerStringByIdLogger ends on: {DateTime.Now}");

            switch (result)
            {
                case IGetWorkerStringByIdSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetWorkerStringByIdErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}