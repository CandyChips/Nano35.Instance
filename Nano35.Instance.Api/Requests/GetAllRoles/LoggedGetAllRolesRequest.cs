using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRoles
{
    public class LoggedGetAllRolesRequest :
        IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract>
    {
        private readonly ILogger<LoggedGetAllRolesRequest> _logger;
        private readonly IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract> _nextNode;

        public LoggedGetAllRolesRequest(
            ILogger<LoggedGetAllRolesRequest> logger,
            IPipelineNode<IGetAllRolesRequestContract, IGetAllRolesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input)
        {
            _logger.LogInformation($"GetAllRolesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input);
            _logger.LogInformation($"GetAllRolesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllRolesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllRolesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}