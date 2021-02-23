using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateWorkersRole
{
    public class LoggedUpdateWorkersRoleRequest :
        IPipelineNode<
            IUpdateWorkersRoleRequestContract,
            IUpdateWorkersRoleResultContract>
    {
        private readonly ILogger<LoggedUpdateWorkersRoleRequest> _logger;
        private readonly IPipelineNode<
            IUpdateWorkersRoleRequestContract,
            IUpdateWorkersRoleResultContract> _nextNode;

        public LoggedUpdateWorkersRoleRequest(
            ILogger<LoggedUpdateWorkersRoleRequest> logger,
            IPipelineNode<
                IUpdateWorkersRoleRequestContract,
                IUpdateWorkersRoleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateWorkersRoleResultContract> Ask(
            IUpdateWorkersRoleRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateWorkersRole starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"LoggedUpdateWorkersRole ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IUpdateWorkersRoleSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IUpdateWorkersRoleErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}