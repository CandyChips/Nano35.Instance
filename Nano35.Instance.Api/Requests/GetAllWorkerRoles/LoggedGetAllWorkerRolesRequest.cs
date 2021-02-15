using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkerRoles
{
    public class LoggedGetAllWorkerRolesRequest :
        IPipelineNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>
    {
        private readonly ILogger<LoggedGetAllWorkerRolesRequest> _logger;
        private readonly IPipelineNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract> _nextNode;

        public LoggedGetAllWorkerRolesRequest(
            ILogger<LoggedGetAllWorkerRolesRequest> logger,
            IPipelineNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllWorkerRolesResultContract> Ask(
            IGetAllWorkerRolesRequestContract input)
        {
            _logger.LogInformation($"GetAllWorkerRolesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllWorkerRoles  Logger ends on: {DateTime.Now}");
            return result;
        }
    }
}