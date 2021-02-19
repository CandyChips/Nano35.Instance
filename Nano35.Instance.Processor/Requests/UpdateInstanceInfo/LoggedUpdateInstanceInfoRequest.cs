using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateInstanceInfo
{
    public class LoggedUpdateInstanceInfoRequest :
        IPipelineNode<
            IUpdateInstanceInfoRequestContract,
            IUpdateInstanceInfoResultContract>
    {
        private readonly ILogger<LoggedUpdateInstanceInfoRequest> _logger;
        
        private readonly IPipelineNode<
            IUpdateInstanceInfoRequestContract, 
            IUpdateInstanceInfoResultContract> _nextNode;

        public LoggedUpdateInstanceInfoRequest(
            ILogger<LoggedUpdateInstanceInfoRequest> logger,
            IPipelineNode<
                IUpdateInstanceInfoRequestContract,
                IUpdateInstanceInfoResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateInstanceInfoResultContract> Ask(
            IUpdateInstanceInfoRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateInstanceInfo starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateInstanceInfo ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateInstanceInfoSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateInstanceInfoErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}