using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkerRoles
{
    public class LoggedGetAllWorkerRolesRequest :
        IPipelineNode<
            IGetAllWorkerRolesRequestContract,
            IGetAllWorkerRolesResultContract>
    {
        private readonly ILogger<LoggedGetAllWorkerRolesRequest> _logger;
        
        private readonly IPipelineNode<
            IGetAllWorkerRolesRequestContract, 
            IGetAllWorkerRolesResultContract> _nextNode;

        public LoggedGetAllWorkerRolesRequest(
            ILogger<LoggedGetAllWorkerRolesRequest> logger,
            IPipelineNode<
                IGetAllWorkerRolesRequestContract,
                IGetAllWorkerRolesResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<IGetAllWorkerRolesResultContract> Ask(
            IGetAllWorkerRolesRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllWorkerRolesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllWorkerRolesLogger ends on: {DateTime.Now}");
            
            switch (result)
            {
                case IGetAllWorkerRolesSuccessResultContract success:
                    _logger.LogInformation("with success");
                    break;
                case IGetAllWorkerRolesErrorResultContract error:
                    _logger.LogError($"with error {error.Message}");
                    break;
            }
            return result;
        }
    }
}