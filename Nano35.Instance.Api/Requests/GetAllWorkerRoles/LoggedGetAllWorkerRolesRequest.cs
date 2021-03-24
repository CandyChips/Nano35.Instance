using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkerRoles
{
    public class LoggedGetAllWorkerRolesRequest :
        PipeNodeBase<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>
    {
        private readonly ILogger<LoggedGetAllWorkerRolesRequest> _logger;

        public LoggedGetAllWorkerRolesRequest(
            ILogger<LoggedGetAllWorkerRolesRequest> logger,
            IPipeNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract> next) :
            base(next)
        {
            _logger = logger;
        }


        public override async Task<IGetAllWorkerRolesResultContract> Ask(
            IGetAllWorkerRolesRequestContract input)
        {
            _logger.LogInformation($"GetAllWorkerRolesLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllWorkerRoles  Logger ends on: {DateTime.Now}");
            
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