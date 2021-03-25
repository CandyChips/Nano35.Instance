using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateWorkersComment
{
    public class LoggedUpdateWorkersCommentRequest :
        PipeNodeBase<
            IUpdateWorkersCommentRequestContract,
            IUpdateWorkersCommentResultContract>
    {
        private readonly ILogger<LoggedUpdateWorkersCommentRequest> _logger;

        public LoggedUpdateWorkersCommentRequest(
            ILogger<LoggedUpdateWorkersCommentRequest> logger,
            IPipeNode<IUpdateWorkersCommentRequestContract,
                IUpdateWorkersCommentResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateWorkersCommentResultContract> Ask(
            IUpdateWorkersCommentRequestContract input)
        {
            _logger.LogInformation($"UpdateWorkersCommentLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
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