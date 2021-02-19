using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateInstanceRealName
{
    public class LoggedUpdateInstanceRealNameRequest :
        IPipelineNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceRealNameRequest> _logger;
        private readonly IPipelineNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract> _nextNode;

        public LoggedUpdateInstanceRealNameRequest(
            ILogger<LoggedUpdateInstanceRealNameRequest> logger,
            IPipelineNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateInstanceRealNameResultContract> Ask(IUpdateInstanceRealNameRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateInstanceRealName starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateInstanceRealName ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceRealNameSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceRealNameErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}