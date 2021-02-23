using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateWorkersRole
{
    public class LoggedUpdateWorkersRoleRequest :
        IPipelineNode<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>
    {
        private readonly ILogger<LoggedUpdateWorkersRoleRequest> _logger;
        private readonly IPipelineNode<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract> _nextNode;

        public LoggedUpdateWorkersRoleRequest(
            ILogger<LoggedUpdateWorkersRoleRequest> logger,
            IPipelineNode<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IUpdateWorkersRoleResultContract> Ask(
            IUpdateWorkersRoleRequestContract input)
        {
            _logger.LogInformation($"UpdateWorkersRoleLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"UpdateWorkersRoleLogger ends on: {DateTime.Now}");
            
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