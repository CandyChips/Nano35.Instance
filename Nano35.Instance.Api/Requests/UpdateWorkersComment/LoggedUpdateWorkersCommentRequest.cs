using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateWorkersComment
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
            IUpdateWorkersCommentRequestContract input)
        {
            _logger.LogInformation($"UpdateWorkersCommentLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateWorkersCommentLogger ends on: {DateTime.Now}");
            
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