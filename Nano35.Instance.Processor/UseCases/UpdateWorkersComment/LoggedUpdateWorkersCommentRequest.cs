using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersComment
{
    public class LoggedUpdateWorkersCommentRequest :
        IPipelineNode<
            IUpdateWorkersCommentRequestContract,
            IUpdateWorkersCommentResultContract>
    {
        private readonly ILogger<LoggedUpdateWorkersCommentRequest> _logger;
        private readonly IPipelineNode<
            IUpdateWorkersCommentRequestContract,
            IUpdateWorkersCommentResultContract> _nextNode;

        public LoggedUpdateWorkersCommentRequest(
            ILogger<LoggedUpdateWorkersCommentRequest> logger,
            IPipelineNode<
                IUpdateWorkersCommentRequestContract,
                IUpdateWorkersCommentResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateWorkersCommentResultContract> Ask(
            IUpdateWorkersCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateWorkersComment starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateWorkersComment ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateWorkersCommentSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateWorkersCommentErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}