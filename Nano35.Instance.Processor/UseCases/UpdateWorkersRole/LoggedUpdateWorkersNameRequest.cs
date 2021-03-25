using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersRole
{
    public class LoggedUpdateWorkersRoleRequest :
        PipeNodeBase<
            IUpdateWorkersRoleRequestContract,
            IUpdateWorkersRoleResultContract>
    {
        private readonly ILogger<LoggedUpdateWorkersRoleRequest> _logger;
        public LoggedUpdateWorkersRoleRequest(
            ILogger<LoggedUpdateWorkersRoleRequest> logger,
            IPipeNode<IUpdateWorkersRoleRequestContract,
                IUpdateWorkersRoleResultContract> next) : base(next)
        {
            _logger = logger;
        }

        public override async Task<IUpdateWorkersRoleResultContract> Ask(
            IUpdateWorkersRoleRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoggedUpdateWorkersRole starts on: {DateTime.Now}");
            var result = await DoNext(input, cancellationToken);
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